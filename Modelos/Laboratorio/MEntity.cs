namespace Modelos.Laboratorio
{
    public class MEntity
    {
        public int ResEquipoId { get; set; }

        public DateTime ResFecha { get; set; }

        public DateTime ResHora { get; set; }

        public string ResTurno { get; set; }

        public string ResAbrev { get; set; }

        public string ResEstado { get; set; }

        public string ResResultado { get; set; }

        public string ResError { get; set; }

        public decimal ResConvertido { get; set; }
    }
}
