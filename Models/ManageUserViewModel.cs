using System.Collections.Generic;

namespace ShareALittle.Models
{
    public class ManageUsersViewModel
    {
        public ApplicationUser[] Administrators { get; set; }

        public ApplicationUser[] Everyone { get; set;}
    }
}