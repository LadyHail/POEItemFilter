using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using POEItemFilter.Library.Enumerables;
using POEItemFilter.Models;

namespace POEItemFilter.ViewModels
{
    public class FilterViewModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        //[Required]
        //[Range(1, 50, ErrorMessage = "Filter requires at least 1 item.")]
        public List<ItemUser> Items { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public Classes? Dedicated { get; set; }

        public FilterViewModel()
        {
            Items = new List<ItemUser>();
        }
    }
}