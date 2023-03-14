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
        List<Product> listFilter = new List<Product>();

        public PageProductList()
        {
            InitializeComponent();

            Conclusion();
        }

        public PageProductList(string FIO)
        {
            InitializeComponent();
            

            Conclusion();
            TextFIO.Text = FIO;
        }

        private void Conclusion()
        {
            ListProduct.ItemsSource = ClassBase.entities.Product.ToList();
            ComboSort.SelectedIndex = 0;
            ComboFilter.SelectedIndex = 0;
            TextCountBD.Text = "Количество записей в БД: " + ClassBase.entities.Product.ToList().Count;
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
            List<Product> products = ClassBase.entities.Product.ToList();

            //listFilter = new List<Product>();
            listFilter = ClassBase.entities.Product.ToList();
            //Фильтрация

            if (ComboFilter.SelectedIndex == 1)
            {
                listFilter = new List<Product>();
                listFilter.Where(x => x.ProductDiscountAmount >= 0 && Convert.ToDouble(x.ProductDiscountAmount) <= 9.9999).ToList();
            }
            else if (ComboFilter.SelectedIndex == 2)
            {
                listFilter = new List<Product>();
                listFilter.Where(x => x.ProductDiscountAmount >= 10 && Convert.ToDouble(x.ProductDiscountAmount) <= 14.9999).ToList();
            }
            else if (ComboFilter.SelectedIndex == 3)
            {
                listFilter = new List<Product>();
                listFilter.Where(x => x.ProductDiscountAmount >= 15).ToList();
            }
            else
            {
                listFilter = ClassBase.entities.Product.ToList();
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
    }
}
