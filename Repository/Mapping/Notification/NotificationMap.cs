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

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.Property(x => x.Title).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Message).IsRequired().HasMaxLength(250);
            builder.Property(x => x.DtNotification).IsRequired();
            builder.Property(x => x.NotificationType).IsRequired();

            builder.HasOne(x => x.Destination).WithMany(x => x.Notifications).IsRequired().OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Sender).WithMany().IsRequired(false);
        }
    }
}
