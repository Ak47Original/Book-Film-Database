namespace Book_Film_Database.Models;

public class Review
{
    public string Title { get; set; } = "";
    public string Genre { get; set; } = "";
    public int Rating { get; set; }
    public string Text { get; set; } = "";
}