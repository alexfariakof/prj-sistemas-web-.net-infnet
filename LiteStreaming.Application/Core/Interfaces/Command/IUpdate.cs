namespace LiteStreaming.Application.Core.Interfaces.Command;

public interface IUpdate<T> where T : class, new()
{
    T Update(T obj);
}
