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
        public PageProductList()
        {
            InitializeComponent();
        }
        public PageProductList( int role)
        {
            InitializeComponent();
            ListProduct.ItemsSource = ClassBase.entities.Product.ToList();
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
    }
}
