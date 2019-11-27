using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ypf.IBLL;

namespace MVCTest001
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            /**************************全局变量中注册AutoFac****************************************/
            //1.创建容器
            var builder = new ContainerBuilder();
            //2.把当前程序中所有controller全部注册进来
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();
            //3.把Ypf.BLL中所有类注册给它的全部实现接口，并且把类中的属性也注册
            string dllName = ConfigurationManager.AppSettings["DLLName"].ToString();
            Assembly ass = Assembly.Load(dllName);

            //builder.RegisterAssemblyTypes(ass).Where(t => !t.IsAbstract).AsImplementedInterfaces().PropertiesAutowired();

            //小技巧:在Ypf.BLL层还有很多类，而我们不需要AutoFac帮我们全部重新创建，这时候我们可以在IYpf.BLL层中申明一个IBLLSupport接口，让需要AutoFac创建的类都去实现这个接口，然后注册的话改为这种写法:
            builder.RegisterAssemblyTypes(ass).Where(t => !t.IsAbstract && typeof(IBLLSupport).IsAssignableFrom(t)).AsImplementedInterfaces().PropertiesAutowired();
 
            var container = builder.Build();

            //4.下面这句话表示当我们使用MVC创建controller对象的时候，都是由AutoFac为我们创建controller对象的
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
