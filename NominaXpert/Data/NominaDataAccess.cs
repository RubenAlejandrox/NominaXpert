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
        public int RegistrarNomina(int idEmpleado, int idAuditoria, DateTime periodoInicio, DateTime periodoFin)
        {
            string estadoPago = "Pendiente";  // El estado inicial por defecto

            string query = @"
                INSERT INTO nomina.nomina (id_empleado, fecha_inicio, fecha_fin, estado_pago, creado_at)
                VALUES (@idEmpleado, @periodoInicio, @periodoFin, @estadoPago, @creadoAt)
                RETURNING id;";  // Usamos RETURNING para obtener la ID generada

            try
            {
                NpgsqlParameter[] parameters = new NpgsqlParameter[]
                {
            _dbAccess.CreateParameter("@idEmpleado", idEmpleado),
            _dbAccess.CreateParameter("@periodoInicio", periodoInicio),
            _dbAccess.CreateParameter("@periodoFin", periodoFin),
            _dbAccess.CreateParameter("@estadoPago", estadoPago),
            _dbAccess.CreateParameter("@creadoAt", DateTime.Now)
                };

                _dbAccess.Connect();
                DataTable result = _dbAccess.ExecuteQuery_Reader(query, parameters);

                if (result.Rows.Count > 0)
                {
                    // Obtener la ID generada
                    int idNomina = Convert.ToInt32(result.Rows[0]["id"]);
                    _logger.Info($"Nómina registrada correctamente para el empleado {idEmpleado}, ID de la nómina: {idNomina}.");
                    return idNomina;  // Retornar la ID de la nómina recién creada
                }
                else
                {
                    throw new Exception("No se pudo obtener la ID de la nómina.");
                }
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
                SELECT id, id_empleado, periodo_inicio, periodo_fin, estado_pago, creado_at
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

        /// <summary>
        /// Método para obtener la última nómina generada para un empleado específico
        /// </summary>
        /// <param name="idEmpleado"></param>
        /// <returns></returns>
        public int ObtenerUltimaNominaGenerada(int idEmpleado)
        {
            string query = @"
                SELECT id
                FROM nomina.nomina
                WHERE id_empleado = @idEmpleado
                ORDER BY fecha_creacion DESC
                LIMIT 1"; // Asegura la última nómina generada
            try
            {
                NpgsqlParameter[] parameters = new NpgsqlParameter[]
                {
            _dbAccess.CreateParameter("@idEmpleado", idEmpleado)
                };

                _dbAccess.Connect();
                DataTable resultado = _dbAccess.ExecuteQuery_Reader(query, parameters);

                if (resultado.Rows.Count > 0)
                {
                    return Convert.ToInt32(resultado.Rows[0]["id"]);
                }
                else
                {
                    return 0; // Retornar 0 si no se encuentra ninguna nómina
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener la última nómina generada.");
                return 0;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }


        /// <summary>
        /// Método para buscar una nómina por su ID
        /// </summary>
        /// <param name="idNomina"></param>
        /// <returns></returns>
        public NominaConsulta BuscarNominaPorId(int idNomina)
        {
            string query = @"
        SELECT n.id AS IdNomina, e.id AS IdEmpleado, p.nombre_completo AS NombreEmpleado, 
               e.departamento AS Departamento, e.sueldo AS SueldoBase, p.rfc AS RFCEmpleado, 
               n.estado_pago AS EstadoPago
        FROM nomina.nomina n
        INNER JOIN nomina.empleados e ON n.id_empleado = e.id
        INNER JOIN seguridad.personas p ON e.id_persona = p.id
        WHERE n.id = @idNomina;";

            try
            {
                if (idNomina <= 0)
                    throw new ArgumentException("La ID de la nómina no es válida.");

                var parametros = new NpgsqlParameter[]
                {
            _dbAccess.CreateParameter("@idNomina", idNomina)
                };

                _dbAccess.Connect();
                DataTable dt = _dbAccess.ExecuteQuery_Reader(query, parametros);

                if (dt.Rows.Count == 0)
                    return null;

                var row = dt.Rows[0];

                return new NominaConsulta
                {
                    IdNomina = Convert.ToInt32(row["IdNomina"]),
                    IdEmpleado = Convert.ToInt32(row["IdEmpleado"]),
                    EstadoPago = row["EstadoPago"].ToString(),
                    DatosEmpleado = new Empleado
                    {
                        Id = Convert.ToInt32(row["IdEmpleado"]),
                        Departamento = row["Departamento"].ToString(),
                        Sueldo = Convert.ToDecimal(row["SueldoBase"]),
                        DatosPersonales = new Persona
                        {
                            NombreCompleto = row["NombreEmpleado"].ToString(),
                            Rfc = row["RFCEmpleado"].ToString()
                        }
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al buscar la nómina por ID.");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }


        /// <summary>
        ///Actualizar solo el estado de pago de una nómina
        /// </summary>
        /// <param name="idNomina"></param>
        /// <param name="nuevoEstado"></param>
        /// <returns></returns>
        public int ActualizarEstadoPago(int idNomina, string nuevoEstado)
        {
            string query = @"
        UPDATE nomina.nomina
        SET estado_pago = @nuevoEstado
        WHERE id = @idNomina";

            try
            {
                if (idNomina <= 0)
                    throw new ArgumentException("ID de nómina inválido.");

                var parametros = new NpgsqlParameter[]
                {
            _dbAccess.CreateParameter("@idNomina", idNomina),
            _dbAccess.CreateParameter("@nuevoEstado", nuevoEstado)
                };

                _dbAccess.Connect();
                int rowsAffected = _dbAccess.ExecuteNonQuery(query, parametros);

                _logger.Info($"Estado de la nómina ID {idNomina} actualizado a '{nuevoEstado}'. Filas afectadas: {rowsAffected}");
                return rowsAffected;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al actualizar el estado de pago de la nómina.");
                return 0;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

    }
}