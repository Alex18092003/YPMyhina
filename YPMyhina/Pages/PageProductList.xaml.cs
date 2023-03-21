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

namespace YPMyhina
{
    /// <summary>
    /// Логика взаимодействия для PageProductList.xaml
    /// </summary>
    public partial class PageProductList : Page
    {
        User user;
        List<Product> listFilter = new List<Product>();

        public PageProductList()
        {
            InitializeComponent();

            Conclusion();
        }

        public PageProductList(User user)
        {
            InitializeComponent();
            this.user = user;

            Conclusion();
            TextFIO.Text = user.UserSurname + " " +user.UserSurname + " " + user.UserPatronymic;
          
        }

        /// <summary>
        /// Метод заполнения ComboBox
        /// </summary>
        private void Conclusion()
        {
            try {
                ListProduct.ItemsSource = ClassBase.entities.Product.ToList();
                ComboSort.SelectedIndex = 0;
                ComboFilter.SelectedIndex = 0;
                TextCountBD.Text = "Количество записей в БД: " + ClassBase.entities.Product.ToList().Count;

            }
            catch
            {
                MessageBox.Show("Что-то пошло не так", "Ошибка");
            }

        }

        private void ButtonExit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Classes.ClassFrame.frame.Navigate(new Pages.PageAuthorization());
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так", "Ошибка");
            }
        }

        /// <summary>
        /// метод для сортировки/ фильтрации / поиска
        /// </summary>
        private void Filter()
        {
            try
            {
                List<Product> products = ClassBase.entities.Product.ToList();

                listFilter = ClassBase.entities.Product.ToList();
                //Фильтрация

                switch (ComboFilter.SelectedIndex)
                {
                    case 0:
                        {
                            listFilter = listFilter.ToList();
                        }
                        break;
                    case 1:
                        {
                            listFilter = listFilter.Where(x => x.ProductDiscountAmount >= 0 && Convert.ToDouble(x.ProductDiscountAmount) <= 9.9999).ToList();
                        }
                        break;
                    case 2:
                        {
                            listFilter = listFilter.Where(x => x.ProductDiscountAmount >= 10 && Convert.ToDouble(x.ProductDiscountAmount) <= 14.9999).ToList();
                        }
                        break;
                    case 3:
                        {
                            listFilter = listFilter.Where(x => x.ProductDiscountAmount >= 15).ToList();

                        }
                        break;

                }

                //Сортировка
                switch (ComboSort.SelectedIndex)
                {
                    case 1:
                        {
                            listFilter.Sort((x, y) => x.ProductCost.CompareTo(y.ProductCost));
                        }
                        break;
                    case 2:
                        {
                            listFilter.Sort((x, y) => x.ProductCost.CompareTo(y.ProductCost));
                            listFilter.Reverse();

                        }
                        break;
                }


                //поиск
                if (!string.IsNullOrWhiteSpace(TextBoxSeach.Text))
                {
                    listFilter = listFilter.Where(x => x.ProductName.ToLower().Contains(TextBoxSeach.Text.ToLower())).ToList();
                }

                ListProduct.ItemsSource = listFilter;
                if (listFilter.Count == 0)
                {
                    MessageBox.Show("Нет записей", "Информация");
                }
                TextCount.Text = listFilter.Count + "из" + ClassBase.entities.Product.ToList().Count;
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

        private void ComboFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Filter();

        }

        private void TextBoxSeach_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        //первая заглавная буква
        private void TextBoxSeach_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
               
                if (TextBoxSeach.Text.Length == 1)
                {
                    TextBoxSeach.Text = TextBoxSeach.Text.ToUpper();
                    TextBoxSeach.Select(TextBoxSeach.Text.Length, 0);
                }

            }
            catch
            {
                MessageBox.Show("Что-то пошло не так", "Ошибка");
            }
        }

        private void ButtonOrder_Click(object sender, RoutedEventArgs e)
        {
            Windows.OrderWindow w = new Windows.OrderWindow(user);
            w.ShowDialog();

            if(ClassBase.products.Count == 0)
            {
                ButtonOrder.Visibility = Visibility.Collapsed;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ButtonOrder.Visibility = Visibility.Visible;
            MenuItem mi = (MenuItem)sender;
            int index = Convert.ToInt32( mi.Uid);
            Product product = ClassBase.entities.Product.FirstOrDefault(x => x.ProductID == index);
            ClassBase.products.Add(product);
        }

        private void ButtonDelete_Loaded(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (user == null || user.UserRole == 1)
            {
                btn.Visibility = Visibility.Collapsed;
            }
            else if (user.UserRole == 2 || user.UserRole == 3)
            {
                btn.Visibility = Visibility.Visible;
                ButtonOrders.Visibility = Visibility.Visible;
            }
            else
            {
                btn.Visibility = Visibility.Collapsed;
            }
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            switch (MessageBox.Show("Подтвердите удаление", "Сообщение", MessageBoxButton.YesNo))
            {
                case MessageBoxResult.Yes:
                    Button btn = (Button)sender;
                    int index = Convert.ToInt32( btn.Uid);

                    List<OrderProduct> products = ClassBase.entities.OrderProduct.Where(x => x.ProductArticleNumber == index).ToList();
                    if (products.Count == 0)
                    {
                        Product prod = ClassBase.entities.Product.FirstOrDefault(z => z.ProductID == index);
                        ClassBase.entities.Product.Remove(prod);
                        ClassBase.entities.SaveChanges();
                        Classes.ClassFrame.frame.Navigate(new PageProductList(user));
                    }
                    else
                    {
                        MessageBox.Show("Удаление товара запрещено", "Сообщение");
                    }
                    break;
                default:
                    break;
            }
        }

        private void ButtonOrders_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Classes.ClassFrame.frame.Navigate(new Pages.PageOrders());
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так", "Ошибка");
            }
        }
    }
}
