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
    /// Логика взаимодействия для WindowCaptcha.xaml
    /// </summary>
    public partial class WindowCaptcha : Window
    {
        public string text = String.Empty;


        public WindowCaptcha()
        {
            InitializeComponent();
            Random random = new Random();
                for (int i = 0; i < 15; i++)
            {
                SolidColorBrush brush = new SolidColorBrush(Color.FromRgb((byte)random.Next(256), (byte)random.Next(256), (byte)random.Next(256)));
                Line line = new Line()
                {
                    X1 = random.Next((int)CanvasCaptcha.Width),
                    Y1 = random.Next((int)CanvasCaptcha.Height),
                    X2 = random.Next((int)CanvasCaptcha.Width),
                    Y2 = random.Next((int)CanvasCaptcha.Height),
                    Stroke = brush,
                };
                CanvasCaptcha.Children.Add(line);
            }
            string chars = "abcdefghijklmnopqrstuvwxyzQWERTYUIOPASDFGHJKLZXCVBNM0123456789";
            for(int i = 0; i < 5; i ++)
            {
                text += chars[random.Next(chars.Length)];
            }

            for(int i =0; i < 5; i ++)
            {
                int margin = 40;
                int v = random.Next(3);
                if (v == 0)
                {
                    int font = random.Next(16, 20);

                    TextBlock textBlock = new TextBlock()
                    {
                        Text = text[i].ToString(),
                        FontSize = font,
                        FontStyle = FontStyles.Italic,
                        Margin = new Thickness(i * 35, random.Next(50), random.Next(50), 0),

                    };
                    CanvasCaptcha.Children.Add(textBlock);
                }
                else if (v == 1)
                {
                    int font = random.Next(16, 25);

                    TextBlock textBlock = new TextBlock()
                    {
                        Text = text[i].ToString(),
                        FontSize = font,

                        Margin = new Thickness(i * 35, random.Next(50), random.Next(50), 0),

                    };
                    CanvasCaptcha.Children.Add(textBlock);
                }
                else if (v == 2)
                {
                    int font = random.Next(16, 25);

                    TextBlock textBlock = new TextBlock()
                    {
                        Text = text[i].ToString(),
                        FontSize = font,
                        FontWeight = FontWeights.Bold,
                        FontStyle = FontStyles.Italic,
                        Margin = new Thickness(i * 35, random.Next(50), random.Next(50), 0),

                    };
                    CanvasCaptcha.Children.Add(textBlock);
                }
            }
        }

        int j = 0;
        /// <summary>
        /// метод для проверки капчи ввведенной пользователем
        /// </summary>
        public void Proverka()
        {
            try { 
            if(TextBoxCaptcha.Text == text)
            {
                j++;
                this.Close();
                Classes.ClassFrame.frame.Navigate(new Pages.PageAuthorization(j));
            }
            else
            {
                MessageBox.Show("Упс! Код введен неверно", "Сообщение");
                    this.Close();
                    Classes.ClassFrame.frame.Navigate(new Pages.PageAuthorization(j));
            }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так", "Ошибка");
            }
        }

        private void TextBoxCaptcha_KeyDown(object sender, KeyEventArgs e)
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

        private void ButtonEnter_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
            Proverka();
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так", "Ошибка");
            }
        }

    }
}
