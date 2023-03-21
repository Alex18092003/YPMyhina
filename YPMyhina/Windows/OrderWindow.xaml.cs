using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YPMyhina.Windows
{
    /// <summary>
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        User user;
        public OrderWindow(User user)
        {
            InitializeComponent();
            this.user = user;
            ListOrder.ItemsSource = ClassBase.products;

            List<Order> orders = ClassBase.entities.Order.ToList();
            int count = 0;
            foreach(Order order in orders)
            {
                count = order.OrderID;
            }
            TextOrderNumber.Text = " " + (count + 1);

            List<Point> pickupPoints = ClassBase.entities.Point.ToList();
            ComboBoxOrderPickupPoint.Items.Add("Пункт выдачи");
            foreach (Point pickup in pickupPoints)
            {
                ComboBoxOrderPickupPoint.Items.Add(pickup.PointIndex + " " + pickup.City + " " + pickup.Street + " " + pickup.House);
            }
            ComboBoxOrderPickupPoint.SelectedIndex = 0;

            if (user != null)
            {
                TextName.Text = " " + user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
            }
            else
            {
                TextName.Text = " Гость";
            }
            AmountDiscount();
        }

        /// <summary>
        /// метод для подсчета скидки и общей стоимости
        /// </summary>
        private void AmountDiscount()
        {
            try
            {
                double startAmount = 0, discount, endAmount = 0;
                foreach (Product product in ClassBase.products)
                {
                    endAmount += (double)product.PriceOrder;
                    startAmount += (double)product.CostOrder;
                }
                discount = 100 - 100 * endAmount / startAmount;
                ResultDiscount.Text = "Общая скидка: " + Math.Round(discount, 1) + "%";
                ResultAmount.Text = "Общая стоимость: " + string.Format("{0:C2}", endAmount);
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так", "Ошибка");
            }
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Random random = new Random();

                bool stockProduct = true; //количество товара на складе
                foreach (Product product in ClassBase.products)
                {
                    if (product.ProductQuantityInStock < 3)
                    {
                        stockProduct = false;
                    }
                }

                Order order = new Order(); // создание нового заказа
                order.OrderStatus = 1;
                if (stockProduct == false)
                {
                    order.OrderDeliveryDate = DateTime.Now.AddDays(6);
                }
                else
                {
                    order.OrderDeliveryDate = DateTime.Now.AddDays(3);
                }
                if (ComboBoxOrderPickupPoint.SelectedIndex != 0)
                {
                    order.OrderPickupPoint = ComboBoxOrderPickupPoint.SelectedIndex;
                    order.OrderDate = DateTime.Now;
                    if (user != null)
                    {
                        order.OrderClient = user.UserID;
                    }
                    order.OrderKod = random.Next(100, 1000); //создание кода заказа

                    ClassBase.entities.Order.Add(order);

                    foreach (Product product in ClassBase.products) //создание новых элементов таблицы OrderProduct
                    {
                        OrderProduct orderProduct = new OrderProduct()
                        {
                            OrderID = order.OrderID,
                            ProductArticleNumber = product.ProductID,
                            OrderProductICount = 1
                        };
                        ClassBase.entities.OrderProduct.Add(orderProduct);
                    }
                    ClassBase.entities.SaveChanges();
                    MessageBox.Show("Заказ успешно добавлен", "Информация");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Не выбран пункт выдачи заказа", "Информация");
                }

            }
            catch
            {
                MessageBox.Show("Что-то пошло не так", "Ошибка");
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                switch (MessageBox.Show("Подтвердите удаление товара", "Удаление", MessageBoxButton.YesNo))
                {
                    case MessageBoxResult.Yes:
                        Button btn = (Button)sender;
                        int index = Convert.ToInt32( btn.Uid);
                        Product product = ClassBase.products.FirstOrDefault(z => z.ProductID == index);
                        ClassBase.products.Remove(product);
                        ListOrder.Items.Refresh();
                        break;
                    default:
                        break;
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так", "Ошибка");
            }
        }

        private void Count_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                TextBox tb = (TextBox)sender;
                int index = Convert.ToInt32( tb.Uid);
                if (tb.Text == "0")
                {
                    Product product = ClassBase.products.FirstOrDefault(z => z.ProductID == index);
                    ClassBase.products.Remove(product);
                    ListOrder.Items.Refresh();
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так", "Ошибка");
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
