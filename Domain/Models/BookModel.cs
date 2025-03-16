using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreSys_API.Domain.BO
{
    [Table("Book")]
    public class BookModel
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [ForeignKey("AuthorId")]
        public int AuthorId { get; set; }
        [ForeignKey("GenreId")]
        public int GenreId { get; set; }

        public GenreModel? Genre { get; set; }
        public AuthorModel? Author { get; set; }
    }
}
