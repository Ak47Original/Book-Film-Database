using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Book_Film_Database.Models;
using Book_Film_Database.Data;
using System;
using System.ComponentModel.DataAnnotations;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Controls.Primitives;


namespace Book_Film_Database;

public partial class AnimeList : UserControl
{
    public List<Anime> SearchedAnimeList { get; set; } = new List<Anime>();
    public AnimeList()
    {
        InitializeComponent();
        Console.WriteLine($"Počet anime: {App.AppData.AnimesList.Count}");
        Console.WriteLine($"Počet mangy: {App.AppData.MangaList.Count}");
        AnimeListBox.ItemsSource = App.AppData.AnimesList;
        /*
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

            var StackPanel = new StackPanel {HorizontalAlignment = HorizontalAlignment.Stretch,};
            AnimeContainer.Children.Add(StackPanel);
            var AName = new TextBlock { Text = anime.Name, FontSize = 30, HorizontalAlignment = HorizontalAlignment.Stretch, Background = new SolidColorBrush(Color.Parse("#0e0433"))};
            StackPanel.Children.Add(AName);
            var AGenre = new TextBlock { Text = anime.Genre, FontSize = 25, HorizontalAlignment = HorizontalAlignment.Stretch, Background = new SolidColorBrush(Color.Parse("#0e0433"))};
            StackPanel.Children.Add(AGenre);
            
        }
        */
        
        //<TextBlock Text="Demon Slayer" FontSize="30"></TextBlock>
        //    <TextBlock Text="Action" FontSize="25"></TextBlock>
    }

    private void SearchTextBox_OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        var textBox = sender as TextBox;
        string currentText = "";
        int i = 0;
        if (textBox != null)
        {
            currentText = textBox.Text;
            if (currentText.Length == 1){
                foreach (var anime in App.AppData.AnimesList)
                {
                    if (currentText[0] == anime.Name[0]) 
                    {
                        SearchedAnimeList.Add(anime); 
                        Console.WriteLine(anime.Name);
                    }
                }
                Console.WriteLine("---------------------------------------------------");
            }
            if (currentText.Length > 1)
            {
                
                i = SearchedAnimeList.Count;
                for (int l = 0; l < i; l++)
                {
                    if (currentText[currentText.Length - 1] != SearchedAnimeList[l].Name[currentText.Length - 1])
                    {   
                        Console.WriteLine(currentText[currentText.Length - 1]);
                        Console.WriteLine(SearchedAnimeList[l].Name[currentText.Length - 1]);
                        Console.WriteLine("------------------------");
                        SearchedAnimeList.Remove(SearchedAnimeList[l]);
                        i--;
                        l--;
                    }
                }

                foreach (var anime in SearchedAnimeList)
                {
                    Console.WriteLine(anime.Name);
                }
                Console.WriteLine("---------------------------------------------------");
            }
        }
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        var button = sender as ToggleButton;
        var grid = button.Content as Grid;
        var heart = grid.Children[1] as Path;
        var anime = button.DataContext as Anime;
        heart.IsVisible = button.IsChecked ?? false;
        
        bool IsChecked = button.IsChecked ?? false;
        
        if (heart != null)
        {
            heart.IsVisible = IsChecked;
        }

        if (anime != null)
        {
            anime.IsFavorite = IsChecked;

            if (IsChecked)
            {
                if (!App.AppData.FavoritesAnimeList.Contains(anime))
                {
                    App.AppData.FavoritesAnimeList.Add(anime);
                }
            }
            else
            {
                App.AppData.FavoritesAnimeList.Remove(anime);
            }
        }
    }
}