namespace Recursiva.Models
{
    public class ResultsViewModel
    {
        public int TotalRegistrados { get; set; }
        public float PromedioEdad { get; set; }
        public IEnumerable<Socio> CasadosUniversitarios { get; set; }
        public IEnumerable<string> NombresMasComunes { get; set; }
        public IEnumerable<InfoEdades> PromedioEdades { get; set; }

    }
}
