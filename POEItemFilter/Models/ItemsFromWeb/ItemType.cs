using POEItemFilter.Library.Enumerables;

namespace POEItemFilter.Models.ItemsDB
{
    public class ItemType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public byte ItemBaseTypeId { get; set; }

        public Attributes? Attribute1 { get; set; }

        public Attributes? Attribute2 { get; set; }

        //public ItemAttribute Attribute1 { get; set; }

        //public byte? Attribute1Id { get; set; }

        //public ItemAttribute Attribute2 { get; set; }

        //public byte? Attribute2Id { get; set; }
    }
}