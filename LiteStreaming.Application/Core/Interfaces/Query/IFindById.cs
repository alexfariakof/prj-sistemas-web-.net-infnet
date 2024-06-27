namespace LiteStreaming.Application.Core.Interfaces.Query;

public interface IFindById<T> where T : class, new()
{
    T FindById(Guid id);
}
