using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaXpert.Model
{
    public class DetalleNomina
    {
        public int Id { get; set; }
        public int IdNomina { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }
        public decimal Monto { get; set; }

        // Constructor predeterminado
        public DetalleNomina()
        {
            Descripcion = string.Empty;
            Tipo = string.Empty; // Dejarlo vacío o usar un valor por defecto que puede ser asignado dinámicamente
            Monto = 0.00m;
        }

        // Constructor con parámetros
        public DetalleNomina(int idNomina, string descripcion, string tipo, decimal monto)
        {
            IdNomina = idNomina;
            Descripcion = descripcion;
            Tipo = tipo;
            Monto = monto;
        }

        // Constructor con todos los campos
        public DetalleNomina(int id, int idNomina, string descripcion, string tipo, decimal monto)
        {
            Id = id;
            IdNomina = idNomina;
            Descripcion = descripcion;
            Tipo = tipo;
            Monto = monto;
        }
    }


}
