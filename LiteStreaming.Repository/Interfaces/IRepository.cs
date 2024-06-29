using Microsoft.Data.SqlClient;
using System.Linq.Expressions;

namespace Repository.Interfaces;
public interface IRepository<T> where T : class, new()
{
    public void Save(T entity);
    public void Update(T entity);
    public void Delete(T entity);
    public IEnumerable<T> FindAll(string sortProperty = null, SortOrder sortOrder = 0);
    public T GetById(Guid id);
    public T GetById(int id);
    public IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    public bool Exists(Expression<Func<T, bool>> expression);
}