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
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(m => m.Id);

           
            builder.Property(m => m.MessageText)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(m => m.MessageType)
                .IsRequired()
                .HasConversion<string>() 
                .HasMaxLength(20);

            builder.Property(m => m.IsRead)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(m => m.SentAt)
                .IsRequired();

            builder.Property(m => m.ReadAt)
                .IsRequired(false);

           
            builder.HasOne(m => m.Sender)
                .WithMany(u => u.SentMessages)
                .HasForeignKey(m => m.SenderId)
                .HasPrincipalKey(u=>u.UserId)
                .OnDelete(DeleteBehavior.Restrict);  

            
            builder.HasOne(m => m.Receiver)
                .WithMany(u => u.ReceivedMessages)
                .HasForeignKey(m => m.ReceiverId)
                .HasPrincipalKey(u => u.UserId)
                .OnDelete(DeleteBehavior.Restrict);  


            builder.HasOne(m => m.Course)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.CourseId)
                .OnDelete(DeleteBehavior.SetNull);  

            // Indexes
            builder.HasIndex(m => new { m.SenderId, m.SentAt })
                .HasDatabaseName("IX_Messages_SenderProfileId_SentAt");

            builder.HasIndex(m => new { m.ReceiverId, m.IsRead, m.SentAt })
                .HasDatabaseName("IX_Messages_ReceiverProfileId_IsRead_SentAt");

            builder.HasIndex(m => m.CourseId)
                .HasDatabaseName("IX_Messages_CourseId");

            builder.HasIndex(m => m.MessageType)
                .HasDatabaseName("IX_Messages_MessageType");
        }
    }
}
