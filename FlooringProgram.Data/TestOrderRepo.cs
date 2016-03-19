using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringProgram.Models;

namespace FlooringProgram.Data
{
    public class TestOrderRepo : IOrderRepo
    {
        private static List<Order> _orders;

        //ctor
        public TestOrderRepo()
        {
            if (_orders == null)
            {
                _orders = new List<Order>();
                var order1 = new Order();
                order1.orderNumber = 1;
                order1.customerName = "Wise";
                order1.stateName = "OH";
                order1.taxRate = 6.25M;
                order1.productType = "Wood";
                order1.Area = 100.00M;
                order1.CostPerSquareFoot = 5.15M;
                order1.LaborCostPerSquareFoot = 4.75M;
                order1.MaterialCost = 515.00M;
                order1.LaborCost = 475.00M;
                order1.Tax = 61.88M;
                order1.Total = 1051.88M;
                _orders.Add(order1);
            }
        }

        public List<Order> LoadOrders(string date)
        {
            return _orders;
        }

        public void OverWriteFileWithOrder(List<Order> orders, string date)
        {
            _orders = orders;
        }
    }
}
