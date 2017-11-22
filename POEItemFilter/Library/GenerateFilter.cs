using System;
using System.IO;
using POEItemFilter.Models.Filters;
using POEItemFilter.ViewModels;

namespace POEItemFilter.Library
{
    public static class GenerateFilter
    {
        /// <summary>
        /// Create temporary file. The place where items will be saved. The file is deleted after download.
        /// </summary>
        /// <param name="filterName">Name of the filter file.</param>
        /// <returns>Destination to save filter items.</returns>
        public static StreamWriter CreateTempFile(string filterName)
        {
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Filters");
            FileInfo filterFile = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"\Filters\" + filterName + ".filter");
            StreamWriter filterText = filterFile.AppendText();
            return filterText;
        }

        /// <summary>
        /// Save each item to the file. Set priority to properties.
        /// </summary>
        /// <param name="filterText">Save destination</param>
        /// <param name="itemsList">List of items to save</param>
        /// <param name="newFilter">Model of filter created by user</param>
        public static void SaveItems(StreamWriter filterText, ItemUserList itemsList, Filter newFilter)
        {
            foreach (var item in itemsList.UsersItems)
            {
                string visibility = item.Show == true ? "Show" : "Hide";
                filterText.WriteLine(visibility);

                string output = "";

                output += item.Identified == true ? "    Identified True\n" : "";

                output += item.Quality != null ? "    Quality " + SetProperty(item.Quality) : "";
                output += item.DropLevel != null ? "    DropLevel " + SetProperty(item.DropLevel) : "";
                output += item.Height != null ? "    Height " + SetProperty(item.Height) : "";
                output += item.Width != null ? "    Width " + SetProperty(item.Width) : "";
                output += item.LinkedSockets != null ? "    LinkedSockets " + SetProperty(item.LinkedSockets) : "";
                output += item.Sockets != null ? "    Sockets " + SetProperty(item.Sockets) : "";
                output += item.SocketsGroup != null ? "    SocketsGroup " + SetProperty(item.SocketsGroup) : "";

                output += item.Class != null ? "    Class " + SetProperty(item.Class) : "";
                output += item.BaseType != null ? "    BaseType " + SetProperty(item.BaseType) : "";

                output += item.Rarity != null ? "    Rarity " + item.Rarity + "\n" : "";
                output += item.ItemLevel != null ? "    ItemLevel " + SetProperty(item.ItemLevel) : "";

                output += item.SetFontSize != "32" ? "    SetFontSize " + SetProperty(item.SetFontSize) : "";
                output += item.SetTextColor != null ? "    SetTextColor " + SetProperty(item.SetTextColor) : "";
                output += item.SetBackgroundColor != null ? "    SetBackgroundColor " + SetProperty(item.SetBackgroundColor) : "";
                output += item.SetBorderColor != null ? "    SetBorderColor " + SetProperty(item.SetBorderColor) : "";
                //output += item.PlayAlertSound != null ? "    PlayAlertSound " + SetProperty(item.PlayAlertSound) : "";

                filterText.WriteLine(output);
            }
            filterText.Write("Hide");
            filterText.Close();
        }

        /// <summary>
        /// This short method sets correct way to save item value.
        /// </summary>
        /// <param name="propertyValue">The value to save</param>
        /// <returns>String of item value</returns>
        private static string SetProperty(string propertyValue)
        {
            string output = "";
            int lastIndex = propertyValue.Length - 1;
            bool isNumber = int.TryParse(propertyValue[lastIndex].ToString(), out int noNumber);
            output = propertyValue.Contains(" ") && !isNumber ? "\"" + propertyValue + "\"" + "\n" : propertyValue + "\n";
            return output;
        }

        /// <summary>
        /// Delete temporary filter file.
        /// </summary>
        /// <param name="fileName">Name of the filter.</param>
        public static void DeleteTempFile(string fileName)
        {
            string fullPath = AppDomain.CurrentDomain.BaseDirectory + @"\Filters\" + fileName + ".filter";
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }
    }
}