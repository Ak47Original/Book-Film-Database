using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Book_Film_Database.Models;
using Book_Film_Database.Data;
using System;
using System.ComponentModel.DataAnnotations;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Controls.Primitives;


namespace Book_Film_Database;

public partial class AnimeList : UserControl
{
    
    public List<Anime> SearchedAnimeList { get; set; } = new List<Anime>();
    private Anime _selectedAnime;
    private int selectedAnimeIndex;
    private List<Anime> _animeList;
    private int SearchLength;
    public AnimeList()
    {
        InitializeComponent();
        Console.WriteLine($"Počet anime: {App.AppData.AnimesList.Count}");
        Console.WriteLine($"Počet mangy: {App.AppData.MangaList.Count}");
        AnimeListBox.ItemsSource = App.AppData.AnimesList;
        _animeList = App.AppData.AnimesList;
        /*
        foreach (var anime in App.AppData.AnimesList)
        {
            var Button = new Button {HorizontalAlignment = HorizontalAlignment.Stretch, Background = new SolidColorBrush(Color.Parse("#0e0433")) };
            var StackPanel = new StackPanel {HorizontalAlignment = HorizontalAlignment.Stretch,};
            AnimeContainer.Children.Add(Button);
            var AName = new TextBlock{Text = anime.Name, FontSize = 30, HorizontalAlignment = HorizontalAlignment.Stretch, Background = new SolidColorBrush(Color.Parse("#0e0433"))};
            StackPanel.Children.Add(AName);
            var AGenre = new TextBlock{Text = anime.Genre, FontSize = 25, HorizontalAlignment = HorizontalAlignment.Stretch, Background = new SolidColorBrush(Color.Parse("#0e0433"))};
            StackPanel.Children.Add(AGenre);
            Button.Content = StackPanel;

            var StackPanel = new StackPanel {HorizontalAlignment = HorizontalAlignment.Stretch,};
            AnimeContainer.Children.Add(StackPanel);
            var AName = new TextBlock { Text = anime.Name, FontSize = 30, HorizontalAlignment = HorizontalAlignment.Stretch, Background = new SolidColorBrush(Color.Parse("#0e0433"))};
            StackPanel.Children.Add(AName);
            var AGenre = new TextBlock { Text = anime.Genre, FontSize = 25, HorizontalAlignment = HorizontalAlignment.Stretch, Background = new SolidColorBrush(Color.Parse("#0e0433"))};
            StackPanel.Children.Add(AGenre);

        }
        */

        //<TextBlock Text="Demon Slayer" FontSize="30"></TextBlock>
        //    <TextBlock Text="Action" FontSize="25"></TextBlock>
    }

