using System.ComponentModel.DataAnnotations;

namespace CityBreaks.Web.Models
{
    public class Country
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(10)]
        public string CountryCode { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string CountryName { get; set; } = string.Empty;
    }
}