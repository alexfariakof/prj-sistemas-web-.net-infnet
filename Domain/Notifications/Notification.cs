using Domain.Account.Agreggates;
using Domain.Core.Aggreggates;

namespace Domain.Notifications;
public class Notification : BaseModel
{
    public DateTime DtNotification { get; set; }
    public String Message { get; set; }
    public String Title { get; set; }
    public virtual Customer Destination { get; set; }
    public virtual Customer? Sender { get; set; }
    public NotificationType NotificationType { get; set; }
    public static Notification Create(string titlle, string message, NotificationType notificationType, Customer destination, Customer sender = null)
    {
        if (notificationType == NotificationType.User && sender == null)
            throw new ArgumentNullException("Para tipo de mensagem 'usuário', você deve informar quem foi o remetente");

        if (string.IsNullOrWhiteSpace(titlle))
            throw new ArgumentNullException("Informe o titulo da notificacao");

        if (string.IsNullOrWhiteSpace(message))
            throw new ArgumentNullException("Informe o mensagem da notificacao");

        return new Notification()
        {
            DtNotification = DateTime.Now,
            Message = message,
            NotificationType = notificationType,
            Title = titlle,
            Destination = destination,
            Sender = sender
        };
    }
}
public enum NotificationType
{
    User = 1,
    System = 2
}