using ATeamFinalProject.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ATeamFinalProject.ViewModels
{
    public class RoleAddUserRoleViewModel
    {
        public ApplicationUser User { get; set; }
        public string Role { get; set; }
        public SelectList RoleList { get; set; }
    }
}
