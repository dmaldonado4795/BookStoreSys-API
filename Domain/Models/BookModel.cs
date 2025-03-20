using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookStoreSys_API.Domain.Models
{
    [Table("Book")]
    public class BookModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateOnly bookSaved { get; set; }
        [JsonIgnore]
        public int AuthorId { get; set; }
        [JsonIgnore]
        public int GenreId { get; set; }

        [ForeignKey("GenreId")]
        public GenreModel? Genre { get; set; }
        [ForeignKey("AuthorId")]
        public AuthorModel? Author { get; set; }
    }
}
