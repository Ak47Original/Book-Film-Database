using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Book_Film_Database.Models;
using Book_Film_Database.Data;
using System;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;

namespace Book_Film_Database;

public partial class MangaList : UserControl
{
    public List<Manga> SearchedMangaList { get; set; } = new List<Manga>();
    private Manga _selectedManga;
    private List<Manga> _mangaList;
    private int SearchLength;
    public MangaList()
    {
        InitializeComponent();
        
        MangaListBox.ItemsSource = App.AppData.MangaList;
        _mangaList = App.AppData.MangaList;
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
        SearchedMangaList.RemoveAll(item => item.Name.Length < currentText.Length);
        if (currentText.Length != 0)
        {
            if (currentText.Length < SearchLength)
            {
                SearchedMangaList.Clear();
                foreach (var manga in _mangaList)
                {
                    if (currentText[0] == manga.Name[0]) 
                    {
                        SearchedMangaList.Add(manga); 
                    }
                }
                SearchedMangaList.RemoveAll(item => item.Name.Length < currentText.Length);
                if (currentText.Length > 1)
                {
                    for (int k = 1; k < currentText.Length; k++)
                    {
                        SearchedMangaList.RemoveAll(item => item.Name[k]  != currentText[k]);
                    }
                }
            }
            else
            {
                if (currentText.Length == 1){
                    foreach (var manga in _mangaList)
                    {
                        if (currentText[0] == manga.Name[0]) 
                        {
                            SearchedMangaList.Add(manga); 
                        }
                    }
                }
                if (currentText.Length > 1)
                {
                    SearchedMangaList.RemoveAll(item => item.Name[currentText.Length-1] != currentText[currentText.Length-1]);
                }
            }
            MangaListBox.ItemsSource = App.AppData.MangaList;
            if (SearchedMangaList.Count > 0)
            {
                MangaListBox.ItemsSource = SearchedMangaList;
            }
        }
        else
        {
            MangaListBox.ItemsSource = _mangaList;
            SearchedMangaList.Clear();
        }
        SearchLength = currentText.Length;
    }

    private void ToggleButton_OnClick(object? sender, RoutedEventArgs e)
    {
        var button = sender as ToggleButton;
        var grid = button.Content as Grid;
        var heart = grid.Children[1] as Path;
        var manga = button.DataContext as Manga;
        heart.IsVisible = button.IsChecked ?? false;
        
        bool IsChecked = button.IsChecked ?? false;
        
        if (heart != null)
        {
            heart.IsVisible = IsChecked;
        }

        if (manga != null)
        {
            manga.IsFavorite = IsChecked;

            if (IsChecked)
            {
                if (!App.AppData.FavoritesMangaList.Contains(manga))
                {
                    App.AppData.FavoritesMangaList.Add(manga);
                    App.AppData.SaveUserData();
                }
            }
            else
            {
                App.AppData.FavoritesMangaList.Remove(manga);
            }
        }
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        DetailPanel.IsVisible = true;
        var button = sender as Button;
        var manga = button.DataContext as Manga;
        _selectedManga = manga;
        Name.Text = manga.Name;
        Genre.Text = manga.Genre;
        Description.Text = manga.Description;
        
        
    }

    private void CloseButton_OnClick(object? sender, RoutedEventArgs e)
    {
        DetailPanel.IsVisible = false;
    }

    private void StatusUnread_OnClick(object? sender, RoutedEventArgs e)
    {
        if (App.AppData.ReadMangaList.Contains(_selectedManga))
        {
            App.AppData.ReadMangaList.Remove(_selectedManga);
            App.AppData.UnreadMangaList.Add(_selectedManga);
        }
        else if (App.AppData.ReadingMangaList.Contains(_selectedManga))
        {
            App.AppData.ReadingMangaList.Remove(_selectedManga);
            App.AppData.UnreadMangaList.Add(_selectedManga);
        }
        else if (App.AppData.PlanningMangaList.Contains(_selectedManga))
        {
            App.AppData.PlanningMangaList.Remove(_selectedManga);
            App.AppData.UnreadMangaList.Add(_selectedManga);
        }
        else if (App.AppData.DroppedMangaList.Contains(_selectedManga))
        {
            App.AppData.DroppedMangaList.Remove(_selectedManga);
            App.AppData.UnreadMangaList.Add(_selectedManga);
        }
        
        App.AppData.SaveUserData();
        MangaListBox.ItemsSource = _mangaList.ToArray();
    }

