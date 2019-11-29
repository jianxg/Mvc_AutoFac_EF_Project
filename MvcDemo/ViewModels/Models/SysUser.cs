using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDemo.ViewModels.Models
{
    public class SysUser
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string PassWord { get; set; }

        public virtual ICollection<SysUserRole> SysUserRoles { get; set; }
    }
}