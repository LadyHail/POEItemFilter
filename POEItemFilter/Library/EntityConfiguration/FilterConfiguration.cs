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

            Property(f => f.Description)
                .IsOptional()
                .HasMaxLength(500);

            Property(f => f.CreateDate)
                .IsRequired();

            Property(f => f.EditDate)
                .IsRequired();

            HasRequired(f => f.User)
                .WithMany(f => f.Filter)
                .HasForeignKey(f => f.UserId)
                .WillCascadeOnDelete(false);

            //HasMany(f => f.Items)
            //    .WithRequired(f => f.Filter)
            //    .HasForeignKey(f => f.FilterId)
            //    .WillCascadeOnDelete(false);
        }
    }
}