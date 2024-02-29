namespace Application;
public interface IService<T> where T : class, new()
{
    T Create(T obj);
    List<T> FindAll(Guid idUsuario);
    T FindById(Guid id);
    T Update(T obj);
    bool Delete(T obj);
}