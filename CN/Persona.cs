using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN
{
    public class Persona
    {

        #region Propiedades

        private string _nombreCompleto; //Propiedad FULL de lectura y escritura establece un get y un set para guardar la informacion en la variable privada - propfull

        public string nombreCompleto 
        {
            get { return _nombreCompleto; }
            //set { _nombreCompleto = value; } //se quitó set para agregar a traves de constructores
        }

        private DateTime _fechaCreacion;

        public DateTime fechaCreacion
        {
            get { return _fechaCreacion; }
            //set { _fechaCreacion = value; }
        }

        public int edad // Propiedad de solo lectura tiene un get pero no un set para la variable
        {
            get 
            {
                DateTime hoy = DateTime.Today;
                    int edad = hoy.Year - fechaCreacion.Year;

                if (hoy < fechaCreacion.AddYears(edad)) // se agrego if para calcular edad tomando en cuenta la fecha de nacimiento.
                {
                    edad--;
                }
                return edad;
            }
        }

        public bool esActivo { get; set; } //Propiedad de implementacion automatica - prop - disponible en nuevas versiones de C#

        #endregion

        #region Constructores

        public Persona(string _nombreCompleto, bool _esActivo, DateTime _fechaCreacion)
        {
            this._nombreCompleto = _nombreCompleto;
            this.esActivo = _esActivo; //notar que no lleva guion bajo despues de this, esto es porque no es una propiedad full
            this._fechaCreacion = _fechaCreacion;
        }

        #endregion


    }
}
