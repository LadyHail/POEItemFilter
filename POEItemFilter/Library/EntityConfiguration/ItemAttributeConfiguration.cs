using System.Data.Entity.ModelConfiguration;
using POEItemFilter.Models.ItemsDB;

namespace POEItemFilter.Library.EntityConfiguration
{
    public class ItemAttributeConfiguration : EntityTypeConfiguration<ItemAttribute>
    {
        public ItemAttributeConfiguration()
        {
            HasKey(i => i.Id);

            Property(i => i.Id)
                .HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);

            Property(i => i.Name)
                .IsRequired()
                .HasMaxLength(20);

            HasMany(i => i.Items)
                .WithMany(i => i.Attributes)
                .Map(m =>
                {
                    m.ToTable("AttributesPerItems");
                    m.MapLeftKey("AttributeId");
                    m.MapRightKey("ItemId");
                });
        }
    }
}