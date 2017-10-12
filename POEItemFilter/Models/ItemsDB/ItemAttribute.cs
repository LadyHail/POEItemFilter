using System.Collections.Generic;

namespace POEItemFilter.Models.ItemsDB
{
    public class ItemAttribute
    {
        public byte Id { get; set; }

        public string Name { get; set; }

        public byte? BaseTypeId { get; set; }

        public virtual ICollection<ItemType> Types { get; set; }

        public virtual ICollection<ItemDB> Items { get; set; }

        public ItemAttribute()
        {
            Types = new HashSet<ItemType>();
            Items = new HashSet<ItemDB>();
        }
    }
}