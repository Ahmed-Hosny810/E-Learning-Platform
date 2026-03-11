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
    public class QuizAttemptConfiguration : IEntityTypeConfiguration<QuizAttempt>
    {
        public void Configure(EntityTypeBuilder<QuizAttempt> builder)
        {
            builder.HasKey(qa => qa.Id);

            // Precision for percentages like 88.50%
            builder.Property(qa => qa.Percentage).HasPrecision(5, 2);

            // Security: Don't delete student history if a Quiz is deleted
            builder.HasOne(qa => qa.Quiz)
                   .WithMany(q => q.QuizAttempts)
                   .HasForeignKey(qa => qa.QuizId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(qa => qa.UserAnswers)
                   .WithOne(ua => ua.QuizAttempt)
                   .HasForeignKey(ua => ua.QuizAttemptId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
