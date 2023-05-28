namespace Modelos.Laboratorio
{
    public class MDetalleEquipoLaboratorio : NotifyPropertyChangedBase
    {
        private MEquipoLaboratorio _idCargo;
        public MEquipoLaboratorio Equipo
        {
            get { return _idCargo; }
            set
            {
                _idCargo = value;
                OnPropertyChanged("Equipo");
            }
        }
        private string _idCeargo;
        public string IdDetalle
        {
            get { return _idCeargo; }
            set
            {
                _idCeargo = value;
                OnPropertyChanged("IdDetalle");
            }
        }
        private string _abr;
        public string CodigoLis
        {
            get { return _abr; }
            set
            {
                _abr = value;
                OnPropertyChanged("CodigoLis");
            }
        }
    }
}
