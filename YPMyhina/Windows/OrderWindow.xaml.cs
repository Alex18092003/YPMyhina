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
            ListOrder.SelectedItem = ClassBase.products;

            List<Order> orders = ClassBase.entities.Order.ToList();
            int count = 0;
            foreach(Order order in orders)
            {
                count = order.OrderID;
            }
            TextOrderNumber.Text = " " + count + 1;

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
        }

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
