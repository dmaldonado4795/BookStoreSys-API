namespace BookStoreSys_API.Web.DTOs
{
    public class BookDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PublicationDate { get; set; } = string.Empty;
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
    }
}
