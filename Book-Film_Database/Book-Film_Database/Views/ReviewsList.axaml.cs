using Avalonia.Controls;
using Avalonia.Interactivity;
using System;
using System.Linq;

namespace Book_Film_Database;

public partial class ReviewsList : UserControl
{
    public ReviewsList()
    {
        InitializeComponent();
        RefreshReviews();
    }

    private void AddReviewButton_OnClick(object? sender, RoutedEventArgs e)
    {
        AddReviewPanel.IsVisible = true;
    }

    private void SaveButton_OnClick(object? sender, RoutedEventArgs e)
    {
        string title = TitleInput.Text ?? "";
        string genre = GenreInput.Text ?? "";
        string text = ReviewContentInput.Text ?? ""; 
        
        int rating = 0;
        if (!int.TryParse(RatingInput.Text, out rating) || rating < 0 || rating > 10)
        {
            RatingInput.Text = "";
            return;
        }

        if (string.IsNullOrWhiteSpace(title)) return;

        Book_Film_Database.Models.Review newReview = new Book_Film_Database.Models.Review
        {
            Title = title,
            Genre = genre,
            Rating = rating,
            Text = text
        };
        
        App.AppData.ReviewsList.Add(newReview);
        
        App.AppData.SaveUserData();

        RefreshReviews();
        
        //Reset proměnných
        TitleInput.Text = "";
        GenreInput.Text = "";
        RatingInput.Text = "";
        ReviewContentInput.Text = "";
        AddReviewPanel.IsVisible = false;
    }

    private void CancelButton_OnClick(object? sender, RoutedEventArgs e)
    {
        AddReviewPanel.IsVisible = false;
    }

    private void RefreshReviews()
    {
        ReviewsListBox.ItemsSource = App.AppData.ReviewsList.ToArray();
    }
}