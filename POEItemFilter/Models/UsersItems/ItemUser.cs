using System;
using System.Reflection;
using POEItemFilter.Models.Filters;

namespace POEItemFilter.Models
{
    public class ItemUser
    {
        //PlayAlertSound 7 300
        //visual: czwarty parametr Alpha [0-255]
        public int Id { get; set; }

        public int FilterId { get; set; }

        public Filter Filter { get; set; }

        /// <summary>
        /// eg. Armour.
        /// </summary>
        public string MainCategory { get; set; }

        /// <summary>
        /// eg. Boot.
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// One of three: strength, dexterity, intelligence.
        /// </summary>
        public string Attribute1 { get; set; }

        /// <summary>
        /// One of three: strength, dexterity, intelligence.
        /// </summary>
        public string Attribute2 { get; set; }

        /// <summary>
        /// eg. Iron Hat.
        /// </summary>
        public string BaseType { get; set; }

        /// <summary>
        /// Minimum level item starts to drop.
        /// </summary>
        public string DropLevel { get; set; }

        /// <summary>
        /// One of four: normal, magic, rare, unique.
        /// </summary>
        public string Rarity { get; set; }

        /// <summary>
        /// Value range: 0-20.
        /// </summary>
        public string Quality { get; set; }

        /// <summary>
        /// Value range: 0-100.
        /// </summary>
        public string ItemLevel { get; set; }

        /// <summary>
        /// Value range: 0-6.
        /// </summary>
        public string Sockets { get; set; }

        /// <summary>
        /// Number of red/green/blue sockets. Values: 0-6.
        /// </summary>
        public string SocketsGroup { get; set; }

        /// <summary>
        /// Number of linked sockets. Values: 0, 2-6.
        /// </summary>
        public string LinkedSockets { get; set; }

        /// <summary>
        /// Value range: 1-4. 
        /// </summary>
        public string Height { get; set; }

        /// <summary>
        /// Value range: 1-2.
        /// </summary>
        public string Width { get; set; }

        /// <summary>
        /// RGB format. Value range: 0-255.
        /// </summary>
        public string SetBorderColor { get; set; }

        /// <summary>
        /// RGB format. Value range: 0-255.
        /// </summary>
        public string SetTextColor { get; set; }

        /// <summary>
        /// RGB format. Value range: 0-255.
        /// </summary>
        public string SetBackgroundColor { get; set; }

        /// <summary>
        /// Value range: 18-45. Default: 32.
        /// </summary>
        public string SetFontSize { get; set; } = "32";

        /// <summary>
        /// Default: false.
        /// </summary>
        public bool Identified { get; set; } = false;

        /// <summary>
        /// Default: false.
        /// </summary>
        public bool Corrupted { get; set; } = false;

        /// <summary>
        /// Default: true.
        /// </summary>
        public bool Show { get; set; } = true;

        public override string ToString()
        {
            string output = "";
            Type type = GetType();
            PropertyInfo[] properties = type.GetProperties();

            foreach (PropertyInfo property in properties)
            {
                if (property.GetValue(this, null) != null)
                {
                    if (property.Name != "Id" && property.Name != "FilterId" && property.Name != "Filter")
                    {
                        output += (property.Name + ": " + property.GetValue(this, null).ToString()).TrimEnd() + " | ";
                    }
                }
            }
            return output;
        }
    }
}