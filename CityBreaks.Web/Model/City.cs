using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityBreaks.Web.Model
{
    public class City
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        public int CountryId { get; set; }
        
        [ForeignKey("CountryId")]
        public Country Country { get; set; } = null!;

        public List<Property> Properties { get; set; } = new List<Property>();
    }
}