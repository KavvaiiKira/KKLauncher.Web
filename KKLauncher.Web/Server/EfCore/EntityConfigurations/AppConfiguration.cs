using KKLauncher.DB.Entities;
using KKLauncher.Web.Server.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace KKLauncher.Web.Server.EfCore.EntityConfigurations
{
    public class AppConfiguration : IEntityTypeConfiguration<AppEntity>
    {
        public void Configure(EntityTypeBuilder<AppEntity> builder)
        {
            builder.ToTable(DbConstants.TableNames.APPLICATIONS);

            builder
                .HasKey(a => a.Id);

            builder
                .Property(a => a.Id)
                .HasColumnName(DbConstants.Fields.ID)
                .IsRequired(true);

            builder
                .Property(a => a.Name)
                .HasColumnName(DbConstants.Fields.NAME)
                .IsRequired(true);

            builder
                .Property(a => a.Image)
                .HasColumnName(DbConstants.Fields.IMAGE)
                .IsRequired(true);

            builder
                .Property(a => a.Path)
                .HasColumnName(DbConstants.Fields.PATH)
                .IsRequired(true);

            builder
                .Property(a => a.SteamId)
                .HasColumnName(DbConstants.Fields.STEAMID)
                .IsRequired(false);

            builder
                .Property(a => a.PCId)
                .HasColumnName(DbConstants.Fields.PCID)
                .IsRequired(true);

            builder
                .HasOne(a => a.PC)
                .WithMany()
                .HasForeignKey(a => a.PCId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(ac => ac.Collections)
                .WithMany(ac => ac.Apps)
                .UsingEntity<AppCollectionEntity>(
                    c => c.HasOne<CollectionEntity>().WithMany().HasForeignKey(ac => ac.CollectionId),
                    a => a.HasOne<AppEntity>().WithMany().HasForeignKey(ac => ac.AppId));
        }
    }
}
