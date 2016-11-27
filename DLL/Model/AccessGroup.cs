using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Model
{
    public class AccessGroup
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int AccessGroupId { get; set; }
        public string name { get; set; }
        public bool active { get; set; }
    }
}
