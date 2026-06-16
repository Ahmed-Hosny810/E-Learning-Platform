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
    public class QuizConfiguration : IEntityTypeConfiguration<Quiz>
    {
        public void Configure(EntityTypeBuilder<Quiz> builder)
        {
            builder.ToTable("Quizzes"); 

            builder.HasKey(q => q.Id);

            builder.Property(q => q.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(q => q.Description)
                .HasMaxLength(1000);

            builder.Property(q => q.DisplayOrder)
                .IsRequired();

            builder.Property(q => q.IsPublished)
                .HasDefaultValue(false);

            builder.Property(q => q.IsRequired)
                .HasDefaultValue(false);

            builder.Property(q => q.ShowCorrectAnswers)
                .HasDefaultValue(false);

            builder.Property(q => q.ShuffleQuestions)
                .HasDefaultValue(false);

            builder.Property(q => q.ShuffleOptions)
                .HasDefaultValue(false);

            builder.HasIndex(q => new { q.SectionId, q.DisplayOrder });

            builder.HasOne(q => q.Section)
                   .WithMany(s => s.Quizzes)
                   .HasForeignKey(q => q.SectionId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(q => q.Questions)
                   .WithOne(qs => qs.Quiz)
                   .HasForeignKey(qs => qs.QuizId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
