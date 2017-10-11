using System.ComponentModel.DataAnnotations.Schema;
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
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .IsRequired();

            Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(50);
        }
    }
}