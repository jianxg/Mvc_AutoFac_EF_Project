using MvcDemo.DAL;
using MvcDemo.ViewModels.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDemo.Controllers
{
    public class AccountController : Controller
    {
        private AccountContext db = new AccountContext();

        // GET: Account
        public ActionResult Index()
        {
            return View(db.SysUser);
        }

        /// <summary>
        /// 涉及到需要接受客户端窗口数据的时候，创建一个用于接收HTTP Get请求的Action, 用于显示界面, 提供给用户填写数据;
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            ViewBag.LoginState = "登录前...";
            return View();
        }

        /// <summary>
        /// 应用[HttpPost]属性，用于接收用户发来的数据，完成对应的功能。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(FormCollection fc)
        {
            string email = fc["InputEmail3"];
            string password = fc["InputPassword3"];
            var user = db.SysUser.Where(b => b.Email == email & b.PassWord == password);
            if (user.Count()>0)
            {
                ViewBag.LoginState = email + ":登录后...";
            }
            else
            {
                ViewBag.LoginState = email + ":用户不存在...";
            }
            return View();
        }

        public ActionResult Register()
        {
            ViewBag.RegisterState = "注册前...";
            return View();
        }

        /// <summary>
        /// 应用[HttpPost]属性，用于接收用户发来的数据，完成对应的功能。
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(FormCollection fc)
        {
            string email = fc["InputEmail3"];
            string password = fc["InputPassword3"];
            ViewBag.RegisterState = email + ":注册后...";
            return View();
        }

        public string GetSysUsers()
        {
            string password = string.Empty;
            var users = from a in db.SysUser
                        where a.UserName=="Tom"
                        select a.PassWord;
            foreach (var item in users)
            {
                password += item + "/";
            }
            return password;
        }

        /// <summary>
        /// 更新对象数据
        /// </summary>
        /// <returns></returns>
        public ActionResult EFUpdateDemo()
        {
            //第一步，找到对象
            var sysUser = db.SysUser.FirstOrDefault(u => u.UserName == "Tom");
            //更新对象数据
            if (sysUser!=null)
            {
                sysUser.UserName = "TomCat";
            }
            //保存修改
            db.SaveChanges();
            return View();
        }

        /// <summary>
        /// 新增一个实体对象
        /// </summary>
        /// <returns></returns>
        public ActionResult EFAddDemo()
        {
            //创建一个新的实体对象
            var newSysUser = new SysUser()
            {
                UserName = "Scott",
                PassWord="tiger",
                Email="Scott@sohu.com"
            };
            //增加
            db.SysUser.Add(newSysUser);
            db.SaveChanges();
            return View();
        }

        /// <summary>
        /// 删除一个实体对象
        /// </summary>
        /// <returns></returns>
        public ActionResult EFDeleteDemo()
        {
            //查找实体对象
            var sysUser = db.SysUser.FirstOrDefault(a => a.UserName == "Scott");
            //删除
            if (sysUser != null)
            {
                db.SysUser.Remove(sysUser);
            }
            db.SaveChanges();
            return View();
        }

        /// <summary>
        /// 查询用户及角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Details(int id)
        {
            SysUser sysUser = db.SysUser.Find(id);
            return View(sysUser);
        }

        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(SysUser sysUser)
        {
            db.SysUser.Add(sysUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 修改用户=--获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            SysUser sysUser = db.SysUser.Find(id);
            return View(sysUser);
        }

        /// <summary>
        /// 修改用户-修改
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(SysUser sysUser)
        {
            db.Entry(sysUser).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int id)
        {
            SysUser sysUser = db.SysUser.Find(id);
            return View(sysUser);
        }

        /// <summary>
        /// 确认删除用户
        /// </summary>
        /// <returns></returns>
        [HttpPost,ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            SysUser sysUser = db.SysUser.Find(id);
            db.SysUser.Remove(sysUser);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}