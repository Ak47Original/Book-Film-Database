using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System.Collections.Generic;
using Book_Film_Database.Models;


namespace Book_Film_Database; 

public partial class FavoritesList : UserControl
{
    public FavoritesList()
    {
        InitializeComponent();
    }

    public void ShowFavorite(List<Anime> FavoritesLists)
    {
        MangaListBox.ItemsSource = null;
        MangaListBox.ItemsSource = new List<Anime>(FavoritesLists);
    }
}