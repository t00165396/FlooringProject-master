using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringProgram.Models
{
    public class EditOrderReceipt
    {
        public int Date { get; set; }
        public List<Order> Orders { get; set; }
        public Order Order { get; set; }
    }
}
