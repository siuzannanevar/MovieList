using MovieList.Models;
using MovieList; 
using System;
using System.Windows;
using MovieList;
using Movie_list;

namespace MovieList
{
    public partial class AddEditWindow : Window
    {
        public Film Film { get; private set; }

        public AddEditWindow(Film film = null)
        {
            InitializeComponent();

            GenreBox.ItemsSource = Enum.GetValues(typeof(Genre));

            if (film != null)
            {
                Film = new Film
                {
                    Id = film.Id,
                    Title = film.Title,
                    Director = film.Director,
                    Year = film.Year,
                    Genre = film.Genre,
                    Rating = film.Rating
                };

                TitleBox.Text = Film.Title;
                DirectorBox.Text = Film.Director;
                YearBox.Text = Film.Year.ToString();
                GenreBox.SelectedItem = Film.Genre;
                RatingBox.Text = Film.Rating.ToString();
            }
            else
            {
                Film = new Film();
                GenreBox.SelectedItem = Genre.Unknown; 
            }
        }

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            // Валидация полей
            if (string.IsNullOrWhiteSpace(TitleBox.Text) || string.IsNullOrWhiteSpace(DirectorBox.Text))
            {
                MessageBox.Show(Messages.ErrorNoSelection, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!int.TryParse(YearBox.Text, out var year))
            {
                MessageBox.Show(Messages.ErrorInvalidYear, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!double.TryParse(RatingBox.Text, out var rating) || rating < 0 || rating > 10)
            {
                MessageBox.Show(Messages.ErrorInvalidRating, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Film.Title = TitleBox.Text.Trim();
            Film.Director = DirectorBox.Text.Trim();
            Film.Year = year;
            Film.Rating = rating;

            if (GenreBox.SelectedItem is Genre genre)
                Film.Genre = genre;
            else
                Film.Genre = Genre.Unknown;

            DialogResult = true;
        }
    }
}
