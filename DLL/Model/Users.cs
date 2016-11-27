using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Model
{
    public class Users
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UsersId { get; set; }
        [StringLength(50)]
        [Index(IsUnique = true)]
        public string name { get; set; }
        public string password { get; set; }
        public string salt { get; set; }
        public virtual AccessGroup accessGroup { get; set; }
        //company object
        public virtual Company company { get; set; }
        public bool active { get; set; }
    }
}
