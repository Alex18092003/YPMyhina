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
using System.Windows.Threading;

namespace YPMyhina.Pages
{
    /// <summary>
    /// Логика взаимодействия для PageAuthorization.xaml
    /// </summary>
    public partial class PageAuthorization : Page
    {
        private int timer = 10;
        DispatcherTimer dispatcherTimer = new DispatcherTimer();

        public PageAuthorization()
        {
            InitializeComponent();
        }

        public PageAuthorization(int j)
        {
            InitializeComponent();
            if (j == 1)
            {
                TextTimer.Text = "Попробуйте ввести ещё раз Логин и Пароль";
            }
            else
            {
                TextPassword.IsEnabled = false;
                TextLogin.IsEnabled = false;
                ButtonEntrance.IsEnabled = false;
                dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                dispatcherTimer.Tick += new EventHandler(Back);
                dispatcherTimer.Start();

            }
        }

        private void Back(object sender, EventArgs e)
        {
            if (timer == -1)
            {
                dispatcherTimer.Stop();
                TextPassword.IsEnabled = true;
                TextLogin.IsEnabled = true;
                ButtonEntrance.IsEnabled = true;
                TextTimer.Text = "Попробуйте ввести ещё раз Логин и Пароль"; 
            }
            else
            {
                TextTimer.Text = "Повторно ввести Логин и Пароль можно будет через: " + timer;
            }
            timer--;
        }

        private void TextLogin_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    TextPassword.Focus();
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так", "Ошибка");
            }
        }

        /// <summary>
        /// метод для проверки логина и пароля пользователя
        /// </summary>
        private void Proverka()
        {

                User user = ClassBase.entities.User.FirstOrDefault(x => x.UserPassword == TextPassword.Text && x.UserLogin == TextLogin.Text);
                if (user != null)
                {
                    string FIO = user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
                   
                    Classes.ClassFrame.frame.Navigate(new PageProductList(FIO));
                }
                else
                {
                    MessageBox.Show("Введен не верный логин или пароль", "Сообщение");
                   
                    Windows.WindowCaptcha w = new Windows.WindowCaptcha();
                    
                    TextPassword.Text = "";
                    TextLogin.Text = "";
                    w.ShowDialog();
            }
           
        }

        private void ButtonEntrance_Click(object sender, RoutedEventArgs e)
        {
            Proverka();
        }

        private void TextPassword_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                if (e.Key == Key.Enter)
                {
                    Proverka();
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так", "Ошибка");
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Classes.ClassFrame.frame.Navigate(new PageProductList());
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так", "Ошибка");
            }
        }
    }
}
