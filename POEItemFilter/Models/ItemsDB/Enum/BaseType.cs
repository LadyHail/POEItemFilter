using System.ComponentModel.DataAnnotations;

namespace POEItemFilter.Models.ItemsDB.Enum
{
    public enum BaseType
    {
        Armour = 1,
        Weapon = 2,
        Accessory = 3,
        Currency = 4,

        [Display(Name = "Skill Gem")]
        Skill_Gem = 5,

        Map = 6,
        Flask = 7,
        Essence = 8,

        [Display(Name = "Labyrinth Item")]
        Labyrinth_Item = 9,

        [Display(Name = "Fishing Rod")]
        Fishing_Rod = 10,

        Piece = 11,

        [Display(Name = "Divination Card")]
        Divination_Card = 12
    }
}