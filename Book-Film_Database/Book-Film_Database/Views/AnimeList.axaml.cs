using System.Collections.Generic;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Book_Film_Database.Models;
using Book_Film_Database.Data;
using System;

namespace Book_Film_Database;

public partial class AnimeList : UserControl
{
    public AnimeList()
    {
        InitializeComponent();
        
        Console.WriteLine($"Počet anime: {App.AppData.AnimesList.Count}");
        foreach (var anime in App.AppData.AnimesList)
        {
            var Name = new TextBlock { Text = anime.Name, FontSize = 30};
            AnimeContainer.Children.Add(Name); 
            var Genre = new TextBlock { Text = anime.Genre, FontSize = 25 };
            AnimeContainer.Children.Add(Genre); 
            
        }
        
        
        //<TextBlock Text="Demon Slayer" FontSize="30"></TextBlock>
        //    <TextBlock Text="Action" FontSize="25"></TextBlock>
    }
}