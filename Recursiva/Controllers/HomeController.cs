using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using Recursiva.Services;
using Recursiva.ViewModels;

namespace Recursiva.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICsvService _csvService;
        public HomeController(ICsvService csvService)
        {
            _csvService = csvService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Data(IFormFile file)
        {
            if(file.ContentType != "text/csv" || file.Length == 0) 
            {
                return RedirectToAction("Error");
            }

            ResultsViewModel results = new();

            using (var sr = new StreamReader(file.OpenReadStream()))
            {
                _csvService.LoadFile(sr);

                results.TotalRegistrados = _csvService.GetTotalRegistrados();
                results.PromedioEdad = _csvService.GetPromedioEdadPorEquipo("Racing");
                results.CasadosUniversitarios = _csvService.GetCasadosUniversitarios(100);
                results.NombresMasComunes = _csvService.GetNombresComunesPorEquipo("River", 5);
                results.PromedioEdades = _csvService.GetPromedioEdades();
            }
            return View(results);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel());
        }
    }
}
