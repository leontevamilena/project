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

namespace AuthWpf
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Window
    {
        public Auth()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var login = UserSession.Instance.CurrentUser?.Login;
            if (login is not null)
                LoginTextBlock.Text = $"Привет, {login}";
            else
                LoginTextBlock.Text = "Ошибка";
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            UserSession.Instance.Clear();
            Close();
        }


    }
}
