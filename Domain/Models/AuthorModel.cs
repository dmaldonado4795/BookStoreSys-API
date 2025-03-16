using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreSys_API.Domain.BO
{
    [Table("Author")]
    public class AuthorModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DayOfBirth { get; set; }

        [ForeignKey("NationalityId")]
        public int NationalityId { get; set; }

        public NationalityModel? Nationality { get; set; }
        public HashSet<BookModel> Books { get; set; } = new();
    }
}
