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
            builder.ToTable("QuizAttempts");  

            builder.HasKey(qa => qa.Id);

            builder.Ignore(qa => qa.Percentage);

            builder.Property(qa => qa.Status)
                .IsRequired()
                .HasMaxLength(20);           

            builder.Property(qa => qa.Score)
                .HasDefaultValue(0);          

            builder.Property(qa => qa.TotalPoints)
                .HasDefaultValue(0);          

            builder.Property(qa => qa.StudentId)
                .IsRequired();                 


            builder.HasOne(qa => qa.Quiz)
                   .WithMany(q => q.QuizAttempts)
                   .HasForeignKey(qa => qa.QuizId)
                   .OnDelete(DeleteBehavior.Restrict); 

            builder.HasOne(qa => qa.Enrollment)
                   .WithMany()
                   .HasForeignKey(qa => qa.EnrollmentId)
                   .OnDelete(DeleteBehavior.Restrict); 

            builder.HasMany(qa => qa.UserAnswers)
                   .WithOne(ua => ua.QuizAttempt)
                   .HasForeignKey(ua => ua.QuizAttemptId)
                   .OnDelete(DeleteBehavior.Cascade); 

            // Indexes
            builder.HasIndex(qa => new { qa.StudentId, qa.QuizId })
                .HasDatabaseName("IX_QuizAttempts_StudentId_QuizId"); 
        }
    }
}
