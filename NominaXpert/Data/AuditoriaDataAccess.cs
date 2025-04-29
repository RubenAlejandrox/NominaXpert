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
    class AuditoriaDataAccess
    {
        // Logger para la clase
        private static readonly Logger _logger = LoggingManager.GetLogger("NominaXpert.Data.AuditoriaDataAccess");

        // Instancia del acceso a datos de PostgreSQL
        private readonly PostgresSQLDataAccess _dbAccess;

        /// <summary>
        /// Constructor de la clase AuditoriaDataAccess
        /// </summary>
        /// 
        // Registrar Auditoría
        public void RegistrarAuditoria(int idUsuario, string accion, string detalleAccion)
        {
            string query = @"
        INSERT INTO seguridad.auditorias (id_usuario, accion, detalle_accion, fecha, ip_acceso, nombre_equipo)
        VALUES (@idUsuario, @accion, @detalleAccion, @fecha, @ipAcceso, @nombreEquipo)";

            try
            {
                NpgsqlParameter[] parameters = new NpgsqlParameter[]
                {
            _dbAccess.CreateParameter("@idUsuario", idUsuario),
            _dbAccess.CreateParameter("@accion", accion),
            _dbAccess.CreateParameter("@detalleAccion", detalleAccion),
            _dbAccess.CreateParameter("@fecha", DateTime.Now),
            _dbAccess.CreateParameter("@ipAcceso", "192.168.0.1"), // IP estática o dinámica según implementación
            _dbAccess.CreateParameter("@nombreEquipo", "Nombre del equipo") // Este también se obtiene dinámicamente
                };

                // Conexión a la base de datos y ejecución de la consulta
                _dbAccess.Connect();
                _dbAccess.ExecuteNonQuery(query, parameters);

                _logger.Info($"Auditoría registrada: {accion} - {detalleAccion}");
            }
            catch (Exception ex)
            {
                // Si hay un error, lo registramos en el log
                _logger.Error(ex, "Error al registrar la auditoría.");
                throw;
            }
            finally
            {
                // Desconectamos de la base de datos
                _dbAccess.Disconnect();
            }
        }
    }
}