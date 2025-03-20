using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookStoreSys_API.Domain.Models
{
    [Table("Genre")]
    public class GenreModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        public HashSet<BookModel> Books { get; set; } = new();
    }
}
