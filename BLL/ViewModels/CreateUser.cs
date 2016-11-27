using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class CreateUser
    {
        public string name { get; set; }
        public string password { get; set; }
        public List<AccessGroup> accessGroup { get; set; }
        //company object
        public  List<Company> company { get; set; }
    }
}
