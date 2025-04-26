using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaXpert.Model
{
    public class Pago
    {
        public int Id { get; set; }
        public int IdNomina { get; set; }
        public DateTime FechaPago { get; set; }
        public decimal MontoTotal { get; set; }
        public string MontoLetras { get; set; }
        public string MetodoPago { get; set; }
        public string Referencia { get; set; }

        // Constructor predeterminado
        public Pago()
        {
            FechaPago = DateTime.Now;
            MontoLetras = string.Empty;
            MetodoPago = string.Empty; // Método de pago por defecto
        }

        // Constructor con parámetros
        public Pago(int idNomina, decimal montoTotal, string montoLetras, string metodoPago, string referencia)
        {
            IdNomina = idNomina;
            FechaPago = DateTime.Now;
            MontoTotal = montoTotal;
            MontoLetras = montoLetras;
            MetodoPago = metodoPago;
            Referencia = referencia;
        }

        // Constructor con todos los campos
        public Pago(int id, int idNomina, DateTime fechaPago, decimal montoTotal, string montoLetras, string metodoPago, string referencia)
        {
            Id = id;
            IdNomina = idNomina;
            FechaPago = fechaPago;
            MontoTotal = montoTotal;
            MontoLetras = montoLetras;
            MetodoPago = metodoPago;
            Referencia = referencia;
        }

    }

}
