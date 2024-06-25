namespace LiteStreaming.AdministrativeApp.Models;
public class AlertViewModel
{
    public enum AlertType
    {
        Information = 0,
        Warning = 1,
        Danger = 2,
        Success = 3
    }

    private AlertViewModel() { }
    public AlertViewModel(AlertType type, string message)
    {
        Type = GetAlertTypeString(type);
        Header = GetAlertHeader(type);
        Message = message;
    }

    public string? Header { get; set; }
    public string? Type { get; set; }
    public string? Message { get; set; }
    
    private string GetAlertHeader(AlertType type)
    {
        switch (type)
        {
            case AlertType.Information:
                return "Informação";
            case AlertType.Warning:
                return "Aviso";
            case AlertType.Danger:
                return "Erro";
            case AlertType.Success:
                return "Sucesso";
            default:
                return string.Empty;
        }
    }

    private string GetAlertTypeString(AlertType type)
    {
        switch (type)
        {
            case AlertType.Information:
                return "info";
            case AlertType.Warning:
                return "warning";
            case AlertType.Danger:
                return "danger";
            case AlertType.Success:
                return "success";
            default:
                return "dark";
        }
    }
}
