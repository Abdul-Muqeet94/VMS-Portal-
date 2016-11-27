using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Model
{
    public class PreVisitors
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public string VNIC { get; set; }
        public string VfirstName { get; set; }
        public string VlastName { get; set; }
        public string Vaddress { get; set; }
        public string VphoneNo { get; set; }
        public string Vcompany { get; set; }
        public string hFirstName { get; set; }
        public string hLastName { get; set; }
        public string hcompany { get; set; }
        public string hcontactNo { get; set; }
        public string hemail { get; set; }
        public string hfloor { get; set; }
        public string hphone { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; }
        public string CreatedBy { get; set; }
        public bool status { get; set; }
    }
}
