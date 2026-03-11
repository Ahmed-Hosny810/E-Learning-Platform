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
    public class QuestionOptionConfiguration : IEntityTypeConfiguration<QuestionOption>
    {
        public void Configure(EntityTypeBuilder<QuestionOption> builder)
        {
            builder.HasKey(o => o.Id);

            
            builder.Property(o => o.OptionText)
                   .IsRequired()
                   .HasMaxLength(500);

            
            builder.Property(o => o.IsCorrect)
                   .HasDefaultValue(false);

            
            builder.Property(o => o.OrderIndex)
                   .IsRequired();

            builder.HasOne(o => o.Question)
                   .WithMany(q => q.QuestionOptions)
                   .HasForeignKey(o => o.QuestionId)
                   .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
