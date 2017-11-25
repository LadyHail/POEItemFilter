namespace POEItemFilter.ViewModels
{
    public class ItemUserViewModel
    {
        public int FilterId { get; set; }

        public int ItemId { get; set; }

        public byte? BaseTypes { get; set; }

        public int? Types { get; set; }

        public int? Items { get; set; }

        public string UserItem { get; set; }

        public int ItemLvlSelectSign { get; set; }

        public string ItemLvlSelect { get; set; }

        public string ItemLvlRangeSelect1 { get; set; }

        public string ItemLvlRangeSelect2 { get; set; }

        public int DropLevelSelectSign { get; set; }

        public string DropLevelSelect { get; set; }

        public int ItemQualitySelectSign { get; set; }

        public string ItemQualitySelect { get; set; }

        public int ItemRaritySelectSign { get; set; }

        public int ItemRaritySelect { get; set; }

        public int SocketsNumberSelectSign { get; set; }

        public string SocketsNumberSelect { get; set; }

        public int LinkedSocketsNumberSelectSign { get; set; }

        public string LinkedSocketsNumberSelect { get; set; }

        public int RedSocketsSelect { get; set; }

        public int GreenSocketsSelect { get; set; }

        public int BlueSocketsSelect { get; set; }

        public int WhiteSocketsSelect { get; set; }

        public int HeightSelectSign { get; set; }

        public string HeightSelect { get; set; }

        public int WidthSelectSign { get; set; }

        public string WidthSelect { get; set; }

        public bool IdentifiedSelect { get; set; }

        public bool CorruptedSelect { get; set; }

        public string BorderRedSelect { get; set; }

        public string BorderGreenSelect { get; set; }

        public string BorderBlueSelect { get; set; }

        public string BorderAlphaSelect { get; set; }

        public string TextRedSelect { get; set; }

        public string TextGreenSelect { get; set; }

        public string TextBlueSelect { get; set; }

        public string TextAlphaSelect { get; set; }

        public string BackRedSelect { get; set; }

        public string BackGreenSelect { get; set; }

        public string BackBlueSelect { get; set; }

        public string BackAlphaSelect { get; set; }

        public string FontSizeSelect { get; set; }

        public string PlayAlertSoundTypeSelect { get; set; }

        public string PlayAlertSoundVolumeSelect { get; set; }

        public bool Show { get; set; }
    }
}