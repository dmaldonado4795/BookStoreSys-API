using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BookStoreSys_API.Domain.Models
{
    [Table("Nationality")]
    public class NationalityModel
    {
        [Key]
        public int Id { get; set; }
        public string Country { get; set; } = string.Empty;
        public string Nationality { get; set; } = string.Empty;

        [JsonIgnore]
        public HashSet<AuthorModel> Authors { get; set; } = new ();
    }
}
