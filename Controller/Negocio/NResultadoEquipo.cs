using Data.Comandos;
using Modelos.Laboratorio;

namespace Controller.Negocio
{
    public class NResultadoEquipo
    {
        public static string GuardarResultadoEquipo(IEnumerable<MResultadoEquipo> resultados)
        {
            return ResultadoEquipoCommands.SaveResultadoEquipo(resultados);
        }
        public static IEnumerable<MResultadoEquipo> GetResultadosOrdenByNumOrden(string numOrden)
        {
            return ResultadoEquipoCommands.GetResultadosOrdenByNumOrden(numOrden);
        }
        public static IEnumerable<MResultadoEquipo> GetResultadosObtenidosByFecha(DateTime fecha)
        {
            return ResultadoEquipoCommands.GetResultadosObtenidosByFecha(fecha);
        }
    }
}
