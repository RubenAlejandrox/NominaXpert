using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ControlEscolar.Data;
using ControlEscolar.Utilities;
using NLog;
using NominaXpert.Model;
using NominaXpert.View.UsersControl;
using Npgsql;

namespace NominaXpert.Data
{
    class AuditoriaDataAccess
    {
        // Logger para la clase
        private static readonly Logger _logger = LoggingManager.GetLogger("NominaXpert.Data.AuditoriaDataAccess");

        // Instancia del acceso a datos de PostgreSQL
        private readonly PostgresSQLDataAccess _dbAccess;

        public AuditoriaDataAccess()
        {
            try
            {
                // Obtiene la instancia única de PostgresSQLDataAccess (patrón Singleton)
                _dbAccess = PostgresSQLDataAccess.GetInstance();
                _logger.Info("Instancia de AuditoriaDataAccess creada correctamente.");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al inicializar AuditoriaDataAccess");
                throw;
            }
        }


        /// <summary>
        /// Constructor de la clase AuditoriaDataAccess
        /// </summary>
        /// 
        // Registrar Auditoría
        public void RegistrarAuditoria(int idUsuario, string accion, string detalleAccion)
        {
            string query = @"
        INSERT INTO seguridad.auditorias (id_usuario, accion, detalle_accion, fecha, ip_acceso, nombre_equipo, hora)
        VALUES (@idUsuario, @accion, @detalleAccion, @fecha, @ipAcceso, @nombreEquipo, @hora)";

            try
            {

                // Obtener la IP local de la máquina (puedes ajustarlo para obtener la IP externa si lo deseas)
                string ipAcceso = GetLocalIPAddress();

                // Obtener el nombre del equipo
                string nombreEquipo = Environment.MachineName;
                

                NpgsqlParameter[] parameters = new NpgsqlParameter[]
                {
                    _dbAccess.CreateParameter("@idUsuario", idUsuario),
                    _dbAccess.CreateParameter("@accion", accion),
                    _dbAccess.CreateParameter("@detalleAccion", detalleAccion),
                    _dbAccess.CreateParameter("@fecha",  DateTime.Now.Date),
                    _dbAccess.CreateParameter("@ipAcceso", ipAcceso),  // IP obtenida dinámicamente
                    _dbAccess.CreateParameter("@nombreEquipo", nombreEquipo), // Nombre del equipo obtenido dinámicamente
                    _dbAccess.CreateParameter("@hora", DateTime.Now) // Hora actual
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

        // Función para obtener la IP local de la máquina IPv4
        private string GetLocalIPAddress()
        {
            string localIP = string.Empty;
            // Obtener la dirección IP local
            foreach (var host in System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList)
            {
                // Filtrar solo las direcciones IPv4
                if (host.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    localIP = host.ToString();
                    break; // Salir del bucle una vez que encontramos la primera dirección IPv4
                }
            }
            return localIP;
        }
    }
}