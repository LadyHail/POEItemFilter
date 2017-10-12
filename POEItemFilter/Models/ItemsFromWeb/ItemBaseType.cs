using System.Collections.Generic;
using POEItemFilter.Library.Enumerables;

namespace POEItemFilter.Models.ItemsDB
{
    public class ItemBaseType
    {
        public byte Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<ItemType> Types { get; set; }

        public Attributes? Attribute1 { get; set; }

        public Attributes? Attribute2 { get; set; }

        //public ItemAttribute Attribute1 { get; set; }

        //public byte? Attribute1Id { get; set; }

        //public ItemAttribute Attribute2 { get; set; }

        //public byte? Attribute2Id { get; set; }

        public ItemBaseType()
        {
            Types = new HashSet<ItemType>();
        }
    }
}