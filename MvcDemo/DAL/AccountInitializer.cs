using MvcDemo.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcDemo.DAL
{
    public class AccountInitializer:DropCreateDatabaseIfModelChanges<AccountContext>
    {
        protected override void Seed(AccountContext context)
        {
            var sysUsers = new List<SysUser>
            {
                new SysUser { UserName="Tom", Email="Tom@sohu.com",PassWord="1" },
                new SysUser { UserName="Jerry", Email="Jerry@sohu.com",PassWord="2" }
            };
            sysUsers.ForEach(a => context.SysUser.Add(a));
            context.SaveChanges();

            var sysRoles = new List<SysRole>
            {
                new SysRole { RoleName="Administrators", RoleDesc="administrator have full...." },
                new SysRole { RoleName="General Users", RoleDesc="General Users can access...." }
            };
            sysRoles.ForEach(a => context.SysRole.Add(a));
            context.SaveChanges();

            var sysUserRoles = new List<SysUserRole>
            {
                new SysUserRole { SysUserId=1,SysRoleId=1 },
                new SysUserRole { SysUserId=1,SysRoleId=2 },
                new SysUserRole { SysUserId=2,SysRoleId=2 }
            };
            sysUserRoles.ForEach(a => context.SysUserRole.Add(a));
            context.SaveChanges();
               
        }
    }
}