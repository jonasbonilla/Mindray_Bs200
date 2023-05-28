using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Laboratorio
{
    public class MEquipoLaboratorio : NotifyPropertyChangedBase
    {
        private string _idCargo;
        public string IdEquipo
        {
            get { return _idCargo; }
            set
            {
                _idCargo = value;
                OnPropertyChanged("IdEquipo");
            }
        }
        private string _idNombre;
        public string Nombre
        {
            get { return _idNombre; }
            set
            {
                _idNombre = value;
                OnPropertyChanged("Nombre");
            }
        }

        private bool _mtipoCargo;
        public bool Estado
        {
            get { return _mtipoCargo; }
            set
            {
                _mtipoCargo = value;
                OnPropertyChanged("Estado");
            }
        }
        private int _pue;
        public int Puerto
        {
            get { return _pue; }
            set
            {
                _pue = value;
                OnPropertyChanged("Puerto");
            }
        }
        public string EstadoSiNo => Estado ? "SI" : "NO";
    }
}
