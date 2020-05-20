using System;
using Helpdesk.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace Helpdesk.Persistence.Common.Configurations
{
    public abstract class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(entity => entity.CreatedBy)
                   .IsRequired();

            builder.Property(entity => entity.CreatedOn)
                   .IsRequired();

            builder.Property(entity => entity.CreatedProcess)
                   .IsRequired()
                   .HasMaxLength(1024);

            builder.Property(entity => entity.ModifiedProcess)
                   .HasMaxLength(1024);

            builder.Property(entity => entity.Identifier)
                   .IsRequired()
                   .HasValueGenerator(typeof(GuidValueGenerator));
        }
    }
}