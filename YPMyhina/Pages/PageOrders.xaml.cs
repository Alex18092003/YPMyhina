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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YPMyhina.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageOrders.xaml
    /// </summary>
    public partial class PageOrders : Page
    {
        User user;
        public PageOrders()
        {
            InitializeComponent();
            Conclusion();
        }

        public PageOrders(User user)
        {
            InitializeComponent();
            this.user = user;
            Conclusion();
        }

        /// <summary>
        /// Метод заполнения ComboBox
        /// </summary>
        private void Conclusion()
        {
            try
            {
                ListVOrders.ItemsSource = ClassBase.entities.Order.ToList();
                ComboSort.SelectedIndex = 0;
                ComboFilt.SelectedIndex = 0;

            }
            catch
            {
                MessageBox.Show("Что-то пошло не так", "Ошибка");
            }

        }

        /// <summary>
        /// Фильтрация и сортировка списка заказов
        /// </summary>
        private void Filter()
        {
            List<Order> orders = ClassBase.entities.Order.ToList();
            if (ComboFilt.SelectedIndex > 0)
            {
                switch (ComboFilt.SelectedIndex)
                {
                    case 1:
                        orders = orders.Where(x => x.DiscountProcent > 0 && x.DiscountProcent < 10).ToList();
                        break;
                    case 2:
                        orders = orders.Where(x => x.DiscountProcent >= 10 && x.DiscountProcent < 15).ToList();
                        break;
                    case 3:
                        orders = orders.Where(x => x.DiscountProcent >= 15).ToList();
                        break;
                }
            }
            if (ComboSort.SelectedIndex > 0)
            {
                switch (ComboSort.SelectedIndex)
                {
                    case 1:
                        orders = orders.OrderBy(x => x.Summa).ToList();
                        break;
                    case 2:
                        orders = orders.OrderByDescending(x => x.Summa).ToList();
                        break;
                }
            }
            ListVOrders.ItemsSource = orders;
            if (orders.Count == 0)
            {
                _ = MessageBox.Show("Данные не найдены");
            }
        }


        private void ButonBack_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (user == null)
                {
                    Classes.ClassFrame.frame.Navigate(new PageProductList());
                }
                else if (user.UserRole == 2 || user.UserRole == 3 || user.UserRole == 1)
                {
                    Classes.ClassFrame.frame.Navigate(new PageProductList(user));
                }

            }
            catch
            {
                MessageBox.Show("Что-то пошло не так", "Ошибка");
            }
        }

        private void ComboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void ComboFilt_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

    }
}
