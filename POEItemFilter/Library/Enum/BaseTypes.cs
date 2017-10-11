using System.ComponentModel.DataAnnotations;

namespace POEItemFilter.Models.ItemsDB.Enum
{
    public enum BaseTypes
    {
        Armour = 1,
        Weapon = 2,
        Accessory = 3,
        Currency = 4,

        [Display(Name = "Skill Gem")]
        SkillGem = 5,

        Map = 6,
        Flask = 7,
        Essence = 8,

        [Display(Name = "Labyrinth Item")]
        LabyrinthItem = 9,

        [Display(Name = "Fishing Rod")]
        FishingRod = 10,

        Piece = 11,

        [Display(Name = "Divination Card")]
        DivinationCard = 12
    }
}