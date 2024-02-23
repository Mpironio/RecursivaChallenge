using Microsoft.AspNetCore.Mvc;
using Recursiva.Models;
using System.Diagnostics;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using Recursiva.Services;

namespace Recursiva.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICsvService _csvService;
        public HomeController(ILogger<HomeController> logger, ICsvService csvService)
        {
            _logger = logger;
            _csvService = csvService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Data(IFormFile file)
        {
            ResultsViewModel results = new ResultsViewModel();

            using (var sr = new StreamReader(file.OpenReadStream()))
            {
                _csvService.LoadFile(sr);

                results.TotalRegistrados = _csvService.GetTotalRegistrados();
                results.PromedioEdad = _csvService.GetPromedioPorEquipo("River");
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
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }


}
