using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Library.UserEntities;

namespace UserAPI.Data.Configurations
{
    public class PasswordConfiguration : IEntityTypeConfiguration<Password>
    {
        public void Configure(EntityTypeBuilder<Password> builder)
        {
            builder.Property(p => p.PasswordId)
                .HasDefaultValueSql("NEWSEQUENTIALID()");
        }
    }
}
