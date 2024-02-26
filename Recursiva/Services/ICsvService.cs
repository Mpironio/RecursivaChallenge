using Recursiva.Entities;
using Recursiva.Models;

namespace Recursiva.Services
{
    public interface ICsvService
    {
        void LoadFile(StreamReader stream);
        int GetTotalRegistrados();
        float GetPromedioEdadPorEquipo(string equipo);
        IEnumerable<CasadosUniversitarios> GetCasadosUniversitarios(int take);
        IEnumerable<string> GetNombresComunesPorEquipo(string equipo, int take);
        IEnumerable<InfoEdades> GetPromedioEdades();
        IEnumerable<string> GetEquipos();
        IEnumerable<string> GetEstudios();


    }
}