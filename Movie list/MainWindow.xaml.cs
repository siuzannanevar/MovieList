using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Data;
using MovieList.Models;

namespace MovieList
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Film> Film { get; set; }
        private CollectionViewSource collectionView;

        public MainWindow()
        {
            InitializeComponent();

            Film = new ObservableCollection<Film>
            {
                new Film() { Title = "Avengers: Endgame", Director = "Anthony Russo", Year = 2019, Genre = Genre.SciFi, Rating = 8.4 },
                new Film() { Title = "10 Things I Hate About You", Director = "Gil Junger", Year = 1999, Genre = Genre.Romcom, Rating = 7.4 },
                new Film() { Title = "Green Book", Director = "Peter Farrelly", Year = 2018, Genre = Genre.Drama, Rating = 8.2 }
            };

            collectionView = new CollectionViewSource { Source = Film };
            FilmsDataGrid.ItemsSource = collectionView.View;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var window = new AddEditWindow();
            if (window.ShowDialog() == true)
            {
                Film.Add(window.Film);
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (FilmsDataGrid.SelectedItem is Film selected)
            {
                var window = new AddEditWindow(selected);
                if (window.ShowDialog() == true)
                {
                    int index = Film.IndexOf(selected);
                    Film[index] = window.Film;
                }
            }
            else
            {
                MessageBox.Show("Please select a movie to edit.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (FilmsDataGrid.SelectedItem is Film selected)
            {
                var result = MessageBox.Show(
                    $"Delete movie \"{selected.Title}\"?",
                    "Confirm Deleting",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    Film.Clear();
                }
            }
            else
            {
                MessageBox.Show("Please select a movie to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (collectionView == null) return;

            string query = SearchBox.Text?.Trim();

            if (string.IsNullOrEmpty(query))
            {
                collectionView.View.Filter = null;
            }

            else
            {
                collectionView.View.Filter = item =>
                {
                    if (item is Film film)
                    {
                        return film.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                               film.Director.Contains(query, StringComparison.OrdinalIgnoreCase);
                    }
                    return false;
                };
            }
        }
    }
}
