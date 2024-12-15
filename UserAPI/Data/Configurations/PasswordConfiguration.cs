using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UserAPI.Models;

namespace UserAPI.Data.Configurations
{
    public class PasswordConfiguration : IEntityTypeConfiguration<Password>
    {
        public void Configure(EntityTypeBuilder<Password> builder)
        {
            builder.Property(p => p.PasswordId)
                .HasDefaultValueSql("NEWSEQUENTIALID()");

            builder.Property(p => p.PasswordHash)
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnType("nvarchar(30)");

            builder.Property(p => p.PasswordSalt)
                .IsRequired()
                .HasMaxLength(4)
                .HasColumnType("nvarchar(4)");

            builder.Property(p => p.UserId)
                .IsRequired();

            builder.HasOne(p => p.User)
            .WithOne(u => u.Password)
            .HasForeignKey<Password>(p => p.UserId)
            .IsRequired();
        }
    }
}
