using System.Collections.Generic;
using POEItemFilter.Models;
using POEItemFilter.Models.ItemsDB;

namespace POEItemFilter.ViewModels
{
    public class NewItemViewModel
    {
        public IEnumerable<ItemDB> Items { get; set; }

        public IEnumerable<ItemBaseType> BaseTypes { get; set; }

        public IEnumerable<ItemType> Types { get; set; }

        public IEnumerable<ItemAttribute> Attributes { get; set; }

        public ItemUser ItemUser { get; set; }
    }
}