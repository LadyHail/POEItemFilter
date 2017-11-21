using System;
using System.IO;
using System.Reflection;
using POEItemFilter.Models.Filters;
using POEItemFilter.ViewModels;

namespace POEItemFilter.Library
{
    public static class GenerateFilter
    {
        public static StreamWriter CreateTempFile(string filterName)
        {
            Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + @"\Filters");
            FileInfo filterFile = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + @"\Filters\" + filterName + ".filter");
            StreamWriter filterText = filterFile.AppendText();
            return filterText;
        }

        public static void SaveItems(StreamWriter filterText, ItemUserList itemsList, Filter newFilter)
        {
            foreach (var item in itemsList.UsersItems)
            {
                newFilter.Items.Add(item);
                if (item.Show == true)
                {
                    filterText.WriteLine("Show");
                }
                else
                {
                    filterText.WriteLine("Hide");
                }

                string output = "";
                Type type = item.GetType();
                PropertyInfo[] properties = type.GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    if (property.GetValue(item, null) != null)
                    {
                        if (property.Name != "Id" &&
                            property.Name != "Show" &&
                            property.Name != "Attribute1" &&
                            property.Name != "Attribute2")
                        {
                            if (property.Name != "SetFontSize" && property.GetValue(item, null).ToString() != "32")
                            {
                                output = "    " + (property.Name + " " + property.GetValue(item, null).ToString()).Trim();
                                filterText.WriteLine(output);
                            }
                        }
                    }
                }
            }
            filterText.WriteLine("Hide");
        }

        public static void DeleteTempFile()
        {

        }
    }
}