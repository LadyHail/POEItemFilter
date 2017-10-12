using System.Collections.Generic;

namespace POEItemFilter.Models.ItemsDB
{
    public class ItemBaseType
    {
        public byte Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ItemType> Types { get; set; }

        public virtual ICollection<ItemDB> Items { get; set; }

        public virtual ICollection<ItemAttribute> Attributes { get; set; }

        public ItemBaseType()
        {
            Types = new HashSet<ItemType>();
            Items = new HashSet<ItemDB>();
            Attributes = new HashSet<ItemAttribute>();
        }
    }
}

