using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStoreSys_API.Domain.BO
{
    [Table("Nationality")]
    public class NationalityModel
    {
        [Key]
        public int Id { get; set; }
        public string Country { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;

        public HashSet<AuthorModel> Authors { get; set; } = new ();
    }
}
