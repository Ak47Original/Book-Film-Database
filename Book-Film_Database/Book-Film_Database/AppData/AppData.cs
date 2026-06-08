using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Book_Film_Database.Models;
using Microsoft.VisualBasic.FileIO;


namespace Book_Film_Database.Data;
 
public class AppData
{
    public List<Anime> AnimesList { get; set; } = new List<Anime>();
    public List<Anime> WatchedAnimeList { get; set; } = new List<Anime>();
    public List<Anime> WatchingAnimeList { get; set; } = new List<Anime>();
    public List<Anime> PlanningAnimeList { get; set; } = new List<Anime>();
    public List<Anime> DroppedAnimeList { get; set; } = new List<Anime>();
    public List<Anime> FavoritesAnimeList { get; set; } = new List<Anime>();
    public List<Manga> MangaList { get; set; } = new List<Manga>();
    public List<Manga> FavoritesMangaList { get; set; } = new List<Manga>();
    public List<Anime> UnwatchedAnimeList { get; set; } = new List<Anime>();
    public List<Book_Film_Database.Models.Review> ReviewsList { get; set; } = new List<Book_Film_Database.Models.Review>();
    
    public List<Manga> ReadMangaList { get; set; } = new List<Manga>();
    public List<Manga> ReadingMangaList { get; set; } = new List<Manga>();
    public List<Manga> PlanningMangaList { get; set; } = new List<Manga>();
    public List<Manga> DroppedMangaList { get; set; } = new List<Manga>();
    public List<Manga> UnreadMangaList { get; set; } = new List<Manga>();
    public List<Anime> CustomAnimeList { get; set; } = new List<Anime>();
    public List<Manga> CustomMangaList { get; set; } = new List<Manga>();



    
    string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"AppData" ,  "Anime.csv");
    string path2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"AppData" ,  "Manga.csv");
    string UserDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "AppData", "UserData.json");
    
    public void ReadAnimeCSV()
    {
        AnimesList.Clear();
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
        Console.WriteLine($"Reading CSV from {path}");
        using (TextFieldParser parser = new TextFieldParser(path))
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
                                    if (!(WatchedAnimeList.Contains(AnimesList[Rank - 1]) ||
                                          WatchingAnimeList.Contains(AnimesList[Rank - 1]) ||
                                          PlanningAnimeList.Contains(AnimesList[Rank - 1]) ||
                                          DroppedAnimeList.Contains(AnimesList[Rank - 1])))
                                    {
                                        UnwatchedAnimeList.Add(AnimesList[Rank - 1]);
                                    }
                                    Console.WriteLine($" {Rank}");
                                    column = 0;
                                    break;
                            }
                            
                        }    
                    }
            }
        }
    }
    public void ReadMangaCSV()
    {
        MangaList.Clear();
        int columnM = 0;
        int FieldCountM = 0;
        string NameM = "";
        string GenreM = "";
        string DescriptionM = "";
        string RatingM = "";
        Console.WriteLine($"Reading CSV from {path2}");
        using (TextFieldParser parser = new TextFieldParser(path2))
        {
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
            while (!parser.EndOfData)
            {
                    string[] fields = parser.ReadFields();
                    foreach (string field in fields)
                    {
                        FieldCountM++;
                        if (FieldCountM > 6)
                        {
                            Console.WriteLine(columnM);
                            columnM++;
                            switch (columnM)
                            {
                                case 1: 
                                    NameM = field;
                                    break;
                                case 2:
                                    DescriptionM = field;
                                    break;
                                case 3:
                                    RatingM = field;
                                    break;
                                case 5:
                                    GenreM = field;
                                    break;
                                case 6:
                                    
                                    MangaList.Add(new Manga {Name = NameM, Description = DescriptionM, Rating = RatingM, Genre = GenreM});
                                    columnM = 0;
                                    break;
                            }
                            
                        }    
                    }
            }
        }
    }

    public class UserDataStorage
    {
        public List<Anime> FavoritesAnime { get; set; } = new List<Anime>();
        public List<Manga> FavoritesManga { get; set; } = new List<Manga>();
        public List<Book_Film_Database.Models.Review> Reviews { get; set; } = new List<Book_Film_Database.Models.Review>();
        public List<Anime> WatchedAnime { get; set; } = new List<Anime>();
        public List<Anime> WatchingAnime { get; set; } = new List<Anime>();
        public List<Anime> PlanningAnime { get; set; } = new List<Anime>();
        public List<Anime> DroppedAnime { get; set; } = new List<Anime>();
        public List<Anime> CustomAnimeList { get; set; } = new List<Anime>();
        public List<Manga> CustomMangaList { get; set; } = new List<Manga>();
    }
    public void SaveUserData()
    {
        try
        {
            string directory = Path.GetDirectoryName(UserDataPath);
            if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

            var package = new UserDataStorage()
            {
                FavoritesAnime = this.FavoritesAnimeList,
                FavoritesManga = this.FavoritesMangaList,
                Reviews = this.ReviewsList,
                WatchedAnime = this.WatchedAnimeList,
                WatchingAnime = this.WatchingAnimeList,
                PlanningAnime = this.PlanningAnimeList,
                DroppedAnime = this.DroppedAnimeList,
            };

            string json = System.Text.Json.JsonSerializer.Serialize(package, new System.Text.Json.JsonSerializerOptions 
            { 
                WriteIndented = true 
            });

            File.WriteAllText(UserDataPath, json);
            Console.WriteLine("Data has been saved.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public void LoadUserData()
    {
        try
        {
            if (File.Exists(UserDataPath))
            {
                string json = File.ReadAllText(UserDataPath);
                var package = System.Text.Json.JsonSerializer.Deserialize<UserDataStorage>(json);

                if (package != null)
                {
                    this.FavoritesAnimeList = package.FavoritesAnime ?? new List<Anime>();
                    this.FavoritesMangaList = package.FavoritesManga ?? new List<Manga>();
                    this.ReviewsList = package.Reviews ?? new List<Book_Film_Database.Models.Review>();
                    this.WatchedAnimeList = package.WatchedAnime ?? new List<Anime>();
                    this.WatchingAnimeList = package.WatchingAnime ?? new List<Anime>();
                    this.PlanningAnimeList = package.PlanningAnime ?? new List<Anime>();
                    this.DroppedAnimeList = package.DroppedAnime ?? new List<Anime>();
                    
                    foreach (var anime in AnimesList)
                    {
                        bool isFav = FavoritesAnimeList.Any(f => f.Name == anime.Name);
                    
                        if (isFav)
                        {
                            anime.IsFavorite = true; 
                        }
                    }
                    foreach (var manga in MangaList)
                    {
                        bool isFav = FavoritesMangaList.Any(f => f.Name == manga.Name);
                        if (isFav)
                        {
                            manga.IsFavorite = true; // uprav název podle své třídy Manga
                        }
                    }
                    foreach (var anime in CustomAnimeList)
                    {
                        if (!AnimesList.Any(a => a.Name == anime.Name)) AnimesList.Add(anime);
                    }

                    foreach (var manga in CustomMangaList)
                    {
                        if (!MangaList.Any(m => m.Name == manga.Name)) MangaList.Add(manga);
                    }
                    
                    Console.WriteLine("User data loaded.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public void ResetAllUserData()
    {
        try
        {
            FavoritesAnimeList.Clear();
            FavoritesMangaList.Clear();
            ReviewsList.Clear();

            if (File.Exists(UserDataPath))
            {
                File.Delete(UserDataPath);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}