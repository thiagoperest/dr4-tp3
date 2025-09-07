using CityBreaks.Web.Model;
using CityBreaks.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace CityBreaks.Web.Pages
{
    public class FilterPropertiesModel : PageModel
    {
        private readonly IPropertyService _propertyService;

        public FilterPropertiesModel(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [BindProperty]
        public FilterCriteria Filters { get; set; } = new();

        public List<Property> Properties { get; set; } = new();
        public bool HasSearched { get; set; }
        public string AppliedFiltersText { get; set; } = string.Empty;

        public void OnGet()
        {
            // Página inicial - sem resultados
        }

        public async Task OnPostAsync()
        {
            HasSearched = true;
            
            Properties = await _propertyService.GetFilteredAsync(
                Filters.MinPrice,
                Filters.MaxPrice,
                Filters.CityName,
                Filters.PropertyName);

            var appliedFilters = new List<string>();

            if (Filters.MinPrice.HasValue)
                appliedFilters.Add($"Preço mínimo: {Filters.MinPrice:C}");

            if (Filters.MaxPrice.HasValue)
                appliedFilters.Add($"Preço máximo: {Filters.MaxPrice:C}");

            if (!string.IsNullOrWhiteSpace(Filters.CityName))
                appliedFilters.Add($"Cidade: {Filters.CityName}");

            if (!string.IsNullOrWhiteSpace(Filters.PropertyName))
                appliedFilters.Add($"Propriedade: {Filters.PropertyName}");

            AppliedFiltersText = appliedFilters.Any() 
                ? string.Join(" | ", appliedFilters)
                : "Nenhum filtro aplicado";
        }
    }

    public class FilterCriteria
    {
        [Range(0, 10000, ErrorMessage = "O preço mínimo deve estar entre R$ 0 e R$ 10.000")]
        public decimal? MinPrice { get; set; }

        [Range(0, 10000, ErrorMessage = "O preço máximo deve estar entre R$ 0 e R$ 10.000")]
        public decimal? MaxPrice { get; set; }

        [StringLength(100, ErrorMessage = "O nome da cidade deve ter no máximo 100 caracteres")]
        public string? CityName { get; set; }

        [StringLength(250, ErrorMessage = "O nome da propriedade deve ter no máximo 250 caracteres")]
        public string? PropertyName { get; set; }
    }
}