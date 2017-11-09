using System.Collections.Generic;
using POEItemFilter.Models.ItemsDB;

namespace POEItemFilter.ViewModels
{
    public class NewItemViewModel
    {
        public IEnumerable<ItemDB> Items { get; set; }

        public IEnumerable<ItemBaseType> BaseTypes { get; set; }

        public IEnumerable<ItemType> Types { get; set; }

        public IEnumerable<ItemAttribute> Attributes { get; set; }

        public int LastItemId { get; set; }

        public int LastBaseTypeId { get; set; }

        public int LastTypeId { get; set; }

        public int LastAttributeId { get; set; }
    }
}