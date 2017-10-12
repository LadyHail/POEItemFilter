using System.Data.Entity.ModelConfiguration;
using POEItemFilter.Models.ItemsDB;

namespace POEItemFilter.Library.EntityConfiguration
{
    public class ItemBaseTypeConfiguration : EntityTypeConfiguration<ItemBaseType>
    {
        public ItemBaseTypeConfiguration()
        {
            HasKey(i => i.Id);

            Property(i => i.Id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(50);

            HasMany(i => i.Types)
                .WithRequired()
                .WillCascadeOnDelete(false);
        }
    }
}