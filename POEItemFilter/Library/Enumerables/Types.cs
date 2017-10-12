using System.ComponentModel.DataAnnotations;

namespace POEItemFilter.Library.Enumerables
{
    public enum Types
    {
        [Display(Name = "Body Armour")]
        BodyArmour = 1,

        Boot = 2,
        Glove = 3,
        Helmet = 4,
        Shield = 5,

        Bow = 6,
        Claw = 7,
        Dagger = 8,

        [Display(Name = "One Hand Axe")]
        OneHandAxe = 9,

        [Display(Name = "One Hand Mace")]
        OneHandMace = 10,

        [Display(Name = "One Hand Sword")]
        OneHandSword = 11,

        Sceptre = 12,
        Stave = 13,

        [Display(Name = "Two Hand Axe")]
        TwoHandAxe = 14,

        [Display(Name = "Two Hand Mace")]
        TwoHandMace = 15,

        [Display(Name = "Two Hand Sword")]
        TwoHandSword = 16,

        Wand = 17,

        [Display(Name = "Thrusting One Hand Sword")]
        ThrustingOneHandSword = 18,

        Amulet = 19,
        Belt = 20,
        Ring = 21,
        Quiver = 22,

        Currency = 23,

        [Display(Name = "Support Skill Gem")]
        SupportSkillGem = 24,

        [Display(Name = "Active Skill Gem")]
        ActiveSkillGem = 25,

        Map = 26,

        [Display(Name = "Utility Flask")]
        UtilityFlask = 27,

        [Display(Name = "Life Flask")]
        LifeFlask = 28,

        [Display(Name = "Mana Flask")]
        ManaFlask = 29,

        [Display(Name = "Hybrid Flask")]
        HybridFlask = 30,

        Essence = 31,

        [Display(Name = "Labyrinth Trinket")]
        LabyrinthTrinket = 32,

        [Display(Name = "Labyrinth Item")]
        LabyrinthItem = 33,

        [Display(Name = "Fishing Rod")]
        FishingRod = 34,

        Piece = 35,

        [Display(Name = "Divination Card")]
        DivinationCard = 36
    }
}