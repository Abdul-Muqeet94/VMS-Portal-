using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class Users
    {
        public int userId { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
        public int acessgroup_id { get; set; }
        public string accessgroup_name { get; set; }
        public string company_id { get; set; }
        public string compnay_name { get; set; }
        public CreateUser create_user { get; set; }
        public bool active { get; set; }
    }
}
