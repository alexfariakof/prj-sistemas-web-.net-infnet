namespace LiteStreaming.AdministrativeApp.Models;
public abstract class BasePageModel
{
    private IEnumerable<object>? _items;

    public void SetItems<T>(IEnumerable<T>? items) where T : class, new()
    {
        _items = items;
    }

    public IEnumerable<T> GetItems<T>() where T : class, new()
    {
        return _items?.Cast<T>() ?? Enumerable.Empty<T>();
    }
}
