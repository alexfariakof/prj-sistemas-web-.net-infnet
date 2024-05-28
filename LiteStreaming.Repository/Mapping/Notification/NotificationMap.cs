using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Notifications;

namespace Repository.Mapping.Notifications
{
    public  class NotificationMap : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.ToTable(nameof(Notification));

            builder.HasKey(notification => notification.Id);
            builder.Property(notification => notification.Id).ValueGeneratedOnAdd();
            builder.Property(notification => notification.Title).IsRequired().HasMaxLength(150);
            builder.Property(notification => notification.Message).IsRequired().HasMaxLength(250);
            builder.Property(notification => notification.DtNotification).IsRequired();
            builder.Property(notification => notification.NotificationType).IsRequired();

            builder.HasOne(notification => notification.Destination).WithMany(customer => customer.Notifications).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(notification => notification.Sender).WithMany().IsRequired(false);
        }
    }
}
