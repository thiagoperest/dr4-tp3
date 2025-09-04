using System.ComponentModel.DataAnnotations;

namespace CityBreaks.Web.Models
{
    public class Country
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [StringLength(10)]
        public string Code { get; set; } = string.Empty;
    }
}