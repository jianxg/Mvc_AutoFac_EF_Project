using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ypf.IBLL;

namespace Ypf.BLL
{
    public class RoleBLL : IRoleBLL
    {
        public IUserBLL userBLL { get; set; }

        /// <summary>
        /// 展示角色信息
        /// </summary>
        /// <returns></returns>
        public string ShowRoleInfor()
        {
            return "我是管理员角色";
        }


        public string ShowDIDemo()
        {
            return "哈哈：" + userBLL.GetUserInfor();
        }
    }
}
