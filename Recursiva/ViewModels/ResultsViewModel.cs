using Recursiva.Models;

namespace Recursiva.ViewModels
{
    public class ResultsViewModel
    {
        public int TotalRegistrados { get; set; }
        public float PromedioEdad { get; set; }
        public IEnumerable<CasadosUniversitarios> CasadosUniversitarios { get; set; } = new List<CasadosUniversitarios>();
        public IEnumerable<string> NombresMasComunes { get; set; } = new List<string>();
        public IEnumerable<InfoEdades> PromedioEdades { get; set; } = new List<InfoEdades>();

    }
}
