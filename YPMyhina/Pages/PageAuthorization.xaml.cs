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
    /// Логика взаимодействия для PageAuthorization.xaml
    /// </summary>
    public partial class PageAuthorization : Page
    {
        public PageAuthorization()
        {
            InitializeComponent();
        }

        private void TextLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                TextPassword.Focus();
            }

        }
        private void Proverka()
        {
            User user = ClassBase.entities.User.FirstOrDefault(x => x.UserPassword == TextPassword.Text && x.UserLogin == TextLogin.Text);
            if(user != null)
            {
                int role = user.UserRole;
                MessageBox.Show($"{role}", "sd");
                Classes.ClassFrame.frame.Navigate(new PageProductList(role));
            }
            else
            {
                MessageBox.Show("Данный логин и пароль не зарегистрированы", "Сообщение");
            }
        }

        private void ButtonEntrance_Click(object sender, RoutedEventArgs e)
        {
            Proverka();
        }

        private void TextPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Proverka();
            }
        }
    }
}
