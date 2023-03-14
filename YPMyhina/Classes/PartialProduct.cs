using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace YPMyhina
{
    public partial class Product
    {
        public SolidColorBrush ColorDiscount
        {
            get
            {
                if (ProductDiscountAmount > 15)
                {
                    return (SolidColorBrush)new BrushConverter().ConvertFrom("#7fff00");

                }
                else
                {
                    return Brushes.White;
                }
            }
        }

        public string StringProductCost
        {
            get
            {
                if (ProductDiscountAmount == 0 )
                {
                    return null;
                }
                else
                {
                    double b = Convert.ToDouble(ProductCost) / 100 * Convert.ToDouble(ProductDiscountAmount);
                    double bb = ProductCost - b;
                    return ProductCost + " ";
                }
            }
        }

        public string StringProductCost2
        {
            get
            {
                if (ProductDiscountAmount == 0)
                {
                    return ProductCost + " ";
                }
                else
                {
                    double b = Convert.ToDouble(ProductCost) / 100 * Convert.ToDouble(ProductDiscountAmount);
                    double bb = ProductCost - b;
                    return null;
                }
            }
        }

        public string StringProductCost3
        {
            get
            {
                if (ProductDiscountAmount == 0)
                {
                    return null;
                }
                else
                {
                    double b = Convert.ToDouble(ProductCost) / 100 * Convert.ToDouble(ProductDiscountAmount);
                    double bb = ProductCost - b;
                    return " " + bb;
                }
            }
        }

    }
}
