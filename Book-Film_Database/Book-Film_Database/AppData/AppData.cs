using System.Collections.Generic;
using Book_Film_Database.Models;

namespace Book_Film_Database.AppData;

public class AppData
{
    public List<Anime> AnimeList { get; set; } = new List<Anime>();
    public List<Manga> MangaList { get; set; } = new List<Manga>();

    public void AddAnime(Anime NewAnime)
    {
        AnimeList.Add(NewAnime);
    }

    public void AddManga(Manga NewManga)
    {
        MangaList.Add(NewManga);
    }
}