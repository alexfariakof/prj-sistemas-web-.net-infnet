using System.Linq.Expressions;

namespace Repository;
public abstract class RepositoryBase<T> where T : class, new()
{
    protected RegisterContext Context { get; set; }

    public RepositoryBase(RegisterContext context)
    {
        Context = context;
    }

    public virtual void Save(T entity)
    {
        this.Context.Add(entity);
        this.Context.SaveChanges();
    }

    public virtual void Update(T entity)
    {
        this.Context.Update(entity);
        this.Context.SaveChanges();
    }
    public virtual void Delete(T entity)
    {
        this.Context.Remove(entity);
        this.Context.SaveChanges();
    }
    public virtual IEnumerable<T> GetAll()
    {
        return this.Context.Set<T>().ToList();
    }

    public virtual T GetById(Guid id)
    {
        return this.Context.Set<T>().Find(id) ?? new();
    }

    public virtual IEnumerable<T> Find(Expression<Func<T, bool>> expression)
    {
        return this.Context.Set<T>().Where(expression);
    }

    public virtual bool Exists(Expression<Func<T, bool>> expression)
    {
        return this.Find(expression).Any();
    }
}