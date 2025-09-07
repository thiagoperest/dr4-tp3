using CityBreaks.Web.Model;
using CityBreaks.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CityBreaks.Web.Pages
{
    public class EditPropertyModel : PageModel
    {
        private readonly IPropertyService _propertyService;
        private readonly ICityService _cityService;

        public EditPropertyModel(IPropertyService propertyService, ICityService cityService)
        {
            _propertyService = propertyService;
            _cityService = cityService;
        }

        [BindProperty]
        public PropertyEditModel Input { get; set; } = new();

        public SelectList? Cities { get; set; }
        
        [TempData]
        public string? SuccessMessage { get; set; }
        
        [TempData]
        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var property = await _propertyService.GetByIdAsync(id);

            if (property == null)
            {
                return NotFound();
            }

            Input = new PropertyEditModel
            {
                Id = property.Id,
                Name = property.Name,
                PricePerNight = property.PricePerNight,
                CityId = property.CityId,
                CurrentCityName = property.City?.Name,
                CurrentCountryName = property.City?.Country?.CountryName
            };

            Cities = await _cityService.GetCitiesSelectListAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                Cities = await _cityService.GetCitiesSelectListAsync();
                return Page();
            }

            var success = await _propertyService.UpdateAsync(
                Input.Id, 
                Input.Name, 
                Input.PricePerNight, 
                Input.CityId);

            if (success)
            {
                SuccessMessage = $"Propriedade '{Input.Name}' atualizada com sucesso!";
                return RedirectToPage(new { id = Input.Id });
            }
            else
            {
                ErrorMessage = "Erro ao atualizar a propriedade. Tente novamente.";
                Cities = await _cityService.GetCitiesSelectListAsync();
                return Page();
            }
        }
    }

    public class PropertyEditModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome da propriedade é obrigatório.")]
        [StringLength(250, ErrorMessage = "O nome deve ter no máximo 250 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O preço por noite é obrigatório.")]
        [Range(0.01, 10000, ErrorMessage = "O preço deve estar entre R$ 0,01 e R$ 10.000,00.")]
        public decimal PricePerNight { get; set; }

        [Required(ErrorMessage = "Selecione uma cidade.")]
        public int CityId { get; set; }

        // Propriedades auxiliares para exibição
        public string? CurrentCityName { get; set; }
        public string? CurrentCountryName { get; set; }
    }
}