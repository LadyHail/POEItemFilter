using System;
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

            model.Id =
                viewModel.ItemId != null ?
                viewModel.ItemId.Value :
                0;

            model.FilterId =
                viewModel.FilterId != null ?
                viewModel.FilterId.Value :
                0;

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
                Convert.ToBoolean(viewModel.CorruptedSelect);

            model.DropLevel =
                viewModel.DropLevelSelectSign != null && viewModel.DropLevelSelect != null ?
                InequalitySelector.Select(viewModel.DropLevelSelectSign.Value) + " " + viewModel.DropLevelSelect :
                null;

            model.Height =
                viewModel.HeightSelectSign != null && viewModel.HeightSelect != null ?
                InequalitySelector.Select(viewModel.HeightSelectSign.Value) + " " + viewModel.HeightSelect :
                null;

            model.Identified =
                Convert.ToBoolean(viewModel.IdentifiedSelect);

            model.ItemLevel =
                viewModel.ItemLvlSelectSign != null && viewModel.ItemLvlSelect != null ?
                InequalitySelector.Select(viewModel.ItemLvlSelectSign.Value) + " " + viewModel.ItemLvlSelect :
                null;

            if (model.ItemLevel == null)
            {
                model.ItemLevel =
                    viewModel.ItemLvlRangeSelect1 != null && viewModel.ItemLvlRangeSelect2 != null ?
                    ">= " + viewModel.ItemLvlRangeSelect1 + "\n" + "<= " + viewModel.ItemLvlRangeSelect2 :
                    null;
            }

            model.LinkedSockets =
                viewModel.LinkedSocketsNumberSelectSign != null && viewModel.LinkedSocketsNumberSelect != null ?
                InequalitySelector.Select(viewModel.LinkedSocketsNumberSelectSign.Value) + " " + viewModel.LinkedSocketsNumberSelect :
                null;

            model.PlayAlertSound =
                viewModel.PlayAlertSoundTypeSelect != null && viewModel.PlayAlertSoundVolumeSelect != null ?
                viewModel.PlayAlertSoundTypeSelect + " " + viewModel.PlayAlertSoundVolumeSelect :
                null;

            model.Quality =
                viewModel.ItemQualitySelectSign != null && viewModel.ItemQualitySelect != null ?
                InequalitySelector.Select(viewModel.ItemQualitySelectSign.Value) + " " + viewModel.ItemQualitySelect :
                null;

            model.Rarity =
                viewModel.ItemRaritySelectSign != null && viewModel.ItemRaritySelect != 300 ?
                InequalitySelector.Select(viewModel.ItemRaritySelectSign.Value) + " " + (Rarity)viewModel.ItemRaritySelect :
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
                model.SetFontSize;

            model.Show =
                Convert.ToBoolean(viewModel.Show);

            model.Sockets =
                viewModel.SocketsNumberSelectSign != null && viewModel.SocketsNumberSelect != null ?
                InequalitySelector.Select(viewModel.SocketsNumberSelectSign.Value) + " " + viewModel.SocketsNumberSelect :
                null;

            model.SocketsGroup =
                viewModel.RedSocketsSelect != null ?
                string.Concat(Enumerable.Repeat("R", viewModel.RedSocketsSelect.Value)) :
                model.SocketsGroup;

            model.SocketsGroup =
                viewModel.GreenSocketsSelect != null ?
                model.SocketsGroup + string.Concat(Enumerable.Repeat("G", viewModel.GreenSocketsSelect.Value)) :
                model.SocketsGroup;

            model.SocketsGroup =
                viewModel.BlueSocketsSelect != null ?
                model.SocketsGroup + string.Concat(Enumerable.Repeat("B", viewModel.BlueSocketsSelect.Value)) :
                model.SocketsGroup;

            model.SocketsGroup =
                viewModel.WhiteSocketsSelect != null ?
                model.SocketsGroup + string.Concat(Enumerable.Repeat("W", viewModel.WhiteSocketsSelect.Value)) :
                model.SocketsGroup;

            model.Width =
                viewModel.WidthSelectSign != null && viewModel.WidthSelect != null ?
                InequalitySelector.Select(viewModel.WidthSelectSign.Value) + " " + viewModel.WidthSelect :
                null;

            return model;
        }
    }
}