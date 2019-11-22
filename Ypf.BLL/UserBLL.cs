using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ypf.IBLL;

namespace Ypf.BLL
{
    public class UserBLL : IUserBLL, IPeopleBLL
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        public string GetUserInfor()
        {
            return "我是获取用户信息的方法";
        }

        /// <summary>
        /// 自我介绍
        /// </summary>
        /// <returns></returns>
        public string Introduce()
        {
            return "我是jianxg";
        }
    }
}
