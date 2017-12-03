using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using POEItemFilter.Library.Enumerables;

namespace POEItemFilter.Models.Filters
{
    public class Filter
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        //[Required]
        //[Range(1, 50, ErrorMessage = "Filter requires at least 1 item.")]
        public ICollection<ItemUser> Items { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public string UserId { get; set; } //nvarchar(128)

        public virtual ApplicationUser User { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime EditDate { get; set; }

        public Classes? Dedicated { get; set; }

        public Filter()
        {
            Items = new HashSet<ItemUser>();
        }
    }
}