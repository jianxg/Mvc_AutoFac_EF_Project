using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ypf.IBLL;

namespace MVCTest001.Controllers
{
    public class HomeController : Controller
    {
        public IUserBLL userBLL { get; set; }

        public string Index()
        {
            var v1 = userBLL.GetUserInfor();
            //没有实现IBLLSupport接口的类 不能使用AutoFac进行创建
            //var v2 = roleBLL.ShowRoleInfor();
            //没有 通过autoFac进行注册的普通类如何进行创建呢？
            IRoleBLL roleBLL = DependencyResolver.Current.GetService<IRoleBLL>();
            var v2 = roleBLL.ShowRoleInfor();

            return v1;
        }




        //public ActionResult Index()
        //{
        //    var v1 = userBLL.GetUserInfor();
        //    return View();
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}