﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace Repository.Persistency.Abstractions;

/// <summary>
/// Repositório base que fornece operações CRUD comuns para entidades.
/// </summary>
/// <typeparam name="T">Tipo da entidade.</typeparam>
public abstract class BaseRepository<T> where T : class, new()
{
    private DbContext Context { get; set; }

    /// <summary>
    /// Inicializa uma nova instância da classe <see cref="BaseRepository{T}"/>.
    /// </summary>
    /// <param name="context">O contexto do banco de dados.</param>
    protected BaseRepository(DbContext context)
    {
        Context = context;
    }

    /// <summary>
    /// Salva a entidade especificada no banco de dados.
    /// </summary>
    /// <param name="entity">A entidade a ser salva.</param>
    public virtual void Save(T entity)
    {
        Context.Add(entity);
        Context.SaveChanges();
    }

    /// <summary>
    /// Atualiza a entidade especificada no banco de dados.
    /// </summary>
    /// <param name="entity">A entidade a ser atualizada.</param>
    public virtual void Update(T entity)
    {
        Context.Update(entity);
        Context.SaveChanges();
    }

    /// <summary>
    /// Exclui a entidade especificada do banco de dados.
    /// </summary>
    /// <param name="entity">A entidade a ser excluída.</param>
    public virtual void Delete(T entity)
    {
        Context.Remove(entity);
        Context.SaveChanges();
    }

    /// <summary>
    /// Obtém a entidade pelo identificador Guid especificado.
    /// </summary>
    /// <param name="id">O identificador Guid.</param>
    /// <returns>A entidade com o identificador Guid especificado.</returns>
    public virtual T GetById(Guid id)
    {
        return Context.Set<T>().Find(id) ?? new();
    }

    /// <summary>
    /// Obtém a entidade pelo identificador inteiro especificado.
    /// </summary>
    /// <param name="id">O identificador inteiro.</param>
    /// <returns>A entidade com o identificador inteiro especificado.</returns>
    public virtual T GetById(int id)
    {
        return Context.Set<T>().Find(id) ?? new();
    }

    /// <summary>
    /// Encontra entidades que correspondem à expressão especificada.
    /// </summary>
    /// <param name="expression">A expressão para filtrar entidades.</param>
    /// <returns>Uma coleção de entidades que correspondem à expressão especificada.</returns>
    public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
        return Context.Set<T>().Where(expression);
    }

    /// <summary>
    /// Verifica se existem entidades que correspondem à expressão especificada.
    /// </summary>
    /// <param name="expression">A expressão para filtrar entidades.</param>
    /// <returns><c>true</c> se existirem entidades que correspondem à expressão especificada; caso contrário, <c>false</c>.</returns>
    public virtual bool Exists(Expression<Func<T, bool>> expression)
    {
        return Find(expression).Any();
    }

    /// <summary>
    /// Retorna todas as entidades.
    /// </summary>
    public virtual IEnumerable<T> FindAll()
    {
        return Context.Set<T>().ToList();
    }

    /// <summary>
    /// Retorna todas as entidades ordenadas pela propriedade especificada.
    /// </summary>
    /// <param name="propertyToSort">A propriedade para ordenar. Se nula, a primeira propriedade pública é usada.</param>
    /// <param name="sortOrder">A ordem para classificar as entidades.</param>
    /// <returns>Uma coleção ordenada de entidades.</returns>
    public virtual IEnumerable<T> FindAllSorted(string propertyToSort = null, SortOrder sortOrder = SortOrder.Ascending)
    {
        var listToSort = FindAll();

        if (propertyToSort is null)
            return listToSort;

        Expression<Func<T, object>>? sortExpression = TryGetSortExpressionFromProperty(propertyToSort)
            ?? TryGetSortExpressionFromNavigation(propertyToSort)
            ?? GetSortExpressionFromDefaultProperty();

        // Ordena a lista com base na expressão de acesso à propriedade
        if (sortOrder == SortOrder.Ascending)
            return listToSort.AsQueryable().OrderBy(sortExpression).ToList();
        else
            return listToSort.AsQueryable().OrderByDescending(sortExpression).ToList();
    }

    /// <summary>
    /// Obtém uma expressão de ordenação com base na primeira propriedade pública da entidade.
    /// </summary>
    /// <returns>Expressão para acessar a primeira propriedade pública.</returns>
    private Expression<Func<T, object>> GetSortExpressionFromDefaultProperty()
    {
        var prop = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).FirstOrDefault();

        // Cria uma expressão para acessar a propriedade diretamente
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, prop?.Name);
        var converted = Expression.Convert(property, typeof(object));
        return Expression.Lambda<Func<T, object>>(converted, parameter);
    }

    /// <summary>
    /// Tenta obter uma expressão de ordenação com base no nome da propriedade especificada.
    /// </summary>
    /// <param name="propertyName">O nome da propriedade.</param>
    /// <returns>Expressão para acessar a propriedade especificada, ou null se não for encontrada.</returns>
    private Expression<Func<T, object>>? TryGetSortExpressionFromProperty(string propertyName)
    {
        var prop = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance)
                   .FirstOrDefault(p => string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase));

        if (prop is null) return null;

        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, prop.Name);
        var converted = Expression.Convert(property, typeof(object));
        return Expression.Lambda<Func<T, object>>(converted, parameter);
    }

    /// <summary>
    /// Tenta obter uma expressão de ordenação com base em uma propriedade de navegação.
    /// </summary>
    /// <param name="propertyName">O nome da propriedade de navegação.</param>
    /// <returns>Expressão para acessar a propriedade de navegação especificada, ou null se não for encontrada.</returns>
    private Expression<Func<T, object>>? TryGetSortExpressionFromNavigation(string propertyName)
    {
        var navigations = Context.Model.FindEntityType(typeof(T))?.GetNavigations();
        if (navigations != null)
        {
            foreach (var navigation in navigations)
            {
                var navigationEntityType = Context.Model.FindEntityType(navigation.ClrType);
                if (navigationEntityType != null)
                {
                    var prop = navigationEntityType.ClrType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                        .FirstOrDefault(p => string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase));
                    if (prop != null)
                    {
                        // Cria uma expressão para acessar a propriedade da entidade de navegação
                        var parameter = Expression.Parameter(typeof(T), "x");
                        var navigationProperty = Expression.Property(parameter, navigation.Name);
                        var property = Expression.Property(navigationProperty, prop.Name);
                        var converted = Expression.Convert(property, typeof(object));
                        return Expression.Lambda<Func<T, object>>(converted, parameter);
                    }
                }
            }
        }
        return null;
    }
}