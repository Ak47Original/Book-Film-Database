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
    }

    private void SearchTextBox_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        var textBox = sender as TextBox;
        string currentText = textBox.Text ?? "";
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
                        SearchedMangaList.RemoveAll(item => item.Name[k] != currentText[k]);
                    }
                }
            }
            else
            {
                if (currentText.Length == 1)
                {
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
                    SearchedMangaList.RemoveAll(item => item.Name[currentText.Length - 1] != currentText[currentText.Length - 1]);
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
                App.AppData.SaveUserData();
            }
        }
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        AddMangaPanel.IsVisible = false;
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

    private void FilterAll(object? sender, RoutedEventArgs e)
    {
        MangaListBox.ItemsSource = App.AppData.MangaList;
        _mangaList = App.AppData.MangaList;
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

    // 🔥 KOMPLETNĚ OPRAVENÉ METODY PRO PŘIDÁVÁNÍ VLASTNÍ MANGY:

    private void AddCustomMangaButton_OnClick(object? sender, RoutedEventArgs e)
    {
        DetailPanel.IsVisible = false;
        AddMangaPanel.IsVisible = true;
    }

    private void CancelCustomManga_OnClick(object? sender, RoutedEventArgs e)
    {
        AddMangaPanel.IsVisible = false;
        NewMangaTitleInput.Text = "";
        NewMangaGenreInput.Text = "";
        NewMangaDescInput.Text = "";
    }

    private void SaveCustomManga_OnClick(object? sender, RoutedEventArgs e)
    {
        string title = NewMangaTitleInput.Text ?? "";
        string genre = NewMangaGenreInput.Text ?? "";
        string description = NewMangaDescInput.Text ?? "";
        
        if (string.IsNullOrWhiteSpace(title))
        {
            NewMangaTitleInput.PlaceholderText = "Název je povinný!";
            return;
        }
        
        // Opraveno na model Manga
        var newManga = new Book_Film_Database.Models.Manga
        {
            Name = title,
            Genre = genre,
            Description = description,
            IsFavorite = false
        };
        
        App.AppData.CustomMangaList.Add(newManga);
        App.AppData.MangaList.Add(newManga);
        
        App.AppData.SaveUserData();
        
        MangaListBox.ItemsSource = App.AppData.MangaList.ToArray();
        _mangaList = App.AppData.MangaList;
        
        AddMangaPanel.IsVisible = false;
        NewMangaTitleInput.Text = "";
        NewMangaGenreInput.Text = "";
        NewMangaDescInput.Text = "";
    }
}