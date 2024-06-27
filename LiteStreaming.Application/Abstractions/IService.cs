namespace LiteStreaming.Application.Abstractions;
public interface IService<T> where T : class, new()
{
    T Create(T obj);
    List<T> FindAll();
    T FindById(Guid id);
    T Update(T obj);
    bool Delete(T obj);
}