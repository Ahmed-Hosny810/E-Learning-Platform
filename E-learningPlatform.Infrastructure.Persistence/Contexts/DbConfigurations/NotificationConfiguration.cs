using E_learningPlatform.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_learningPlatform.Infrastructure.Persistence.Contexts.DbConfigurations
{
    public class NotificationConfiguration : IEntityTypeConfiguration<Notification>
    {
        public void Configure(EntityTypeBuilder<Notification> builder)
        {
            builder.HasKey(n => n.Id);

            // Properties
            builder.Property(n => n.Type)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(n => n.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(n => n.Message)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(n => n.LinkUrl)
                .HasMaxLength(500);

            builder.Property(n => n.Metadata)
                .HasMaxLength(2000);  

            builder.Property(n => n.IsRead)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(n => n.ReadAt)
                .IsRequired(false);

            
            builder.HasOne(n => n.UserProfile)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .HasPrincipalKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Cascade);  

            // Indexes
            builder.HasIndex(n => new { n.UserId, n.IsRead, n.CreatedAt })
                .HasDatabaseName("IX_Notifications_UserId_IsRead_CreatedAt");

            builder.HasIndex(n => n.Type)
                .HasDatabaseName("IX_Notifications_Type");

            builder.HasIndex(n => n.CreatedAt)
                .HasDatabaseName("IX_Notifications_CreatedAt");
        }
    }
}
