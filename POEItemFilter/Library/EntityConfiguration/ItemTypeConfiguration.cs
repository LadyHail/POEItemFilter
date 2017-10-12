﻿using System.Data.Entity.ModelConfiguration;
using POEItemFilter.Models.ItemsDB;

namespace POEItemFilter.Library.EntityConfiguration
{
    public class ItemTypeConfiguration : EntityTypeConfiguration<ItemType>
    {
        public ItemTypeConfiguration()
        {
            HasKey(i => i.Id);

            Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(50);

            Property(i => i.ItemBaseTypeId)
                .IsRequired();
        }
    }
}