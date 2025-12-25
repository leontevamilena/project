using CinemaDbLibrary.Contexts;
using CinemaDbLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace CinemaApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CinemaDbContext _context = new CinemaDbContext();
        public MainWindow()
        {
            InitializeComponent();
        }


        private void AddFrameButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedMovie = MovieComboBox.SelectedItem as Movie;

            if (selectedMovie is null)
                return;

            var openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файл .PNG(*.png)|*.png|Файл .JPG (*.jpg)|*.jpg";

            if (openFileDialog.ShowDialog() == false)
                return;

            var fileInfo = new FileInfo(openFileDialog.FileName);

            if (fileInfo.Length >> 20 > 2)
            {
                MessageBox.Show("Файл слишком большой", "Error");
                return;
            }

            try
            {
                var directory = System.IO.Path.Combine(Environment.CurrentDirectory, "images");

                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);

                var fileDirectory = System.IO.Path.Combine(directory, openFileDialog.SafeFileName);

                System.IO.File.Copy(openFileDialog.FileName, fileDirectory, true);
            }
            catch
            {
                MessageBox.Show("Не удалось сохранить файл", "Error");
                return;
            }


            MessageBox.Show("Файл сохранен", "Успешно");
        }
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var movies = await _context.Movies.ToListAsync();
            MovieComboBox.ItemsSource = movies;
            MovieComboBox.SelectedItem = movies.FirstOrDefault();
        }

    }
}