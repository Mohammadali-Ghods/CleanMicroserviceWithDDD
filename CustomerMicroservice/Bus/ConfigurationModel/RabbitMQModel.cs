using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus.ConfigurationModel
{
    public class RabbitMQModel
    {
        public string Address { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
