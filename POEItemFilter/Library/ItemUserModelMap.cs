using System;
using System.Collections.Generic;
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

            if (viewModel.Items != null)
            {
                string baseType = _context.ItemsDB.SingleOrDefault(i => i.Id == viewModel.Items).Name;
                model.BaseType = baseType.Contains(" ") ? "\"" + baseType + "\"" : baseType;
            }
            else
            {
                model.BaseType = null;
            }

            model.UserBaseType =
                viewModel.UserItem != null && model.BaseType == null ?
                viewModel.UserItem.Contains(" ") ?
                "\"" + viewModel.UserItem + "\"" :
                viewModel.UserItem :
                null;

            model.MainCategory =
                viewModel.BaseTypes != null ?
                _context.BaseTypes.SingleOrDefault(i => i.Id == viewModel.BaseTypes).Name :
                null;

            if (viewModel.Types != null)
            {
                string type = _context.Types.SingleOrDefault(i => i.Id == viewModel.Types).Name;
                model.Class = type.Contains(" ") ? "\"" + type + "\"" : type;
            }
            else
            {
                model.Class = null;
            }

            if (model.MainCategory != null && model.Class == null)
            {
                var classesList = _context.Types.Where(i => i.BaseTypeId == viewModel.BaseTypes).Select(i => i.Name).ToList();
                foreach (var className in classesList)
                {
                    if (className.Contains(" "))
                    {
                        model.Class += "\"" + className + "\"";
                    }
                    else
                    {
                        model.Class += className;
                    }
                    model.Class += " ";
                }
                model.Class = model.Class.Trim();
            }

            model.Corrupted =
                Convert.ToBoolean(viewModel.CorruptedSelect);

            model.DropLevel =
                viewModel.DropLevelSelectSign != null && viewModel.DropLevelSelect != null ?
                InequalitySign.SelectSign(viewModel.DropLevelSelectSign.Value) + " " + viewModel.DropLevelSelect :
                null;

            model.Height =
                viewModel.HeightSelectSign != null && viewModel.HeightSelect != null ?
                InequalitySign.SelectSign(viewModel.HeightSelectSign.Value) + " " + viewModel.HeightSelect :
                null;

            model.Identified =
                Convert.ToBoolean(viewModel.IdentifiedSelect);

            model.ItemLevel =
                viewModel.ItemLvlSelectSign != null && viewModel.ItemLvlSelect != null ?
                InequalitySign.SelectSign(viewModel.ItemLvlSelectSign.Value) + " " + viewModel.ItemLvlSelect :
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
                InequalitySign.SelectSign(viewModel.LinkedSocketsNumberSelectSign.Value) + " " + viewModel.LinkedSocketsNumberSelect :
                null;

            model.PlayAlertSound =
                viewModel.PlayAlertSoundTypeSelect != null && viewModel.PlayAlertSoundVolumeSelect != null ?
                viewModel.PlayAlertSoundTypeSelect + " " + viewModel.PlayAlertSoundVolumeSelect :
                null;

            model.Quality =
                viewModel.ItemQualitySelectSign != null && viewModel.ItemQualitySelect != null ?
                InequalitySign.SelectSign(viewModel.ItemQualitySelectSign.Value) + " " + viewModel.ItemQualitySelect :
                null;

            model.Rarity =
                viewModel.ItemRaritySelectSign != null && viewModel.ItemRaritySelect != 300 ?
                InequalitySign.SelectSign(viewModel.ItemRaritySelectSign.Value) + " " + (Rarity)viewModel.ItemRaritySelect :
                null;

            model.SetBackgroundColor =
                viewModel.BackgroundColor != null ?
                viewModel.BackgroundColor + " " + viewModel.BackAlphaSelect :
                null;

            model.SetBorderColor =
                viewModel.BorderColor != null ?
                viewModel.BorderColor + " " + viewModel.BorderAlphaSelect :
                null;

            model.SetTextColor =
                viewModel.TextColor != null ?
                viewModel.TextColor + " " + viewModel.TextAlphaSelect :
                null;

            model.SetFontSize =
                viewModel.FontSizeSelect != null ?
                viewModel.FontSizeSelect :
                model.SetFontSize;

            model.Show =
                Convert.ToBoolean(viewModel.Show);

            model.Sockets =
                viewModel.SocketsNumberSelectSign != null && viewModel.SocketsNumberSelect != null ?
                InequalitySign.SelectSign(viewModel.SocketsNumberSelectSign.Value) + " " + viewModel.SocketsNumberSelect :
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
                InequalitySign.SelectSign(viewModel.WidthSelectSign.Value) + " " + viewModel.WidthSelect :
                null;

            return model;
        }

        public static ItemUserViewModel ItemUserToViewModel(ItemUser model)
        {
            ApplicationDbContext _context = new ApplicationDbContext();
            ItemUserViewModel viewModel = new ItemUserViewModel();

            viewModel.FilterId = model.FilterId;

            viewModel.ItemId = model.Id;

            viewModel.BaseTypes =
                model.MainCategory != null ?
                _context.BaseTypes.SingleOrDefault(i => i.Name == model.MainCategory).Id :
                (byte?)null;

            if (viewModel.BaseTypes != null)
            {
                var typesInDb = _context.Types.Where(i => i.BaseTypeId == viewModel.BaseTypes).Select(i => i).ToList();
                List<string> types = new List<string>();
                if (model.Class.Contains("\""))
                {
                    types = model.Class.Split('\"').ToList();
                }
                else
                {
                    types = model.Class.Split(' ').ToList();
                }

                List<string> checkList = new List<string>();
                types.RemoveAll(c => c == "" || c == " ");

                foreach (var itemType in types)
                {
                    if (itemType.StartsWith(" ") || itemType.EndsWith(" "))
                    {
                        checkList.AddRange(itemType.Split(' '));
                    }
                    else
                    {
                        checkList.Add(itemType);
                    }

                }
                checkList.RemoveAll(c => c == "" || c == " ");
                int matchCount = 0;
                foreach (var itemType in checkList)
                {
                    bool isMultipleClass = typesInDb.Any(i => i.Name == itemType);
                    matchCount = isMultipleClass ? matchCount + 1 : matchCount;
                }

                if (matchCount == typesInDb.Count)
                {
                    viewModel.Types = null;
                }
                else if (matchCount < typesInDb.Count)
                {
                    model.Class = model.Class.Replace("\"", "");
                    viewModel.Types =
                        model.Class != null ?
                        _context.Types.SingleOrDefault(i => i.Name == model.Class).Id :
                        (int?)null;
                }
                else
                {
                    viewModel.Types = null;
                }
            }
            else
            {
                if (model.Class != null)
                {
                    model.Class = model.Class.Replace("\"", "");
                    viewModel.Types = _context.Types.SingleOrDefault(i => i.Name == model.Class).Id;
                }
            }

            if (model.BaseType != null)
            {
                model.BaseType = model.BaseType.Replace("\"", "");
                viewModel.Items = _context.ItemsDB.SingleOrDefault(i => i.Name == model.BaseType).Id;
            }

            viewModel.UserItem =
                model.UserBaseType != null && model.BaseType == null ?
                model.UserBaseType :
                null;

            if (model.ItemLevel != null)
            {
                int indexOfSpace = model.ItemLevel.IndexOf(" ");
                if (model.ItemLevel.Contains("\n"))
                {
                    int indexOfLastSpace = model.ItemLevel.LastIndexOf(" ");

                    viewModel.ItemLvlRangeSelect1 = model.ItemLevel.Substring(indexOfSpace + 1, 2).Replace("\\", "");
                    viewModel.ItemLvlRangeSelect2 = model.ItemLevel.Substring(indexOfLastSpace + 1);
                }

                viewModel.ItemLvlSelectSign = InequalitySign.SelectInt(model.ItemLevel.Substring(0, indexOfSpace));
                viewModel.ItemLvlSelect = model.ItemLevel.Substring(indexOfSpace + 1);
            }

            if (model.DropLevel != null)
            {
                int indexOfSpace = model.DropLevel.IndexOf(" ");
                viewModel.DropLevelSelectSign = InequalitySign.SelectInt(model.DropLevel.Substring(0, indexOfSpace));
                viewModel.DropLevelSelect = model.DropLevel.Substring(indexOfSpace + 1);
            }

            if (model.Quality != null)
            {
                int indexOfSpace = model.Quality.IndexOf(" ");
                viewModel.ItemQualitySelectSign = InequalitySign.SelectInt(model.Quality.Substring(0, indexOfSpace));
                viewModel.ItemQualitySelect = model.Quality.Substring(indexOfSpace + 1);
            }

            if (model.Rarity != null)
            {
                int indexOfSpace = model.Rarity.IndexOf(" ");
                viewModel.ItemRaritySelectSign = InequalitySign.SelectInt(model.Rarity.Substring(0, indexOfSpace));
                viewModel.ItemRaritySelect = (int)Enum.Parse(typeof(Rarity), model.Rarity.Substring(indexOfSpace + 1));
            }

            if (model.Sockets != null)
            {
                int indexOfSpace = model.Sockets.IndexOf(" ");
                viewModel.SocketsNumberSelectSign = InequalitySign.SelectInt(model.Sockets.Substring(0, indexOfSpace));
                viewModel.SocketsNumberSelect = model.Sockets.Substring(indexOfSpace + 1);
            }

            if (model.LinkedSockets != null)
            {
                int indexOfSpace = model.LinkedSockets.IndexOf(" ");
                viewModel.LinkedSocketsNumberSelectSign = InequalitySign.SelectInt(model.LinkedSockets.Substring(0, indexOfSpace));
                viewModel.LinkedSocketsNumberSelect = model.LinkedSockets.Substring(indexOfSpace + 1);
            }

            if (model.SocketsGroup != null)
            {
                viewModel.RedSocketsSelect = model.SocketsGroup.Count(c => c == 'R');
                viewModel.GreenSocketsSelect = model.SocketsGroup.Count(c => c == 'G');
                viewModel.BlueSocketsSelect = model.SocketsGroup.Count(c => c == 'B');
                viewModel.WhiteSocketsSelect = model.SocketsGroup.Count(c => c == 'W');
            }

            if (model.Height != null)
            {
                int indexOfSpace = model.Height.IndexOf(" ");
                viewModel.HeightSelectSign = InequalitySign.SelectInt(model.Height.Substring(0, indexOfSpace));
                viewModel.HeightSelect = model.Height.Substring(indexOfSpace + 1);
            }

            if (model.Width != null)
            {
                int indexOfSpace = model.Width.IndexOf(" ");
                viewModel.WidthSelectSign = InequalitySign.SelectInt(model.Width.Substring(0, indexOfSpace));
                viewModel.WidthSelect = model.Width.Substring(indexOfSpace + 1);
            }

            if (model.SetBorderColor != null)
            {
                viewModel.BorderColor = model.SetBorderColor.Substring(0, model.SetBorderColor.LastIndexOf(' '));
                viewModel.BorderAlphaSelect = model.SetBorderColor.Substring(model.SetBorderColor.LastIndexOf(' ') + 1);
            }

            if (model.SetTextColor != null)
            {
                viewModel.TextColor = model.SetTextColor.Substring(0, model.SetTextColor.LastIndexOf(' '));
                viewModel.TextAlphaSelect = model.SetTextColor.Substring(model.SetTextColor.LastIndexOf(' ') + 1);
            }

            if (model.SetBackgroundColor != null)
            {
                viewModel.BackgroundColor = model.SetBackgroundColor.Substring(0, model.SetBackgroundColor.LastIndexOf(' '));
                viewModel.BackAlphaSelect = model.SetBackgroundColor.Substring(model.SetBackgroundColor.LastIndexOf(' ') + 1);
            }

            if (model.PlayAlertSound != null)
            {
                int indexOfSpace = model.PlayAlertSound.IndexOf(" ");
                viewModel.PlayAlertSoundTypeSelect = model.PlayAlertSound.Substring(0, indexOfSpace);
                viewModel.PlayAlertSoundVolumeSelect = model.PlayAlertSound.Substring(indexOfSpace + 1);
            }

            viewModel.FontSizeSelect = model.SetFontSize;

            viewModel.IdentifiedSelect = Convert.ToInt32(model.Identified);

            viewModel.CorruptedSelect = Convert.ToInt32(model.Corrupted);

            viewModel.Show = Convert.ToInt32(model.Show);

            return viewModel;
        }
    }
}