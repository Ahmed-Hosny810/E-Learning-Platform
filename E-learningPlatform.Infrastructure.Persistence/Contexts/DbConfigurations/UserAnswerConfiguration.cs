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
    public class UserAnswerConfiguration : IEntityTypeConfiguration<UserAnswer>
    {
        public void Configure(EntityTypeBuilder<UserAnswer> builder)
        {
            builder.HasKey(ua => ua.Id);

            builder.HasOne(ua => ua.Question)
               .WithMany() 
               .HasForeignKey(ua => ua.QuestionId)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ua => ua.SelectedOption)
               .WithMany()
               .HasForeignKey(ua => ua.SelectedOptionId)
               .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
