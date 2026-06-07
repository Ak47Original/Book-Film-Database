namespace Book_Film_Database.Models;


public abstract class Product
{

}

public class Anime : Product
{
    public int Ranks { get; set; }
    public string Name { get; set; }
    public string JapaneseName { get; set; }
    public string Genre { get; set; }
    public string Description { get; set; }
    public string RelatedAnime { get; set; }
    public string RelatedManga { get; set; }
    public string Rating { get; set; }
    public bool IsFavorite { get; set; } = false;
}

public class Manga : Product
{
    public string Name { get; set; }
    public string Genre { get; set; }
    public string Description { get; set; }
    public string Rating { get; set; }
}