namespace LiteStreaming.Application.Core.Interfaces.Query;

public interface IFindAll<T> where T : class, new()
{
    List<T> FindAll();
}
