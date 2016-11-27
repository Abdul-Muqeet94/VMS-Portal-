using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class PreVisitors
    {
        public int ID { get; set; }
        public string VNIC { get; set; }
        public string VfirstName { get; set; }
        public string VlastName { get; set; }
        public string Vaddress { get; set; }
        public string VphoneNo { get; set; }
        public string Vcompany { get; set; }
        public int host_id { get; set; }
        public string host_name { get; set; }
        public string host_company { get; set; }
        public string host_contact_no { get; set; }
        public string host_email { get; set; }
        public string from_Date { get; set; }
        public string to_date { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string CreatedBy { get; set; }
        public bool status { get; set; }
    }
}
