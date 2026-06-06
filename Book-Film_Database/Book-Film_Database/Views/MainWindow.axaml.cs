using Avalonia.Controls;
using System;
using Avalonia.Interactivity;

namespace Book_Film_Database;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void AnimeListButton(object? sender, RoutedEventArgs e)
    {
        
        try
        {
            MainContent.Content = null;
            MainContent.Content = new AnimeList();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"CHYBA: {ex.Message}");
            Console.WriteLine(ex.StackTrace);
        }
    }
}