using CityBreaks.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CityBreaks.Web.Pages
{
    public class DeletePropertyModel : PageModel
    {
        private readonly IPropertyService _propertyService;

        public DeletePropertyModel(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var property = await _propertyService.GetByIdAsync(id);
            
            if (property == null)
            {
                TempData["ErrorMessage"] = "Propriedade não encontrada.";
                return RedirectToPage("/Index");
            }

            var cityName = property.City?.Name ?? "";
            var success = await _propertyService.DeleteAsync(id);

            if (success)
            {
                TempData["SuccessMessage"] = $"Propriedade '{property.Name}' foi excluída com sucesso.";
            }
            else
            {
                TempData["ErrorMessage"] = "Erro ao excluir a propriedade.";
            }

            if (!string.IsNullOrEmpty(cityName))
            {
                return RedirectToPage("/CityDetails", new { name = cityName });
            }

            return RedirectToPage("/Index");
        }
    }
}