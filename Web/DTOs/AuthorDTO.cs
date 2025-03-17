namespace BookStoreSys_API.Web.DTOs
{
    public class AuthorDTO
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DayOfBirth { get; set; } = string.Empty;
        public int NationalityId { get; set; }
    }
}
