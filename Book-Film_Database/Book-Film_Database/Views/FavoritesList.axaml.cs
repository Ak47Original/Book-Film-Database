using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using Book_Film_Database.Models;
using Avalonia.Interactivity;
using System.Linq;

namespace Book_Film_Database; 

public partial class FavoritesList : UserControl
{
    public FavoritesList()
    {
        InitializeComponent();
    }

    public void ShowFavorite(List<Anime> FavoritesLists)
    {
        MyFavoritesListBox.ItemsSource = null;
        MyFavoritesListBox.ItemsSource = new List<Anime>(FavoritesLists);
    }
    private void RemoveFromFavorites_OnClick(object? sender, RoutedEventArgs e)
    {
        if (sender is Button button && button.DataContext is Anime animeToRemove)
        {
            App.AppData.FavoritesAnimeList.Remove(animeToRemove);
            var mainListAnime = App.AppData.AnimesList.FirstOrDefault(a => a.Name == animeToRemove.Name);
            if (mainListAnime != null)
            {
                mainListAnime.IsFavorite = false;
            }
            
            App.AppData.SaveUserData();
            MyFavoritesListBox.ItemsSource = App.AppData.FavoritesAnimeList.ToArray();
        }
    }
}