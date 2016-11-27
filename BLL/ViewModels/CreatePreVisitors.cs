using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ViewModels
{
    public class CreatePreVisitors
    {
        public List<Hosts> hosts = new List<Hosts>();
        public List<PreVisitors> visitorList = new List<PreVisitors>();
        public PreVisitors visitor = new PreVisitors();
    }
}
