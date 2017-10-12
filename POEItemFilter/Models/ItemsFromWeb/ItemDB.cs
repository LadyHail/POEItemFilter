using System.ComponentModel.DataAnnotations;
using POEItemFilter.Library.Enumerables;

namespace POEItemFilter.Models.ItemsDB
{
    public class ItemDB
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte? Level { get; set; }

        public Types Type { get; set; }

        [Display(Name = "Attribute")]
        public Attributes? Attribute1 { get; set; }

        [Display(Name = "Attribute")]
        public Attributes? Attribute2 { get; set; }

        [Display(Name = "Base Type")]
        public BaseTypes BaseType { get; set; }
    }
}