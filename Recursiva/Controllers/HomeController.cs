using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using Recursiva.Services;
using Recursiva.ViewModels;
using System.Text;

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
                return RedirectToAction("Error", new Exception("Tipo de archivo incorrecto"));
            }

            ResultsViewModel results = new();

            using (var sr = new StreamReader(file.OpenReadStream(), Encoding.Latin1, false))
            {
                try {                     
                    _csvService.LoadFile(sr);
                }
                catch (Exception e)
                {
                    return RedirectToAction("Error");
                }
                


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

            return View();
        }
    }
}
