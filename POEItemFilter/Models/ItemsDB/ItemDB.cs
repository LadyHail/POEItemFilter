using POEItemFilter.Models.ItemsDB.Enum;

namespace POEItemFilter.Models.ItemsDB
{
    public class ItemDB
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte? Level { get; set; }

        public Types Type { get; set; }

        public Attributes? Attribute1 { get; set; }

        public Attributes? Attribute2 { get; set; }

        public BaseType BaseType { get; set; }
    }
}