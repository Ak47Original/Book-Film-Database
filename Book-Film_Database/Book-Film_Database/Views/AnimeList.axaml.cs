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

public partial class AnimeList : UserControl
{
    public AnimeList()
    {
        InitializeComponent();
        
        Console.WriteLine($"Počet anime: {App.AppData.AnimesList.Count}");
        
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
            /*
            var StackPanel = new StackPanel {HorizontalAlignment = HorizontalAlignment.Stretch,};
            AnimeContainer.Children.Add(StackPanel);
            var AName = new TextBlock { Text = anime.Name, FontSize = 30, HorizontalAlignment = HorizontalAlignment.Stretch, Background = new SolidColorBrush(Color.Parse("#0e0433"))};
            StackPanel.Children.Add(AName); 
            var AGenre = new TextBlock { Text = anime.Genre, FontSize = 25, HorizontalAlignment = HorizontalAlignment.Stretch, Background = new SolidColorBrush(Color.Parse("#0e0433"))};
            StackPanel.Children.Add(AGenre); 
            */
        }
        
        
        //<TextBlock Text="Demon Slayer" FontSize="30"></TextBlock>
        //    <TextBlock Text="Action" FontSize="25"></TextBlock>
    }
}