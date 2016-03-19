using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringProgram.Models
{
    public interface IOrderRepo
    {
        List<Order> LoadOrders(string date);
        void OverWriteFileWithOrder(List<Order> orders, string date);
    }
}
