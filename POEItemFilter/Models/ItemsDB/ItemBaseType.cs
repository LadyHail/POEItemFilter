using System.Collections.Generic;

namespace POEItemFilter.Models.ItemsDB
{
    public class ItemBaseType
    {
        public byte Id { get; set; }

        public string Name { get; set; }

        public IList<ItemType> Types { get; set; }
    }
}