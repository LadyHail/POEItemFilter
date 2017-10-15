using System.Collections.Generic;

namespace POEItemFilter.Models.Filters
{
    public class Filter
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<ItemUser> Items { get; set; }

        public string Description { get; set; }
    }
}