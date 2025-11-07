namespace Cinema.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Genre { get; set; }
        public int Duration { get; set; }
        public string? AgeRating { get; set; }
    }
}
