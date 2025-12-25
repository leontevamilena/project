using System.Windows;
using System.Windows.Controls;
using AuthLibrary.Contexts;
using AuthLibrary.Services;

namespace AuthWpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private readonly AuthService _authService = new(new CinemaDbContext());

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;

            var result = await _authService.RegisterUserAsync(login, password);
            if (result)
                MessageBox.Show("Вы зарегестрированы");
            else
                MessageBox.Show("Не удалось зарегестрироваться");
        }

        private async void AuthenticateButton_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string password = PasswordBox.Password;

            var user = await _authService.AuthenticateUserAsync(login, password);
            if (user is not null)
            {
                UserSession.Instance.SetCurrentUser(user);

                Auth window = new();
                Hide();
                window.ShowDialog();
                Show();
            }
            else
                MessageBox.Show("Не удалось авторизоваться");
        }
    }
}