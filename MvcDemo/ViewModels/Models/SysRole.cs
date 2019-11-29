using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDemo.ViewModels.Models
{
    public class SysRole
    {
        public int Id { get; set; }

        public string RoleName { get; set; }

        public string RoleDesc { get; set; }

        public ICollection<SysUserRole> SysUserRoles { get; set; }

    }
}