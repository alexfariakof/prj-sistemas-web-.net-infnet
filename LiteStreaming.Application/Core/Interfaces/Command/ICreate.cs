
namespace LiteStreaming.Application.Core.Interfaces.Command;

public interface ICreate<T> where T : class, new ()
{
    T Create(T obj);
}
