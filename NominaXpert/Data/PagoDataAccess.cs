using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlEscolar.Data;
using ControlEscolar.Utilities;
using NLog;
using NominaXpert.Model;
using Npgsql;

namespace NominaXpert.Data
{
    class PagoDataAccess
    {
        // Logger para la clase
        private static readonly Logger _logger = LoggingManager.GetLogger("NominaXpert.Data.PagoDataAccess");

        // Instancia del acceso a datos de PostgreSQL
        private readonly PostgresSQLDataAccess _dbAccess;

        /// <summary>
        /// Constructor de la clase PagoDataAccess
        /// </summary>
        /// 
        // Registrar Pago
        public void RegistrarPago(Pago pago)
        {
            string query = @"
            INSERT INTO nomina.pagos (id_nomina, fecha_pago, monto_total, monto_letras, metodo_pago, referencia)
            VALUES (@idNomina, @fechaPago, @montoTotal, @montoLetras, @metodoPago, @referencia)";

            try
            {
                NpgsqlParameter[] parameters = new NpgsqlParameter[]
                {
                _dbAccess.CreateParameter("@idNomina", pago.IdNomina),
                _dbAccess.CreateParameter("@fechaPago", pago.FechaPago),
                _dbAccess.CreateParameter("@montoTotal", pago.MontoTotal),
                _dbAccess.CreateParameter("@montoLetras", pago.MontoLetras),
                _dbAccess.CreateParameter("@metodoPago", pago.MetodoPago),
                _dbAccess.CreateParameter("@referencia", pago.Referencia)
                };

                _dbAccess.Connect();
                _dbAccess.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al registrar el pago.");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }
    }
}
