using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookStoreSys_API.Domain.Model
{
    [Table("Author")]
    public class AuthorModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DayOfBirth { get; set; }
        [JsonIgnore]
        public int NationalityId { get; set; }

        [ForeignKey("NationalityId")]
        public NationalityModel? Nationality { get; set; }
        [JsonIgnore]
        public HashSet<BookModel> Books { get; set; } = new();
    }
}
