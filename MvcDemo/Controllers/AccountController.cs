using MvcDemo.DAL;
using System;
using System.Collections.Generic;
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
            return View();
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


    }
}