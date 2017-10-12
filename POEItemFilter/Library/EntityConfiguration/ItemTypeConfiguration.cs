using System.Data.Entity.ModelConfiguration;
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

            Property(i => i.BaseTypeId)
                .IsRequired();

            HasMany(i => i.Attributes)
                .WithMany(i => i.Types)
                .Map(m =>
                {
                    m.ToTable("AttributesPerTypes");
                    m.MapLeftKey("TypeId");
                    m.MapRightKey("AttributeId");
                });

            HasMany(i => i.Items)
                .WithRequired(i => i.Type)
                .HasForeignKey(i => i.TypeId)
                .WillCascadeOnDelete(false);
        }
    }
}