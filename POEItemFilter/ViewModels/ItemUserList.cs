using System.Collections.Generic;
using POEItemFilter.Models;

namespace POEItemFilter.ViewModels
{
    public class ItemUserList
    {
        public List<ItemUser> UsersItems { get; set; }

        public ItemUserList()
        {
            UsersItems = new List<ItemUser>();
        }
    }
}