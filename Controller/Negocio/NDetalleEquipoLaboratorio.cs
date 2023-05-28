using Data.Comandos;
using Modelos.Laboratorio;

namespace Controller.Negocio
{
    public class NDetalleEquipoLaboratorio
    {
        public static IEnumerable<MDetalleEquipoLaboratorio> GetDetalleEquipoLaboratorio()
        {
            return DetalleEquipoLaboratorioCommands.GetDetalleEquipoLaboratorio();
        }
        public static IEnumerable<MDetalleEquipoLaboratorio> GetDetalleEquipoLaboratorioByIdEquipo(string idp)
        {
            return DetalleEquipoLaboratorioCommands.GetDetalleEquipoLaboratorioByIdEquipo(idp);
        }
        public static object[] EliminarDetalleEquipoLaboratorio(MDetalleEquipoLaboratorio examen)
        {
            var r = DetalleEquipoLaboratorioCommands.EliminarDetalleEquipoLaboratoiro(examen);
            return new[] { r[0], r[1], examen };
        }
    }
}