    private void StatusRead_OnClick(object? sender, RoutedEventArgs e)
    {
        if (App.AppData.ReadingMangaList.Contains(_selectedManga))
        {
            App.AppData.ReadingMangaList.Remove(_selectedManga);
            App.AppData.ReadMangaList.Add(_selectedManga);
        }
        else if (App.AppData.PlanningMangaList.Contains(_selectedManga))
        {
            App.AppData.PlanningMangaList.Remove(_selectedManga);
            App.AppData.ReadMangaList.Add(_selectedManga);
        }
        else if (App.AppData.DroppedMangaList.Contains(_selectedManga))
        {
            App.AppData.DroppedMangaList.Remove(_selectedManga);
            App.AppData.ReadMangaList.Add(_selectedManga);
        }
        else
        {
            App.AppData.UnreadMangaList.Remove(_selectedManga);
            App.AppData.ReadMangaList.Add(_selectedManga);
        }
        App.AppData.SaveUserData();
        MangaListBox.ItemsSource = _mangaList.ToArray();
    }

    private void StatusReading_OnClick(object? sender, RoutedEventArgs e)
    {
        if (App.AppData.ReadMangaList.Contains(_selectedManga))
        {
            App.AppData.ReadMangaList.Remove(_selectedManga);
            App.AppData.ReadingMangaList.Add(_selectedManga);
        }
        else if (App.AppData.PlanningMangaList.Contains(_selectedManga))
        {
            App.AppData.PlanningMangaList.Remove(_selectedManga);
            App.AppData.ReadingMangaList.Add(_selectedManga);
        }
        else if (App.AppData.DroppedMangaList.Contains(_selectedManga))
        {
            App.AppData.DroppedMangaList.Remove(_selectedManga);
            App.AppData.ReadingMangaList.Add(_selectedManga);
        }
        else
        {
            App.AppData.UnreadMangaList.Remove(_selectedManga);
            App.AppData.ReadingMangaList.Add(_selectedManga);
        }
        App.AppData.SaveUserData();
        MangaListBox.ItemsSource = _mangaList.ToArray();
    }

    private void StatusPlanning_OnClick(object? sender, RoutedEventArgs e)
    {
        if (App.AppData.ReadMangaList.Contains(_selectedManga))
        {
            App.AppData.ReadMangaList.Remove(_selectedManga);
            App.AppData.PlanningMangaList.Add(_selectedManga);
        }
        else if (App.AppData.ReadingMangaList.Contains(_selectedManga))
        {
            App.AppData.ReadingMangaList.Remove(_selectedManga);
            App.AppData.PlanningMangaList.Add(_selectedManga);
        }
        else if (App.AppData.DroppedMangaList.Contains(_selectedManga))
        {
            App.AppData.DroppedMangaList.Remove(_selectedManga);
            App.AppData.PlanningMangaList.Add(_selectedManga);
        }
        else
        {
            App.AppData.UnreadMangaList.Remove(_selectedManga);
            App.AppData.PlanningMangaList.Add(_selectedManga);
        } 
        App.AppData.SaveUserData();
        MangaListBox.ItemsSource = _mangaList.ToArray();
    }

    private void StatusDropped_OnClick(object? sender, RoutedEventArgs e)
    {
        if (App.AppData.ReadMangaList.Contains(_selectedManga))
        {
            App.AppData.ReadMangaList.Remove(_selectedManga);
            App.AppData.DroppedMangaList.Add(_selectedManga);
        }
        else if (App.AppData.ReadingMangaList.Contains(_selectedManga))
        {
            App.AppData.ReadingMangaList.Remove(_selectedManga);
            App.AppData.DroppedMangaList.Add(_selectedManga);
        }
        else if (App.AppData.PlanningMangaList.Contains(_selectedManga))
        {
            App.AppData.PlanningMangaList.Remove(_selectedManga);
            App.AppData.DroppedMangaList.Add(_selectedManga);
        }
        else
        {
            App.AppData.UnreadMangaList.Remove(_selectedManga);
            App.AppData.DroppedMangaList.Add(_selectedManga);
        }
        App.AppData.SaveUserData();
        MangaListBox.ItemsSource = _mangaList.ToArray();
    }

    private void FilterRead(object? sender, RoutedEventArgs e)
    {
        MangaListBox.ItemsSource = App.AppData.ReadMangaList;
        _mangaList = App.AppData.ReadMangaList;

    }

    private void FilterUnread(object? sender, RoutedEventArgs e)
    {
        MangaListBox.ItemsSource = App.AppData.UnreadMangaList;
        _mangaList = App.AppData.UnreadMangaList;

    }

    private void FilterReading(object? sender, RoutedEventArgs e)
    {
        MangaListBox.ItemsSource = App.AppData.ReadingMangaList;
        _mangaList = App.AppData.ReadingMangaList;
    }

    private void FilterPlanning(object? sender, RoutedEventArgs e)
    {
        MangaListBox.ItemsSource = App.AppData.PlanningMangaList;
        _mangaList = App.AppData.PlanningMangaList;

    }

    private void FilterDropped(object? sender, RoutedEventArgs e)
    {
        MangaListBox.ItemsSource = App.AppData.DroppedMangaList;
        _mangaList = App.AppData.DroppedMangaList;
    }

    private void FilterAll(object? sender, RoutedEventArgs e)
    {
        MangaListBox.ItemsSource = App.AppData.MangaList;
        _mangaList = App.AppData.MangaList;
    }
}