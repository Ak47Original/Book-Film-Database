using System;
using System.Collections.Generic;
using System.IO;
using Book_Film_Database.Models;
using Microsoft.VisualBasic.FileIO;


namespace Book_Film_Database.Data;

public class AppData
{
    public List<Anime> AnimeList { get; set; } = new List<Anime>();
    public List<Manga> MangaList { get; set; } = new List<Manga>();
    
    public void ReadCSV()
    {
        AnimeList.Clear();
        MangaList.Clear();
        int column = 0;
        int FieldCount = 0;
        int Rank = 0;
        string Name = "";
        string JapaneseName = "";
        string Genre = "";
        string Description = "";
        string RelatedAnime = "";
        string RelatedManga = "";
        float Rating = 0;
        
        using (TextFieldParser parser = new TextFieldParser(@"C:\Users\andri\Desktop\Anime.csv"))
        {
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            while (!parser.EndOfData)
            {
                    string[] fields = parser.ReadFields();
                    foreach (string field in fields)
                    {
                        if (FieldCount > 17)
                        {
                            Console.WriteLine(column);
                            column++;
                            switch (column)
                            {
                                case 1: 
                                    Rank = int.Parse(field);
                                    break;
                                case 2:
                                    Name = field;
                                    break;
                                case 3:
                                    JapaneseName = field;
                                    break;
                                case 8:
                                    Genre = field;
                                    break;
                                case 9:
                                    Rating =  float.Parse(field);
                                    break;
                                case 12:
                                    Description = field;
                                    break; 
                                case 14:
                                    RelatedManga = field;
                                    break;
                                case 15:
                                    RelatedAnime = field;
                                    break;
                                case 18:
                                
                                    Anime anime = new Anime();
                                    anime.Ranks = Rank;
                                    anime.Name = Name;
                                    anime.JapaneseName = JapaneseName;
                                    anime.Description = Description;
                                    anime.RelatedAnime = RelatedAnime;
                                    anime.RelatedManga = RelatedManga;
                                    anime.Rating = Rating;
                                    AnimeList.Add(anime);
                                    Console.WriteLine($" {anime.Ranks},{anime.Name} ");
                                    break;
                            }
                            
                        }    
                    }
            }
        }
    }
}