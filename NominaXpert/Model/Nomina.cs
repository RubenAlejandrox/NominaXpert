using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaXpert.Model
{
    public class Nomina
    {
        public int Id { get; set; }
        public int IdEmpleado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string EstadoPago { get; set; }
        public DateTime CreadoAt { get; set; }

        // Constructor predeterminado
        public Nomina()
        {
            EstadoPago = "Pendiente";
            CreadoAt = DateTime.Now;
        }

        // Constructor con parámetros
        public Nomina(int idEmpleado, DateTime fechaInicio, DateTime fechaFin)
        {
            IdEmpleado = idEmpleado;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            EstadoPago = "Pendiente"; // Inicialmente la nómina está pendiente
            CreadoAt = DateTime.Now;
        }

        // Constructor completo
        public Nomina(int id, int idEmpleado, DateTime fechaInicio, DateTime fechaFin, string estadoPago, DateTime creadoAt)
        {
            Id = id;
            IdEmpleado = idEmpleado;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            EstadoPago = estadoPago;
            CreadoAt = creadoAt;
        }
    }


}
