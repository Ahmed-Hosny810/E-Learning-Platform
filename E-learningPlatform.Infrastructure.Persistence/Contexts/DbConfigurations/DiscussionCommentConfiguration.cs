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
    public class DiscussionCommentConfiguration : IEntityTypeConfiguration<DiscussionComment>
    {
        public void Configure(EntityTypeBuilder<DiscussionComment> builder)
        {
            builder.ToTable("DiscussionComments");

            builder.HasKey(dc => dc.Id);

            builder.Property(dc => dc.Content)
                .IsRequired()
                .HasMaxLength(2000);  

            builder.Property(dc => dc.IsEdited)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(dc => dc.LikeCount)
                .IsRequired()
                .HasDefaultValue(0);

            // Relationships
            builder.HasOne(dc => dc.Post)
                .WithMany(dp => dp.Comments)
                .HasForeignKey(dc => dc.PostId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(dc => dc.UserProfile)
                   .WithMany(u => u.DiscussionComments)
                   .HasForeignKey(dc => dc.UserId)
                   .HasPrincipalKey(u => u.UserId)
                   .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(dc => dc.ParentComment)
                .WithMany(dc => dc.Replies)
                .HasForeignKey(dc => dc.ParentCommentId)
                .OnDelete(DeleteBehavior.Restrict);  

            // Indexes
            builder.HasIndex(dc => new { dc.PostId, dc.CreatedAt })
                .HasDatabaseName("IX_DiscussionComments_PostId_CreatedAt");

            builder.HasIndex(dc => dc.ParentCommentId)
                .HasDatabaseName("IX_DiscussionComments_ParentCommentId");

            builder.HasIndex(dc => dc.UserId)
                .HasDatabaseName("IX_DiscussionComments_UserId");
        }
    }
}
