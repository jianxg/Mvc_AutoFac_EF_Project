using Autofac;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ypf.BLL;
using Ypf.IBLL;

namespace ClickTest002
{
    class Program
    {
        static void Main(string[] args)
        {
            //引入ioc框架之前的几种写法

            Console.WriteLine("最原始的写法直接new(需添加对BLL层的引用)：");
            UserBLL bll = new UserBLL();
            var result = bll.GetUserInfor();
            Console.WriteLine(result);

            Console.WriteLine("面向接口的写法(仍需添加对BLL层的引用)：");
            IUserBLL ibll = new UserBLL();
            var result1 = ibll.GetUserInfor();
            Console.WriteLine(result1);

            Console.WriteLine("接口+反射(只需将BLL层的程序集拷贝进来)：");
            Assembly ass = Assembly.Load("Ypf.BLL");
            Type type = ass.GetType("Ypf.BLL.UserBLL");
            //调用默认的无参构造函数进行初始化对象
            object myUserBll = Activator.CreateInstance(type);
            IUserBLL ibll1 = (IUserBLL)myUserBll;
            var result2 = ibll1.GetUserInfor();
            Console.WriteLine(result2);

            Console.WriteLine("手写IOC(反射+简单工厂+配置文件)【需将BLL层的程序集拷贝进来】:");
            IUserBLL ibll2 = SimpleFactory.CreateInstance();
            var result3 = ibll2.GetUserInfor();
            Console.WriteLine(result3);

            // AutoFac常见用法总结
            //基本用法(评价：这种用法单纯的是为了介绍AutoFac中的几个方法，仅此而已，在实际开发没有这么用的，坑比用法，起不到任何解耦的作用)
            Console.WriteLine("AutoFac常见用法总结 基本用法:");
            ContainerBuilder builder = new ContainerBuilder();
            //把UserBLL注册为IUserBLL实现类,当请求IUserBLL接口的时候，返回UserBLL对象
            builder.RegisterType<UserBLL>().As<IUserBLL>();
            IContainer resolver = builder.Build();//容器接口
            IUserBLL userBLL = resolver.Resolve<IUserBLL>();
            var result4 = userBLL.GetUserInfor();
            Console.WriteLine(result4);

            //AsImplementedInterfaces的用法(评价：同时添加对Ypf.BLL层和Ypf.IBLL层的引用，这里也是单纯的为了介绍AsImplementedInterfaces()的用法，还是存在实现类的身影，在实际开发中没有这么用的，起不到任何解耦的作用，坑比用法)
            Console.WriteLine("AsImplementedInterfaces的用法:");
            ContainerBuilder builder1 = new ContainerBuilder();
            builder1.RegisterType<UserBLL>().AsImplementedInterfaces();
            IContainer resolver1 = builder1.Build();
            IUserBLL iuserBLL1 = resolver1.Resolve<IUserBLL>();
            IPeopleBLL ipeopleBLL = resolver1.Resolve<IPeopleBLL>();
            var result5 = iuserBLL1.GetUserInfor();
            var result6 = ipeopleBLL.Introduce();
            Console.WriteLine(result5);
            Console.WriteLine(result6);

            //AutoFac+反射(彻底消灭实现类)（评价：彻底摆脱了实现类的身影，与Ypf.BLL层进行了解耦，只需要添加对Ypf.IBLL层的引用，但需要把Ypf.BLL的程序集拷贝到AutoFacTest项目下。）
            Console.WriteLine("AutoFac+反射(彻底消灭实现类):");
            ContainerBuilder builder2 = new ContainerBuilder();
            Assembly ass1 = Assembly.Load("Ypf.BLL");
            builder2.RegisterAssemblyTypes(ass1).AsImplementedInterfaces();
            IContainer resolver2 = builder2.Build();
            IUserBLL iuserBLL2 = resolver2.Resolve<IUserBLL>();
            IPeopleBLL ipeopleBLL2 = resolver2.Resolve<IPeopleBLL>();
            var result7 = iuserBLL2.GetUserInfor();
            var result8 = ipeopleBLL2.Introduce();
            Console.WriteLine(result7);
            Console.WriteLine(result8);

            //AutoFac+反射(通过配置文件获取)
            Console.WriteLine("通过配置文件获取:");
            ContainerBuilder builder3 = new ContainerBuilder();
            string DLLName = ConfigurationManager.AppSettings["DllName"].ToString();
            Assembly ass3 = Assembly.Load(DLLName);
            builder3.RegisterAssemblyTypes(ass3).AsImplementedInterfaces();
            IContainer resolver3 = builder3.Build();
            IUserBLL iuserBLL3 = resolver3.Resolve<IUserBLL>();
            IPeopleBLL ipeopleBLL3 = resolver3.Resolve<IPeopleBLL>();
            var result9 = iuserBLL3.GetUserInfor();
            var result10 = ipeopleBLL3.Introduce();
            Console.WriteLine(result9);
            Console.WriteLine(result10);

            //PropertiesAutowired(属性的自动注入)
            Console.WriteLine("PropertiesAutowired(属性的自动注入):");
            ContainerBuilder builder4 = new ContainerBuilder();
            Assembly ass4 = Assembly.Load(DLLName);
            builder4.RegisterAssemblyTypes(ass4).AsImplementedInterfaces().PropertiesAutowired();
            IContainer resolver4 = builder4.Build();
            IRoleBLL iroleBLL = resolver4.Resolve<IRoleBLL>();
            var result11 = iroleBLL.ShowDIDemo();
            Console.WriteLine(result11);

            //1个接口多个实现类的情况
            Console.WriteLine("1个接口多个实现类的情况");
            ContainerBuilder builder5 = new ContainerBuilder();
            Assembly ass5 = Assembly.Load(DLLName);
            builder5.RegisterAssemblyTypes(ass5).AsImplementedInterfaces();
            IContainer resolver5 = builder5.Build();
            IEnumerable<IAnimalBLL> blls = resolver5.Resolve<IEnumerable<IAnimalBLL>>();
            foreach (var animalBll in blls)
            {
                Console.WriteLine(animalBll.GetType());
                Console.WriteLine(animalBll.Introduce());
            }

            //AutoFac的几种常见生命周期
            Console.WriteLine("AutoFac的几种常见生命周期:InstancePerDependency");
            ContainerBuilder builder6 = new ContainerBuilder();
            Assembly ass6 = Assembly.Load(DLLName);
            builder6.RegisterAssemblyTypes(ass6).AsImplementedInterfaces().InstancePerDependency();
            IContainer resolver6 = builder6.Build();
            IUserBLL iUserBLL1 = resolver6.Resolve<IUserBLL>();
            IUserBLL iUserBLL2 = resolver6.Resolve<IUserBLL>();
            Console.WriteLine(object.ReferenceEquals(iUserBLL1, iUserBLL2));

            Console.WriteLine("AutoFac的几种常见生命周期:SingleInstance,单例，只有在第一次请求的时候创建");
            ContainerBuilder builder7 = new ContainerBuilder();
            Assembly ass7 = Assembly.Load(DLLName);
            builder7.RegisterAssemblyTypes(ass7).AsImplementedInterfaces().SingleInstance();
            IContainer resolver7 = builder7.Build();
            IUserBLL iUserBLL3 = resolver7.Resolve<IUserBLL>();
            IUserBLL iUserBLL4 = resolver7.Resolve<IUserBLL>();
            Console.WriteLine(object.ReferenceEquals(iUserBLL3,iUserBLL4));

            Console.WriteLine();



            Console.ReadKey();
        }

        /// <summary>
        ///  简单工厂，隔离对象的创建
        /// </summary>
        public class SimpleFactory
        {
            private static string DLLName = ConfigurationManager.AppSettings["DllName"].ToString();
            private static string ClassName = ConfigurationManager.AppSettings["ClassName"].ToString();
            public static IUserBLL CreateInstance()
            {
                Assembly ass = Assembly.Load(DLLName);
                Type type = ass.GetType(ClassName);
                object myUserBLL = Activator.CreateInstance(type);
                IUserBLL ibll = (IUserBLL)myUserBLL;
                return ibll;
            }
        }
    }
}
