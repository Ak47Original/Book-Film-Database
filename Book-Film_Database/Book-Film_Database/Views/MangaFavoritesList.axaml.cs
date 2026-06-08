using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using Book_Film_Database.Models;
using Avalonia.Interactivity;
using System.Linq;

namespace Book_Film_Database;

public partial class MangaFavoritesList : UserControl
{
    public MangaFavoritesList()
    {
        InitializeComponent();
    }
    
    public void ShowFavorite(List<Manga> FavoritesMangaLists)
    {
        MyFavoritesMangaListBox.ItemsSource = null;
        MyFavoritesMangaListBox.ItemsSource = new List<Manga>(FavoritesMangaLists);
    }
    private void RemoveFromMangaFavorites_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is Manga mangaToRemove)
        {
            App.AppData.FavoritesMangaList.Remove(mangaToRemove);
            var mainListManga = App.AppData.MangaList.FirstOrDefault(a => a.Name == mangaToRemove.Name);
            if (mainListManga != null)
            {
                mainListManga.IsFavorite = false;
            }
            
            App.AppData.SaveUserData();
            MyFavoritesMangaListBox.ItemsSource = App.AppData.FavoritesMangaList.ToArray();
        }
    }
}