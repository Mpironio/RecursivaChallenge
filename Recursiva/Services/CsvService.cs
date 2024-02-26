using CsvHelper;
using CsvHelper.Configuration;
using Recursiva.Entities;
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
            _socios = new List<Socio>();
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

        public float GetPromedioEdadPorEquipo(string equipo)
        {
            
            var avg = (float)_socios.Where(s => s.Equipo == equipo).Average(s => s.Edad);
            return avg;
        }

        public IEnumerable<CasadosUniversitarios> GetCasadosUniversitarios(int take)
        {
            
            var results = _socios
                .Where(s => s.EstadoCivil == "Casado")
                .Where(s => s.NivelDeEstudios == "Universitario")
                .OrderByDescending(s => s.Edad)
                .Take(take);

            var casadosUniversitarios = results
                .Select(s => new CasadosUniversitarios
                {
                    Nombre = s.Nombre,
                    Equipo = s.Equipo,
                    Edad = s.Edad
                });


            return casadosUniversitarios.ToList();
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

        public IEnumerable<InfoEdades> GetPromedioEdades()
        {
            var equipos = GetEquipos();
            var result = new List<InfoEdades>();

            foreach (var equipo in equipos)
            {
                var promedio = GetPromedioEdadPorEquipo(equipo);

                var max = GetSociosPorEquipo(equipo)
                    .Max(s => s.Edad);

                var min = GetSociosPorEquipo(equipo)
                    .Min(s => s.Edad);

                result.Add(new InfoEdades
                {
                    Equipo = equipo,
                    PromedioEdad = promedio,
                    MaxEdad = max,
                    MinEdad = min
                });
            }

            result = result
                .OrderBy(x => GetSociosPorEquipo(x.Equipo).Count())
                .ToList();

            return result;
        }


        public IEnumerable<string> GetEquipos()
        {
            var equipos = _socios
                .Select(s => s.Equipo)
                .Distinct();
            return equipos.ToList();
        }

        public IEnumerable<string> GetEstudios()
        {
            var estudios = _socios
                .Select(s => s.NivelDeEstudios)
                .Distinct();
            return estudios.ToList();
        }



        private IEnumerable<Socio> GetSociosPorEquipo(string equipo)
        {
            var socios = _socios
                .Where(s => s.Equipo == equipo);
            return socios;
        }
    }
}
