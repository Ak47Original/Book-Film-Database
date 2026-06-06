using System;
using System.Collections.Generic;
using System.IO;
using Book_Film_Database.Models;
using Microsoft.VisualBasic.FileIO;


namespace Book_Film_Database.Data;
 
public class AppData
{
    public List<Anime> AnimesList { get; set; } = new List<Anime>();
    public List<Manga> MangaList { get; set; } = new List<Manga>();
    
    public void ReadCSV()
    {
        AnimesList.Clear();
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
        string Rating = "";
        
        using (TextFieldParser parser = new TextFieldParser(@"C:\Users\andri\Desktop\Anime.csv"))
        {
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            while (!parser.EndOfData)
            {
                    string[] fields = parser.ReadFields();
                    foreach (string field in fields)
                    {
                        FieldCount++;
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
                                    Rating =  field;
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
                                case 17:
                                    
                                    AnimesList.Add(new Anime {Ranks = Rank, Name = Name, JapaneseName = JapaneseName, Description = Description, RelatedAnime = RelatedAnime, RelatedManga = RelatedManga,  Rating = Rating, Genre = Genre});
                                    Console.WriteLine($" {Rank}");
                                    column = 0;
                                    break;
                            }
                            
                        }    
                    }
            }
        }
    }
}