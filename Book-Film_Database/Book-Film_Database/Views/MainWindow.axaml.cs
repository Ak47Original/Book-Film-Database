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
    public void AnimeList_Click()
    {
        try
        {
            
        }
        catch (Exception ex)
        {
            
        }
    }

    public void AnimeList(object? sender, RoutedEventArgs e)
    {
        Content = new AnimeList();
    }
}