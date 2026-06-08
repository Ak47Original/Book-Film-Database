using Avalonia.Controls;
using System;
using Avalonia.Interactivity;
using Book_Film_Database.Models;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Book_Film_Database;

public partial class MainWindow : Window
{
    private readonly AnimeList _animePage = new AnimeList();
    private readonly MangaList _mangaPage = new MangaList();
    private readonly FavoritesList _favoritesPage = new FavoritesList();
    private readonly MangaFavoritesList _favoritesMangaPage = new MangaFavoritesList();
    private readonly ReviewsList _reviewsPage = new ReviewsList();

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
    private void ReviewsListButton(object? sender, RoutedEventArgs e)
    {
        try
        {
            MainContent.Content = null;
            MainContent.Content = _reviewsPage; 
        }
        catch (Exception ex)
        {
            Console.WriteLine($"CHYBA: {ex.Message}");
        }
    }

    private void MangaFavoritesList(object? sender, RoutedEventArgs e)
    {
        try
        {
            MainContent.Content = null;
            _favoritesMangaPage.ShowFavorite(App.AppData.FavoritesMangaList);
            MainContent.Content = _favoritesMangaPage;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"CHYBA: {ex.Message}");
        }
    }

    private void ExportAnime(object? sender, RoutedEventArgs e)
    {
        var lines = App.AppData.AnimesList.Select(anime => 
            $"{anime.Name},{anime.Genre},{anime.Rating}"
        );
    
        File.WriteAllLines($"{App.AppData.AnimeExportPath}", lines);
    }

    private void ExportManga(object? sender, RoutedEventArgs e)
    {
        var lines = App.AppData.MangaList.Select(manga => 
            $"{manga.Name},{manga.Genre},{manga.Rating}"
        );
    
        File.WriteAllLines($"{App.AppData.MangaExportPath}", lines);    }
}