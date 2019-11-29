using MvcDemo.DAL;
using MvcDemo.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDemo.Repositories
{
    public class SysUserRepository:ISysUserRepository
    {
        protected AccountContext db = new AccountContext();

        //查询所有用户
        public IQueryable<SysUser> SelectAll()
        {
           return db.SysUser;
        }

        //通过用户名查询用户
        public SysUser SelectByName(string userName)
        {
            return db.SysUser.FirstOrDefault(u => u.UserName == userName);
        }

        //添加用户
        public void Add(SysUser sysUser)
        {
            db.SysUser.Add(sysUser);
            db.SaveChanges();
        }

        //删除用户
        public bool Delete(int id)

        {
            var delSysUser = db.SysUser.FirstOrDefault(u => u.Id == id);
            if (delSysUser != null)
            {
                db.SysUser.Remove(delSysUser);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}