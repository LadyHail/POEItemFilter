using System.Collections.Generic;

namespace POEItemFilter.Models.ItemsDB
{
    public class ItemDB
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte? Level { get; set; }

        public ItemBaseType BaseType { get; set; }

        public byte BaseTypeId { get; set; }

        public ItemType Type { get; set; }

        public int TypeId { get; set; }

        public ICollection<ItemAttribute> Attributes { get; set; }

        public ItemDB()
        {
            Attributes = new HashSet<ItemAttribute>();
        }
    }
}