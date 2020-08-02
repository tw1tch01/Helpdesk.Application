using Helpdesk.Domain.Users;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Helpdesk.Persistence.Configurations.Users
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            #region Primary Key

            builder.HasKey(user => user.UserId);

            #endregion Primary Key

            #region Properties

            builder.Property(user => user.Username)
                   .IsRequired()
                   .HasMaxLength(64);

            builder.Property(user => user.Name)
                   .IsRequired()
                   .HasMaxLength(64);

            builder.Property(user => user.Surname)
                   .IsRequired()
                   .HasMaxLength(64);

            builder.Property(user => user.Alias)
                   .HasMaxLength(128);

            builder.Property(user => user.Email)
                   .IsRequired()
                   .HasMaxLength(512);

            #endregion Properties

            #region Indexes

            builder.HasIndex(user => user.Username)
                   .IsUnique();

            builder.HasIndex(user => user.Email)
                   .IsUnique();

            #endregion Indexes

            base.Configure(builder);
        }
    }
}