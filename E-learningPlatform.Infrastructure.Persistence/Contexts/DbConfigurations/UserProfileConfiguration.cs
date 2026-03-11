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
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable("UserProfiles");
            builder.HasKey(up => up.Id);
            builder.Property(up => up.UserId)
                   .IsRequired()
                   .HasMaxLength(150);
            builder.HasIndex(u => u.UserId)
                .IsUnique();
            builder.Property(u => u.DisplayName)
            .IsRequired()
            .HasMaxLength(150);

            builder.Property(u => u.Bio)
                .HasMaxLength(2000);

            builder.Property(u => u.ProfilePictureUrl)
                .HasMaxLength(500);

            builder.Property(up => up.Email)
                    .IsRequired()
                    .HasMaxLength(256);

            builder.HasIndex(up => up.Email)
                .IsUnique();

            builder.Property(u => u.CreatedAt)
                .IsRequired();

            builder.Property(u => u.UpdatedAt)
                .IsRequired();

        }
    }
}
