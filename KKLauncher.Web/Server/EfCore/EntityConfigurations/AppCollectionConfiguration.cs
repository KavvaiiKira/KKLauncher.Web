using KKLauncher.DB.Entities;
using KKLauncher.Web.Server.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KKLauncher.Web.Server.EfCore.EntityConfigurations
{
    public class AppCollectionConfiguration : IEntityTypeConfiguration<AppCollectionEntity>
    {
        public void Configure(EntityTypeBuilder<AppCollectionEntity> builder)
        {
            builder.ToTable(DbConstants.TableNames.APPCOLLECTIONS);

            builder
                .Property(ac => ac.AppId)
                .HasColumnName(DbConstants.Fields.APPID)
                .IsRequired(true);

            builder
                .Property(ac => ac.CollectionId)
                .HasColumnName(DbConstants.Fields.COLLECTIONID)
                .IsRequired(true);
        }
    }
}
