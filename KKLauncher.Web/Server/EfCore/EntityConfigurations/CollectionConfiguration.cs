using KKLauncher.DB.Entities;
using KKLauncher.Web.Server.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace KKLauncher.Web.Server.EfCore.EntityConfigurations
{
    public class CollectionConfiguration : IEntityTypeConfiguration<CollectionEntity>
    {
        public void Configure(EntityTypeBuilder<CollectionEntity> builder)
        {
            builder.ToTable(DbConstants.TableNames.COLLECTIONS);

            builder
                .HasKey(c => c.Id);

            builder
                .Property(c => c.Id)
                .HasColumnName(DbConstants.Fields.ID)
                .IsRequired(true);

            builder
                .Property(c => c.Name)
                .HasColumnName(DbConstants.Fields.NAME)
                .IsRequired(true);

            builder
                .Property(c => c.Image)
                .HasColumnName(DbConstants.Fields.IMAGE)
                .IsRequired(true);

            builder
                .Property(c => c.PCId)
                .HasColumnName(DbConstants.Fields.PCID)
                .IsRequired(true);

            builder
                .HasOne(c => c.PC)
                .WithMany()
                .HasForeignKey(c => c.PCId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(ac => ac.Apps)
                .WithMany(ac => ac.Collections)
                .UsingEntity<AppCollectionEntity>(
                    a => a.HasOne<AppEntity>().WithMany().HasForeignKey(ac => ac.AppId),
                    c => c.HasOne<CollectionEntity>().WithMany().HasForeignKey(ac => ac.CollectionId));
        }
    }
}
