using CsvHelper;
using CsvHelper.Configuration;
using Recursiva.Models;
using System.Globalization;

namespace Recursiva.Services
{
    public class CsvService : ICsvService
    {
        private IEnumerable<Socio> _socios;
        private readonly CsvConfiguration _config;
        public CsvService()
        {
            _config = new CsvConfiguration(CultureInfo.CurrentCulture)
            {
                HasHeaderRecord = false,
                Delimiter = ";"
            };

        }
        public void LoadFile(StreamReader stream)
        {
            using (var csv = new CsvReader(stream, _config))
            {
                _socios = csv.GetRecords<Socio>().ToList();
            }
                
        }
        public int GetTotalRegistrados()
        {
            return _socios.Count();
        }

        public float GetPromedioPorEquipo(string equipo)
        {
            
            var avg = (float)_socios.Where(s => s.Equipo == equipo).Average(s => s.Edad);
            return avg;
        }

        public IEnumerable<Socio> GetCasadosUniversitarios(int take)
        {
            
            var results = _socios
                .Where(s => s.EstadoCivil == "Casado")
                .Where(s => s.NivelDeEstudios == "Universitario")
                .OrderByDescending(s => s.Edad);
            return results.Take(take).ToList();
        }

        public IEnumerable<string> GetNombresComunesPorEquipo(string equipo, int take)
        {
            var names = _socios
                .Where(s => s.Equipo == equipo)
                .GroupBy(s => s.Nombre)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key);
            return names.Take(take).ToList();
        }

        public IEnumerable<int> GetPromedioEdades()
        {
            var promedio = _socios
                .GroupBy(s => s.Equipo)
                .Select(g => (int)g.Average(s => s.Edad));
            return promedio.ToList();
        }
    }
}
