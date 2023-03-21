using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace YPMyhina
{
    public partial class Order
    {
        public string OrderList
        {
            get
            {
                List<OrderProduct> products = ClassBase.entities.OrderProduct.Where(x => x.OrderID == OrderID).ToList();
                string orderList = "";
                for (int i = 0; i < products.Count; i++)
                {
                    orderList = i == products.Count - 1
                        ? orderList + products[i].Product.ProductName+ " Количество: " + "1"
                        : orderList + products[i].Product.ProductName + " Количество: " + "1" + "\n";
                }
                return orderList;
            }
        }

        public double Summa
        {
            get
            {
                List<OrderProduct> products = ClassBase.entities.OrderProduct.Where(x => x.OrderID == OrderID).ToList();
                double summa = 0;
                foreach (OrderProduct product in products)
                {
                    summa += product.Product.ProductCost * (double)product.Product.ProductDiscountAmount / 100 * 1;
                }
                return summa;
            }
        }

        public string StrSumma => "" + Summa.ToString("0.00");

        public double DiscountProcent
        {
            get
            {
                List<OrderProduct> products = ClassBase.entities.OrderProduct.Where(x => x.OrderID == OrderID).ToList();
                double summaDiscount = 0;
                foreach (OrderProduct product in products)
                {
                    summaDiscount += (double)product.Product.ProductDiscountAmount * 1;
                }
                double summa = 0;
                foreach (OrderProduct product in products)
                {
                    summa += (double)product.Product.ProductCost * 1;
                }
                double procent = (summa - summaDiscount) / summa * 100;
                return procent;
            }
        }

        public string StrDiscount => "" + DiscountProcent.ToString("0.00");

    }
}
