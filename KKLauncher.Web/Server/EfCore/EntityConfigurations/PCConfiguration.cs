﻿using KKLauncher.DB.Entities;
using KKLauncher.Web.Server.Constants;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace KKLauncher.Web.Server.EfCore.EntityConfigurations
{
    public class PCConfiguration : IEntityTypeConfiguration<PCEntity>
    {
        public void Configure(EntityTypeBuilder<PCEntity> builder)
        {
            builder.ToTable(DbConstants.TableNames.PCS);

            builder
                .HasKey(a => a.Id);

            builder
                .Property(a => a.Id)
                .HasColumnName(DbConstants.Fields.ID);

            builder
                .Property(a => a.Name)
                .HasColumnName(DbConstants.Fields.NAME);

            builder
                .Property(a => a.LocalIp)
                .HasColumnName(DbConstants.Fields.LOCALIP);

            builder
                .Property(a => a.Password)
                .HasColumnName(DbConstants.Fields.PASSWORD);

            builder
                .Property(a => a.SteamPath)
                .HasColumnName(DbConstants.Fields.STEAMPATH);
        }
    }
}
