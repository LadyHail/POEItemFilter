using System.Collections.Generic;
using POEItemFilter.Models;
using POEItemFilter.Models.Filters;

namespace POEItemFilter.ViewModels
{
    public class EditFilterViewModel
    {
        public Filter Filter { get; set; }

        public List<ItemUser> ItemsList { get; set; }

        public EditFilterViewModel()
        {
            ItemsList = new List<ItemUser>();
        }
    }
}