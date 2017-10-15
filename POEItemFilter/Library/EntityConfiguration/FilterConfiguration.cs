using System.Data.Entity.ModelConfiguration;
using POEItemFilter.Models.Filters;

namespace POEItemFilter.Library.EntityConfiguration
{
    public class FilterConfiguration : EntityTypeConfiguration<Filter>
    {
        public FilterConfiguration()
        {
            HasKey(f => f.Id);

            Property(f => f.Name)
                .HasMaxLength(100)
                .IsRequired();

            HasMany(f => f.Items);

            Property(f => f.Description)
                .IsOptional()
                .HasMaxLength(500);
        }
    }
}