using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreSys_API.Domain.BO
{
    [Table("Genre")]
    public class GenreModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public HashSet<BookModel> Books { get; set; } = new();
    }
}
