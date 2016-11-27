using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class EditViewModel
    {
        public int ID { get; set; }
        public string VNIC { get; set; }
        public string VfirstName { get; set; }
        public string VlastName { get; set; }
        public string Vaddress { get; set; }
        public string VphoneNo { get; set; }
        public string Vcompany { get; set; }
        public string host_name { get; set; }
        public string host_email { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public List<Hosts> hosts { get; set; }
    }
}
