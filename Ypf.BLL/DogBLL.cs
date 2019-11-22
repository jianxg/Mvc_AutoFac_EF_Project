using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ypf.IBLL;

namespace Ypf.BLL
{
    public class DogBLL : IAnimalBLL
    {
        public string Introduce()
        {
            return "我是狗";
        }
    }
}
