using Recursiva.Models;

namespace Recursiva.Services
{
    public interface ICsvService
    {
        void LoadFile(StreamReader stream);
        int GetTotalRegistrados();
        float GetPromedioPorEquipo(string equipo);
        IEnumerable<Socio> GetCasadosUniversitarios(int take);
        IEnumerable<string> GetNombresComunesPorEquipo(string equipo, int take);
        IEnumerable<int> GetPromedioEdades();

    }
}