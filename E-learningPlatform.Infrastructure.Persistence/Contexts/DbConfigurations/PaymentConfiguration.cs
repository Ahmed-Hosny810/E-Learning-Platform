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
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.ProviderPaymentId)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Currency)
                .IsRequired()
                .HasMaxLength(3)
                .IsFixedLength();

            builder.Property(p => p.PaymentMethod)
                .HasMaxLength(50)
                .IsRequired(false);

            builder.Property(p => p.Status)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(p => p.FailureReason)  
                .HasMaxLength(500);

            builder.HasOne(p => p.Enrollment)
                .WithMany(e => e.Payments)
                .HasForeignKey(p => p.EnrollmentId)
                .OnDelete(DeleteBehavior.Restrict);  

            builder.HasIndex(p => p.EnrollmentId)
                .HasDatabaseName("IX_Payments_EnrollmentId");

            builder.HasIndex(p => p.ProviderPaymentId)
                .IsUnique()
                .HasDatabaseName("IX_Payments_ProviderPaymentId");

            builder.HasIndex(p => p.Status)
                .HasDatabaseName("IX_Payments_Status");
        }
    }
}
