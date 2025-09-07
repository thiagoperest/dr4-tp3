using CityBreaks.Web.Model;
using CityBreaks.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CityBreaks.Web.Pages
{
    public class CreatePropertyModel : PageModel
    {
        private readonly IPropertyService _propertyService;
        private readonly ICityService _cityService;

        public CreatePropertyModel(IPropertyService propertyService, ICityService cityService)
        {
            _propertyService = propertyService;
            _cityService = cityService;
        }

        [BindProperty]
        public PropertyInputModel Input { get; set; } = new();

        public SelectList? Cities { get; set; }
        
        [TempData]
        public string? SuccessMessage { get; set; }
        
        [TempData]
        public string? ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            Cities = await _cityService.GetCitiesSelectListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Cities = await _cityService.GetCitiesSelectListAsync();

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var property = new Property
            {
                Name = Input.Name,
                PricePerNight = Input.PricePerNight,
                CityId = Input.CityId
            };

            var success = await _propertyService.CreateAsync(property);

            if (success)
            {
                SuccessMessage = $"Propriedade '{Input.Name}' cadastrada com sucesso!";
                return RedirectToPage();
            }
            else
            {
                ErrorMessage = "Erro ao cadastrar a propriedade. Tente novamente.";
                return Page();
            }
        }
    }

    public class PropertyInputModel
    {
        [Required(ErrorMessage = "O nome da propriedade é obrigatório.")]
        [StringLength(250, ErrorMessage = "O nome deve ter no máximo 250 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "O preço por noite é obrigatório.")]
        [Range(0.01, 10000, ErrorMessage = "O preço deve estar entre R$ 0,01 e R$ 10.000,00.")]
        public decimal PricePerNight { get; set; }

        [Required(ErrorMessage = "Selecione uma cidade.")]
        public int CityId { get; set; }
    }
}