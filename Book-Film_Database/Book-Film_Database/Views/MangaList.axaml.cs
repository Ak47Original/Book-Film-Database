using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Book_Film_Database.Models;
using Book_Film_Database.Data;
using System;
using Avalonia.Layout;
using Avalonia.Media;

namespace Book_Film_Database;

public partial class MangaList : UserControl
{
    public MangaList()
    {
        InitializeComponent();
        Console.WriteLine($"Počet anime: {App.AppData.AnimesList.Count}");
        Console.WriteLine($"Počet mangy: {App.AppData.MangaList.Count}");
        MangaListBox.ItemsSource = App.AppData.MangaList;
        
        
        /*
        foreach (var manga in App.AppData.MangaList)
        {
            var Button = new Button {HorizontalAlignment = HorizontalAlignment.Stretch, Background = new SolidColorBrush(Color.Parse("#0e0433")) };
            var StackPanel = new StackPanel {HorizontalAlignment = HorizontalAlignment.Stretch,};
            MangaContainer.Children.Add(Button);
            var MName = new TextBlock{Text = manga.Name, FontSize = 30, HorizontalAlignment = HorizontalAlignment.Stretch, Background = new SolidColorBrush(Color.Parse("#0e0433"))};
            StackPanel.Children.Add(MName);
            var MGenre = new TextBlock{Text = manga.Genre, FontSize = 25, HorizontalAlignment = HorizontalAlignment.Stretch, Background = new SolidColorBrush(Color.Parse("#0e0433"))};
            StackPanel.Children.Add(MGenre);
            Button.Content = StackPanel;

            var StackPanel = new StackPanel {HorizontalAlignment = HorizontalAlignment.Stretch,};
            AnimeContainer.Children.Add(StackPanel);
            var AName = new TextBlock { Text = anime.Name, FontSize = 30, HorizontalAlignment = HorizontalAlignment.Stretch, Background = new SolidColorBrush(Color.Parse("#0e0433"))};
            StackPanel.Children.Add(AName);
            var AGenre = new TextBlock { Text = anime.Genre, FontSize = 25, HorizontalAlignment = HorizontalAlignment.Stretch, Background = new SolidColorBrush(Color.Parse("#0e0433"))};
            StackPanel.Children.Add(AGenre);
        }
        */
    }
}