    private void SearchTextBox_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        var textBox = sender as TextBox;
        string currentText = "";
        int i = 0;
        currentText = textBox.Text;
        SearchedAnimeList.RemoveAll(item => item.Name.Length < currentText.Length);
        if (currentText.Length != 0)
        {
            if (currentText.Length < SearchLength)
            {
                SearchedAnimeList.Clear();
                foreach (var anime in _animeList)
                {
                    if (currentText[0] == anime.Name[0]) 
                    {
                        SearchedAnimeList.Add(anime); 
                    }
                }

                if (currentText.Length > 1)
                {
                    for (int k = 1; k < currentText.Length; k++)
                    {
                        SearchedAnimeList.RemoveAll(item => item.Name[k]  != currentText[k]);
                    }
                }
            }
            else
            {
                if (currentText.Length == 1){
                    foreach (var anime in _animeList)
                    {
                        if (currentText[0] == anime.Name[0]) 
                        {
                            SearchedAnimeList.Add(anime); 
                        }
                    }
                }
                if (currentText.Length > 1)
                {
                    SearchedAnimeList.RemoveAll(item => item.Name[currentText.Length-1] != currentText[currentText.Length-1]);
                }
            }
            AnimeListBox.ItemsSource = App.AppData.AnimesList;
            if (SearchedAnimeList.Count > 0)
            {
                AnimeListBox.ItemsSource = SearchedAnimeList;
            }
        }
        else
        {
            AnimeListBox.ItemsSource = _animeList;
            SearchedAnimeList.Clear();
        }
        SearchLength = currentText.Length;
    }

    private void ToggleButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var button = sender as ToggleButton;
        var grid = button.Content as Grid;
        var heart = grid.Children[1] as Path;
        var anime = button.DataContext as Anime;
        heart.IsVisible = button.IsChecked ?? false;
        
        bool IsChecked = button.IsChecked ?? false;
        
        if (heart != null)
        {
            heart.IsVisible = IsChecked;
        }

        if (anime != null)
        {
            anime.IsFavorite = IsChecked;

            if (IsChecked)
            {
                if (!App.AppData.FavoritesAnimeList.Contains(anime))
                {
                    App.AppData.FavoritesAnimeList.Add(anime);
                    App.AppData.SaveUserData();
                }
            }
            else
            {
                App.AppData.FavoritesAnimeList.Remove(anime);
            }
        }
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        DetailPanel.IsVisible = true;
        var button = sender as Button;
        var anime = button.DataContext as Anime;
        _selectedAnime = anime;
        Name.Text = anime.Name;
        Genre.Text = anime.Genre;
        Description.Text = anime.Description;
        
        
    }

    private void CloseButton_OnClick(object? sender, RoutedEventArgs e)
    {
        DetailPanel.IsVisible = false;
    }

    private void StatusUnwatched_OnClick(object? sender, RoutedEventArgs e)
    {
        if (App.AppData.WatchedAnimeList.Contains(_selectedAnime))
        {
            App.AppData.WatchedAnimeList.Remove(_selectedAnime);
            App.AppData.UnwatchedAnimeList.Add(_selectedAnime);
        }
        else if (App.AppData.WatchingAnimeList.Contains(_selectedAnime))
        {
            App.AppData.WatchingAnimeList.Remove(_selectedAnime);
            App.AppData.UnwatchedAnimeList.Add(_selectedAnime);
        }
        else if (App.AppData.PlanningAnimeList.Contains(_selectedAnime))
        {
            App.AppData.PlanningAnimeList.Remove(_selectedAnime);
            App.AppData.UnwatchedAnimeList.Add(_selectedAnime);
        }
        else if (App.AppData.DroppedAnimeList.Contains(_selectedAnime))
        {
            App.AppData.DroppedAnimeList.Remove(_selectedAnime);
            App.AppData.UnwatchedAnimeList.Add(_selectedAnime);
        }
    }

    private void StatusWatched_OnClick(object? sender, RoutedEventArgs e)
    {
        if (App.AppData.WatchingAnimeList.Contains(_selectedAnime))
        {
            App.AppData.WatchingAnimeList.Remove(_selectedAnime);
            App.AppData.WatchedAnimeList.Add(_selectedAnime);
        }
        else if (App.AppData.PlanningAnimeList.Contains(_selectedAnime))
        {
            App.AppData.PlanningAnimeList.Remove(_selectedAnime);
            App.AppData.WatchedAnimeList.Add(_selectedAnime);
        }
        else if (App.AppData.DroppedAnimeList.Contains(_selectedAnime))
        {
            App.AppData.DroppedAnimeList.Remove(_selectedAnime);
            App.AppData.WatchedAnimeList.Add(_selectedAnime);
        }
        else
        {
            App.AppData.UnwatchedAnimeList.Remove(_selectedAnime);
            App.AppData.WatchedAnimeList.Add(_selectedAnime);
        }
    }

    private void StatusWatching_OnClick(object? sender, RoutedEventArgs e)
    {
        if (App.AppData.WatchedAnimeList.Contains(_selectedAnime))
        {
            App.AppData.WatchedAnimeList.Remove(_selectedAnime);
            App.AppData.WatchingAnimeList.Add(_selectedAnime);
        }
        else if (App.AppData.PlanningAnimeList.Contains(_selectedAnime))
        {
            App.AppData.PlanningAnimeList.Remove(_selectedAnime);
            App.AppData.WatchingAnimeList.Add(_selectedAnime);
        }
        else if (App.AppData.DroppedAnimeList.Contains(_selectedAnime))
        {
            App.AppData.DroppedAnimeList.Remove(_selectedAnime);
            App.AppData.WatchingAnimeList.Add(_selectedAnime);
        }
        else
        {
            App.AppData.UnwatchedAnimeList.Remove(_selectedAnime);
            App.AppData.WatchingAnimeList.Add(_selectedAnime);
        }
    }

    private void StatusPlanning_OnClick(object? sender, RoutedEventArgs e)
    {
        if (App.AppData.WatchedAnimeList.Contains(_selectedAnime))
        {
            App.AppData.WatchedAnimeList.Remove(_selectedAnime);
            App.AppData.PlanningAnimeList.Add(_selectedAnime);
        }
        else if (App.AppData.WatchingAnimeList.Contains(_selectedAnime))
        {
            App.AppData.WatchingAnimeList.Remove(_selectedAnime);
            App.AppData.PlanningAnimeList.Add(_selectedAnime);
        }
        else if (App.AppData.DroppedAnimeList.Contains(_selectedAnime))
        {
            App.AppData.DroppedAnimeList.Remove(_selectedAnime);
            App.AppData.PlanningAnimeList.Add(_selectedAnime);
        }
        else
        {
            App.AppData.UnwatchedAnimeList.Remove(_selectedAnime);
            App.AppData.PlanningAnimeList.Add(_selectedAnime);
        } 
    }

    private void StatusDropped_OnClick(object? sender, RoutedEventArgs e)
    {
        if (App.AppData.WatchedAnimeList.Contains(_selectedAnime))
        {
            App.AppData.WatchedAnimeList.Remove(_selectedAnime);
            App.AppData.DroppedAnimeList.Add(_selectedAnime);
        }
        else if (App.AppData.WatchingAnimeList.Contains(_selectedAnime))
        {
            App.AppData.WatchingAnimeList.Remove(_selectedAnime);
            App.AppData.DroppedAnimeList.Add(_selectedAnime);
        }
        else if (App.AppData.PlanningAnimeList.Contains(_selectedAnime))
        {
            App.AppData.PlanningAnimeList.Remove(_selectedAnime);
            App.AppData.DroppedAnimeList.Add(_selectedAnime);
        }
        else
        {
            App.AppData.UnwatchedAnimeList.Remove(_selectedAnime);
            App.AppData.DroppedAnimeList.Add(_selectedAnime);
        }
    }

    private void FilterAll(object? sender, RoutedEventArgs e)
    {
        AnimeListBox.ItemsSource = App.AppData.AnimesList;
        _animeList = App.AppData.AnimesList;
    }

    private void FilterWatched(object? sender, RoutedEventArgs e)
    {
        AnimeListBox.ItemsSource = App.AppData.WatchedAnimeList;
        _animeList = App.AppData.WatchedAnimeList;

    }

    private void FilterUnwathced(object? sender, RoutedEventArgs e)
    {
        AnimeListBox.ItemsSource = App.AppData.UnwatchedAnimeList;
        _animeList = App.AppData.UnwatchedAnimeList;

    }

    private void FilterWatching(object? sender, RoutedEventArgs e)
    {
        AnimeListBox.ItemsSource = App.AppData.WatchingAnimeList;
        _animeList = App.AppData.WatchingAnimeList;
    }

    private void FilterPlanning(object? sender, RoutedEventArgs e)
    {
        AnimeListBox.ItemsSource = App.AppData.PlanningAnimeList;
        _animeList = App.AppData.PlanningAnimeList;

    }

    private void FilterDropped(object? sender, RoutedEventArgs e)
    {
        AnimeListBox.ItemsSource = App.AppData.DroppedAnimeList;
        _animeList = App.AppData.DroppedAnimeList;
    }
}