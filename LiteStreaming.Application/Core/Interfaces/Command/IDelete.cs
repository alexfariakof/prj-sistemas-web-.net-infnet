namespace LiteStreaming.Application.Core.Interfaces.Command;

public interface IDelete<T> where T : class, new()
{
    bool Delete(T obj);
}