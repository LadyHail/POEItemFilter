using POEItemFilter.Models.UsersItems;

namespace POEItemFilter.Models
{
    public class ItemUser
    {
        public int Id { get; set; }

        public string MainCategory { get; set; }

        public string Class { get; set; }

        public string Attribute1 { get; set; }

        public string Attribute2 { get; set; }

        public string BaseType { get; set; }

        public string Rarity { get; set; }

        public string Quality { get; set; }

        public string Sockets { get; set; }

        public string SocketsGroup { get; set; }

        public string LinkedSockets { get; set; }

        public string ItemLevel { get; set; }

        public string DropLevel { get; set; }

        public string Height { get; set; }

        public string Width { get; set; }

        public bool Identified { get; set; } = false;

        public bool Corrupted { get; set; } = false;

        public Visual Visual { get; set; }

        public bool Show { get; set; } = true;
    }
}