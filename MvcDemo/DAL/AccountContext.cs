using MvcDemo.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace MvcDemo.DAL
{
    public class AccountContext:DbContext
    {
        public AccountContext()
            :base("AccountContext")
        {

        }

        public DbSet<SysUser> SysUser { get; set; }

        public DbSet<SysRole> SysRole { get; set; }

        public DbSet<SysUserRole> SysUserRole { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}