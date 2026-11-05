using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Text;
using System.Collections.Generic;

namespace The25th.Models.ViewModels
{
    public class RoleManagementVM
    {
        public ApplicationUser ApplicationUser { get; set; }
        public IEnumerable<SelectListItem> RoleList { get; set; }
    }
}