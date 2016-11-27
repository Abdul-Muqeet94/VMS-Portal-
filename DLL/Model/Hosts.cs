using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Model
{
    public class Hosts
    {
        [Key]
        public int EmpID { get; set; }
        public string hFirst { get; set; }
        public string hLast { get; set; }
        public string EmpCode { get; set; }
        public string Company_ID { get; set; }
    }
}
