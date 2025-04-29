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
    class BonificacionDataAccess
    {
        // Logger para la clase
        private static readonly Logger _logger = LoggingManager.GetLogger("NominaXpert.Data.BonificacionDataAccess");

        // Instancia del acceso a datos de PostgreSQL
        private readonly PostgresSQLDataAccess _dbAccess;

        /// <summary>
        /// Constructor de la clase NominasDataAccess
        /// </summary>

        public BonificacionDataAccess()
        {
            try
            {
                // Obtiene la instancia única de PostgresSQLDataAccess (patrón Singleton)
                _dbAccess = PostgresSQLDataAccess.GetInstance();
                _logger.Info("Instancia de BonificacionDataAccess creada correctamente.");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al inicializar BonificacionDataAccess");
                throw;
            }
        }

        // Registrar Bonificación
        public void RegistrarBonificacion(Bonificacion bonificacion)
        {
            string query = @"
                INSERT INTO nomina.bonificaciones (id_nomina, tipo, monto)
                VALUES (@idNomina, @tipo, @monto)";

            try
            {
                NpgsqlParameter[] parameters = new NpgsqlParameter[]
                {
                    _dbAccess.CreateParameter("@idNomina", bonificacion.IdNomina),
                    _dbAccess.CreateParameter("@tipo", bonificacion.IdTipo),
                    _dbAccess.CreateParameter("@monto", bonificacion.Monto)
                };

                _dbAccess.Connect();
                _dbAccess.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al registrar la bonificación.");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        // Obtener Bonificaciones de una nómina
        public List<Bonificacion> ObtenerBonificacionesPorNomina(int idNomina)
        {
            List<Bonificacion> bonificaciones = new List<Bonificacion>();
            string query = @"
                SELECT id, id_nomina, tipo, monto
                FROM nomina.bonificaciones
                WHERE id_nomina = @idNomina";

            try
            {
                NpgsqlParameter[] parameters = new NpgsqlParameter[]
                {
                    _dbAccess.CreateParameter("@idNomina", idNomina)
                };

                _dbAccess.Connect();
                DataTable resultado = _dbAccess.ExecuteQuery_Reader(query, parameters);

                foreach (DataRow row in resultado.Rows)
                {
                    Bonificacion bonificacion = new Bonificacion
                    {
                        Id = Convert.ToInt32(row["id"]),
                        IdNomina = Convert.ToInt32(row["id_nomina"]),
                        IdTipo = Convert.ToInt32(row["id_tipo"]),
                        Monto = Convert.ToDecimal(row["monto"])
                    };
                    bonificaciones.Add(bonificacion);
                }

                return bonificaciones;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener las bonificaciones.");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        // Eliminar Bonificación
        public int EliminarBonificacion(int idBonificacion)
        {
            string query = "DELETE FROM nomina.bonificaciones WHERE id = @id";

            try
            {
                NpgsqlParameter[] parameters = new NpgsqlParameter[]
                {
                    _dbAccess.CreateParameter("@id", idBonificacion)
                };

                _dbAccess.Connect();
                int rowsAffected = _dbAccess.ExecuteNonQuery(query, parameters);
                return rowsAffected;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al eliminar la bonificación.");
                return 0;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }
    }
}
