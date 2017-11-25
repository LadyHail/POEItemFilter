using System.Linq;
using POEItemFilter.Library.Enumerables;
using POEItemFilter.Models;
using POEItemFilter.ViewModels;

namespace POEItemFilter.Library
{
    public static class ItemUserModelMap
    {
        public static ItemUser ViewModelToItemUser(ItemUserViewModel viewModel)
        {
            ApplicationDbContext _context = new ApplicationDbContext();
            ItemUser model = new ItemUser();

            model.Id = viewModel.ItemId;

            model.FilterId = viewModel.FilterId;

            model.BaseType =
                viewModel.Items != null ?
                _context.ItemsDB.SingleOrDefault(i => i.Id == viewModel.Items).Name :
                null;

            model.Class =
                viewModel.Types != null ?
                _context.Types.SingleOrDefault(i => i.Id == viewModel.Types).Name :
                null;

            model.MainCategory =
                viewModel.BaseTypes != null ?
                _context.BaseTypes.SingleOrDefault(i => i.Id == viewModel.BaseTypes).Name :
                null;

            model.Corrupted =
                viewModel.CorruptedSelect;

            model.DropLevel =
                InequalitySelector.Select(viewModel.DropLevelSelectSign) != null && viewModel.DropLevelSelect != null ?
                InequalitySelector.Select(viewModel.DropLevelSelectSign) + " " + viewModel.DropLevelSelect :
                null;

            model.Height =
                InequalitySelector.Select(viewModel.HeightSelectSign) != null && viewModel.HeightSelect != null ?
                InequalitySelector.Select(viewModel.HeightSelectSign) + " " + viewModel.HeightSelect :
                null;

            model.Identified =
                viewModel.IdentifiedSelect;

            model.ItemLevel =
                InequalitySelector.Select(viewModel.ItemLvlSelectSign) != null && viewModel.ItemLvlSelect != null ?
                InequalitySelector.Select(viewModel.ItemLvlSelectSign) + " " + viewModel.ItemLvlSelect :
                null;

            if (model.ItemLevel == null)
            {
                model.ItemLevel =
                    viewModel.ItemLvlRangeSelect1 != null && viewModel.ItemLvlRangeSelect2 != null ?
                    ">= " + viewModel.ItemLvlRangeSelect1 + "\n" + "<= " + viewModel.ItemLvlRangeSelect2 :
                    null;
            }

            model.LinkedSockets =
                InequalitySelector.Select(viewModel.LinkedSocketsNumberSelectSign) != null && viewModel.LinkedSocketsNumberSelect != null ?
                InequalitySelector.Select(viewModel.LinkedSocketsNumberSelectSign) + " " + viewModel.LinkedSocketsNumberSelect :
                null;

            model.PlayAlertSound =
                viewModel.PlayAlertSoundTypeSelect != null && viewModel.PlayAlertSoundVolumeSelect != null ?
                viewModel.PlayAlertSoundTypeSelect + " " + viewModel.PlayAlertSoundVolumeSelect :
                null;

            model.Quality =
                InequalitySelector.Select(viewModel.ItemQualitySelectSign) != null && viewModel.ItemQualitySelect != null ?
                InequalitySelector.Select(viewModel.ItemQualitySelectSign) + " " + viewModel.ItemQualitySelect :
                null;

            model.Rarity =
                InequalitySelector.Select(viewModel.ItemRaritySelectSign) != null && viewModel.ItemRaritySelect != 300 ?
                InequalitySelector.Select(viewModel.ItemRaritySelectSign) + " " + (Rarity)viewModel.ItemRaritySelect :
                null;

            model.SetBackgroundColor =
                viewModel.BackRedSelect != null && viewModel.BackGreenSelect != null && viewModel.BackBlueSelect != null ?
                viewModel.BackRedSelect + " " + viewModel.BackGreenSelect + " " + viewModel.BackBlueSelect + " " + viewModel.BackAlphaSelect :
                null;

            model.SetBorderColor =
                viewModel.BorderRedSelect != null && viewModel.BorderGreenSelect != null && viewModel.BorderBlueSelect != null ?
                viewModel.BorderRedSelect + " " + viewModel.BorderGreenSelect + " " + viewModel.BorderBlueSelect + " " + viewModel.BorderAlphaSelect :
                null;

            model.SetTextColor =
                viewModel.TextRedSelect != null && viewModel.TextGreenSelect != null && viewModel.TextBlueSelect != null ?
                viewModel.TextRedSelect + " " + viewModel.TextGreenSelect + " " + viewModel.TextBlueSelect + " " + viewModel.TextAlphaSelect :
                null;

            model.SetFontSize =
                viewModel.FontSizeSelect != null ?
                viewModel.FontSizeSelect :
                null;

            model.Show = viewModel.Show;

            model.Sockets =
                InequalitySelector.Select(viewModel.SocketsNumberSelectSign) != null && viewModel.SocketsNumberSelect != null ?
                InequalitySelector.Select(viewModel.SocketsNumberSelectSign) + " " + viewModel.SocketsNumberSelect :
                null;

            model.SocketsGroup =
                viewModel.RedSocketsSelect != 0 ?
                string.Concat(Enumerable.Repeat("R", viewModel.RedSocketsSelect)) :
                model.SocketsGroup;

            model.SocketsGroup =
                viewModel.GreenSocketsSelect != 0 ?
                model.SocketsGroup + string.Concat(Enumerable.Repeat("G", viewModel.GreenSocketsSelect)) :
                model.SocketsGroup;

            model.SocketsGroup =
                viewModel.BlueSocketsSelect != 0 ?
                model.SocketsGroup + string.Concat(Enumerable.Repeat("B", viewModel.BlueSocketsSelect)) :
                model.SocketsGroup;

            model.SocketsGroup =
                viewModel.WhiteSocketsSelect != 0 ?
                model.SocketsGroup + string.Concat(Enumerable.Repeat("W", viewModel.WhiteSocketsSelect)) :
                model.SocketsGroup;

            model.Width =
                InequalitySelector.Select(viewModel.WidthSelectSign) != null && viewModel.WidthSelect != null ?
                InequalitySelector.Select(viewModel.WidthSelectSign) + " " + viewModel.WidthSelect :
                null;

            return model;
        }
    }
}