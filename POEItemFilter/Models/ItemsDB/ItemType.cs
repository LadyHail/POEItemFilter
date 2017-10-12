using System.Collections.Generic;

namespace POEItemFilter.Models.ItemsDB
{
    public class ItemType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte BaseTypeId { get; set; }

        public virtual ICollection<ItemAttribute> Attributes { get; set; }

        public virtual ICollection<ItemDB> Items { get; set; }

        public ItemType()
        {
            Attributes = new HashSet<ItemAttribute>();
            Items = new HashSet<ItemDB>();
        }
    }
}