using Data.Comandos;
using Modelos.Laboratorio;

namespace Controller.Negocio
{
    public class NEquipoLaboratorio
    {
        public static IEnumerable<MEquipoLaboratorio> GetEquiposLaboratorio()
        {
            return EquipoLaboratorioCommands.GetEquiposLaboratorio();
        }
        public static object[] GuardarEquipoLaboratorio(MEquipoLaboratorio unidad, IEnumerable<MDetalleEquipoLaboratorio> examenes, bool isNew)
        {
            if (string.IsNullOrEmpty(unidad.Nombre)) return new object[] { 1, "Por favor, ingrese el detalle del equipo", null };

            var r = EquipoLaboratorioCommands.SaveEquipoLaboratorio(unidad, examenes, isNew);

            if ((int)r[0] == 1 && r[1].ToString().Contains("23505"))
                return new object[] { 1, "La equipo que trata de ingresar ya está registrado", unidad };

            return new[] { r[0], r[1], unidad };
        }
    }
}
