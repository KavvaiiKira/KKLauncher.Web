using KKLauncher.DB.Entities;
using KKLauncher.Web.Server.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace KKLauncher.Web.Server.EfCore.EntityConfigurations
{
    public class WhitelistConfiguration : IEntityTypeConfiguration<WhitelistEntity>
    {
        public void Configure(EntityTypeBuilder<WhitelistEntity> builder)
        {
            builder.ToTable(DbConstants.TableNames.WHITELISTS);

            builder
                .HasKey(a => a.Id);

            builder
                .Property(a => a.Id)
                .HasColumnName(DbConstants.Fields.ID)
                .IsRequired(true);

            builder
                .Property(a => a.TelegramId)
                .HasColumnName(DbConstants.Fields.TELEGRAMID)
                .IsRequired(true);
        }
    }
}
