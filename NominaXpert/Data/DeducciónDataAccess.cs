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
    class DeducciónDataAccess
    {
        // Logger para la clase
        private static readonly Logger _logger = LoggingManager.GetLogger("NominaXpert.Data.DeduccionDataAccess");

        // Instancia del acceso a datos de PostgreSQL
        private readonly PostgresSQLDataAccess _dbAccess;

        /// <summary>
        /// Funcion de clase DeduccionDataAccess
        /// </summary>
        /// 
        // Registrar Deducción
        public void RegistrarDeduccion(Deduccion deduccion)
        {
            string query = @"
            INSERT INTO nomina.deducciones (id_nomina, tipo, monto)
            VALUES (@idNomina, @tipo, @monto)";

            try
            {
                NpgsqlParameter[] parameters = new NpgsqlParameter[]
                {
                _dbAccess.CreateParameter("@idNomina", deduccion.IdNomina),
                _dbAccess.CreateParameter("@tipo", deduccion.IdTipo),
                _dbAccess.CreateParameter("@monto", deduccion.Monto)
                };

                _dbAccess.Connect();
                _dbAccess.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al registrar la deducción.");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        // Obtener Deducciones de una nómina
        public List<Deduccion> ObtenerDeduccionesPorNomina(int idNomina)
        {
            List<Deduccion> deducciones = new List<Deduccion>();
            string query = @"
            SELECT id, id_nomina, tipo, monto
            FROM nomina.deducciones
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
                    Deduccion deduccion = new Deduccion
                    {
                        Id = Convert.ToInt32(row["id"]),
                        IdNomina = Convert.ToInt32(row["id_nomina"]),
                        IdTipo = Convert.ToInt32(row["id_tipo"]),
                        Monto = Convert.ToDecimal(row["monto"])
                    };
                    deducciones.Add(deduccion);
                }

                return deducciones;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener las deducciones.");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        // Eliminar Deducción
        public int EliminarDeduccion(int idDeduccion)
        {
            string query = "DELETE FROM nomina.deducciones WHERE id = @id";

            try
            {
                NpgsqlParameter[] parameters = new NpgsqlParameter[]
                {
                _dbAccess.CreateParameter("@id", idDeduccion)
                };

                _dbAccess.Connect();
                int rowsAffected = _dbAccess.ExecuteNonQuery(query, parameters);
                return rowsAffected;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al eliminar la deducción.");
                return 0;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }
    }
}