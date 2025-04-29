using System;
using System.Collections.Generic;
using System.Data;
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
        public class NominaDataAccess
        {
            // Logger para la clase
            private static readonly Logger _logger = LoggingManager.GetLogger("NominaXpert.Data.NominaDataAccess");

            // Instancia del acceso a datos de PostgreSQL
            private readonly PostgresSQLDataAccess _dbAccess;

            /// <summary>
            /// Constructor de la clase NominasDataAccess
            /// </summary>

            public NominaDataAccess()
            {
                try
                {
                    // Obtiene la instancia única de PostgresSQLDataAccess (patrón Singleton)
                    _dbAccess = PostgresSQLDataAccess.GetInstance();
                    _logger.Info("Instancia de NominaDataAccess creada correctamente.");
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Error al inicializar NominaDataAccess");
                    throw;
                }
            }

        // Crear una nueva nómina
        public void RegistrarNomina(int idEmpleado, int idAuditoria, DateTime periodoInicio, DateTime periodoFin)
        {
            string estadoPago = "Pendiente";  // El estado inicial por defecto

            string query = @"
                INSERT INTO nomina.nomina (id_empleado, id_auditoria, periodo_inicio, periodo_fin, estado_pago, creado_at)
                VALUES (@idEmpleado, @idAuditoria, @periodoInicio, @periodoFin, @estadoPago, @creadoAt)";

            try
            {
                NpgsqlParameter[] parameters = new NpgsqlParameter[]
                {
                    _dbAccess.CreateParameter("@idEmpleado", idEmpleado),
                    _dbAccess.CreateParameter("@idAuditoria", idAuditoria),
                    _dbAccess.CreateParameter("@periodoInicio", periodoInicio),
                    _dbAccess.CreateParameter("@periodoFin", periodoFin),
                    _dbAccess.CreateParameter("@estadoPago", estadoPago),
                    _dbAccess.CreateParameter("@creadoAt", DateTime.Now)
                };

                _dbAccess.Connect();
                _dbAccess.ExecuteNonQuery(query, parameters);
                _logger.Info($"Nómina registrada correctamente para el empleado {idEmpleado}.");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al registrar la nómina.");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }
        // Obtener todas las nóminas
        public List<Nomina> ObtenerTodasLasNominas()
        {
            List<Nomina> nominas = new List<Nomina>();

            string query = @"
                SELECT id, id_empleado, id_auditoria, periodo_inicio, periodo_fin, estado_pago, creado_at
                FROM nomina.nomina
                ORDER BY id";

            try
            {
                _dbAccess.Connect();
                DataTable resultado = _dbAccess.ExecuteQuery_Reader(query);

                foreach (DataRow row in resultado.Rows)
                {
                    Nomina nomina = new Nomina
                    {
                        Id = Convert.ToInt32(row["id"]),
                        IdEmpleado = Convert.ToInt32(row["id_empleado"]),
                        IdAuditoria = Convert.ToInt32(row["id_auditoria"]),
                        FechaInicio = Convert.ToDateTime(row["periodo_inicio"]),
                        FechaFin = Convert.ToDateTime(row["periodo_fin"]),
                        EstadoPago = row["estado_pago"].ToString(),
                        CreadoAt = Convert.ToDateTime(row["creado_at"])
                    };
                    nominas.Add(nomina);
                }

                _logger.Info($"Se obtuvieron {nominas.Count} nóminas.");
                return nominas;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener las nóminas.");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        // Actualizar una nómina existente
        public int ActualizarNomina(Nomina nomina)
        {
            string query = @"
                UPDATE nomina.nomina
                SET id_empleado = @idEmpleado,
                    id_auditoria = @idAuditoria,
                    periodo_inicio = @periodoInicio,
                    periodo_fin = @periodoFin,
                    estado_pago = @estadoPago
                WHERE id = @id";

            try
            {
                NpgsqlParameter[] parameters = new NpgsqlParameter[]
                {
                    _dbAccess.CreateParameter("@id", nomina.Id),
                    _dbAccess.CreateParameter("@idEmpleado", nomina.IdEmpleado),
                    _dbAccess.CreateParameter("@idAuditoria", nomina.IdAuditoria),
                    _dbAccess.CreateParameter("@periodoInicio", nomina.FechaInicio),
                    _dbAccess.CreateParameter("@periodoFin", nomina.FechaFin),
                    _dbAccess.CreateParameter("@estadoPago", nomina.EstadoPago)
                };

                _dbAccess.Connect();
                int rowsAffected = _dbAccess.ExecuteNonQuery(query, parameters);
                _logger.Info($"Nómina con ID {nomina.Id} actualizada correctamente. Filas afectadas: {rowsAffected}");
                return rowsAffected;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al actualizar la nómina con ID {nomina.Id}");
                return 0;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        // Eliminar (baja lógica) una nómina
        public int EliminarNomina(int idNomina)
        {
            string query = "UPDATE nomina.nomina SET estado_pago = 'Eliminado' WHERE id = @idNomina";

            try
            {
                NpgsqlParameter[] parameters = new NpgsqlParameter[]
                {
                    _dbAccess.CreateParameter("@idNomina", idNomina)
                };

                _dbAccess.Connect();
                int rowsAffected = _dbAccess.ExecuteNonQuery(query, parameters);
                _logger.Info($"Nómina con ID {idNomina} eliminada correctamente. Filas afectadas: {rowsAffected}");
                return rowsAffected;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al eliminar la nómina con ID {idNomina}");
                return 0;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }
    }
}