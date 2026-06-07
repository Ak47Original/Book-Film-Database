using Avalonia.Controls;
using System;
using Avalonia.Interactivity;

namespace Book_Film_Database;

public partial class MainWindow : Window
{
    private readonly AnimeList _animePage = new AnimeList();
    private readonly MangaList _mangaPage = new MangaList();
    private readonly FavoritesList _favoritesPage = new FavoritesList();
    public MainWindow()
    {
        InitializeComponent();
    }

    public void AnimeListButton(object? sender, RoutedEventArgs e)
    {
        
        try
        {
            MainContent.Content = null;
            MainContent.Content = _animePage;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"CHYBA: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }
    }
    public void MangaListButton(object? sender, RoutedEventArgs e)
    {
        
        try
        {
            MainContent.Content = null;
            MainContent.Content = _mangaPage;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"CHYBA: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }
    }
    public void FavoritesListButton(object? sender, RoutedEventArgs e)
    {
        try
        {
            MainContent.Content = null;
            _favoritesPage.ShowFavorite(App.AppData.FavoritesAnimeList);
            MainContent.Content = _favoritesPage;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"CHYBA: {ex.Message}");
        }
    }
}