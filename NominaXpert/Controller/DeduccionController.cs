using ControlEscolar.Utilities;
using NominaXpert.Data;
using NominaXpert.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace NominaXpert.Controller
{
    public class DeduccionController
    {
        private readonly DeducciónDataAccess _deduccionDataAccess;

        // Logger para la clase
        private static readonly Logger _logger = LoggingManager.GetLogger("NominaXpert.Controller.DeduccionesController");

        public DeduccionController()
        {
            _deduccionDataAccess = new DeducciónDataAccess(); // Inicializamos el acceso a datos
        }

        // Método que obtiene las deducciones asociadas a una nómina específica
        public List<Deduccion> ObtenerDeduccionesPorNomina(int idNomina)
        {
            try
            {
                _logger.Info($"Deducciones obtenidas para la nómina ID: {idNomina}.");
                return _deduccionDataAccess.ObtenerDeduccionesPorNomina(idNomina);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al obtener las deducciones para la nómina ID: {idNomina}.");
                throw new ApplicationException("Error al obtener las deducciones", ex);
            }
        }

        // Método para registrar una nueva deducción
        public void RegistrarDeduccion(Deduccion deduccion)
        {
            try
            {
                _deduccionDataAccess.RegistrarDeduccion(deduccion);
                _logger.Info($"Deducción registrada correctamente: {deduccion.IdNomina}, {deduccion.IdTipo}, {deduccion.Monto}.");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al registrar la deducción: {deduccion.IdNomina}, {deduccion.IdTipo}, {deduccion.Monto}.");
                throw new ApplicationException("Error al registrar la deducción", ex);
            }
        }

        // Método para actualizar una deducción
        public int ActualizarDeduccion(Deduccion deduccion)
        {
            try
            {
                _logger.Info($"Actualizando la deducción ID: {deduccion.Id}.");
                return _deduccionDataAccess.ActualizarDeduccion(deduccion);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al actualizar la deducción ID: {deduccion.Id}.");
                throw new ApplicationException("Error al actualizar la deducción", ex);
            }
        }

        // Método para eliminar una deducción
        public int EliminarDeduccion(int idDeduccion, int idNomina)
        {
            _logger.Info($"Iniciando la eliminación de la deducción ID: {idDeduccion}");

            try
            {
                _logger.Info($"Eliminando la deducción ID: {idDeduccion}");
                return _deduccionDataAccess.EliminarDeduccion(idDeduccion, idNomina);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al eliminar la deducción ID: {idDeduccion}");
                throw new ApplicationException("Error al eliminar la deducción", ex);
            }
        }
    }
}
