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
    public class BonificacionController
    {
        private readonly BonificacionDataAccess _bonificacionDataAccess;

        // Logger para la clase
        private static readonly Logger _logger = LoggingManager.GetLogger("NominaXpert.Controller.PercepcionesController");

        public BonificacionController()
        {
            _bonificacionDataAccess = new BonificacionDataAccess(); // Inicializamos el acceso a datos

        }

        // Método que obtiene las bonificaciones asociadas a una nómina específica
        public List<Bonificacion> ObtenerBonificacionesPorNomina(int idNomina)
        {
            try
            {
                // Llamamos al método de acceso a datos para obtener las bonificaciones
                _logger.Info($"Bonificaciones obtenidas para la nómina ID: {idNomina}.");
                return _bonificacionDataAccess.ObtenerBonificacionesPorNomina(idNomina);
            }
            catch (Exception ex)
            {
                // Manejo de errores
                _logger.Error(ex, $"Error al obtener las bonificaciones para la nómina ID: {idNomina}.");
                throw new ApplicationException("Error al obtener las bonificaciones", ex);
            }
        }

        // Método para registrar una nueva bonificación
        public void RegistrarBonificacion(Bonificacion bonificacion)
        {
            try
            {
                _bonificacionDataAccess.RegistrarBonificacion(bonificacion);
                _logger.Info($"Bonificación registrada correctamente: {bonificacion.IdNomina}, {bonificacion.IdTipo}, {bonificacion.Monto}.");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al registrar la bonificación: {bonificacion.IdNomina}, {bonificacion.IdTipo}, {bonificacion.Monto}.");
                throw new ApplicationException("Error al registrar la bonificación", ex);

            }
        }

        // Método para actualizar una bonificación
        public int ActualizarBonificacion(Bonificacion bonificacion)
        {
            try
            {
                _logger.Info($"Actualizando la bonificación ID: {bonificacion.Id}.");
                return _bonificacionDataAccess.ActualizarBonificacion(bonificacion);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al actualizar la bonificación ID: {bonificacion.Id}.");
                throw new ApplicationException("Error al actualizar la bonificación", ex);
            }
        }

        // Método para eliminar una bonificación
        public int EliminarBonificacion(int idBonificacion)
        {
            _logger.Info($"Iniciando la eliminación de la bonificación ID: {idBonificacion}");

            try
            {
                _logger.Info($"Eliminando la bonificación ID: {idBonificacion}");
                return _bonificacionDataAccess.EliminarBonificacion(idBonificacion);
                
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al eliminar la bonificación ID: {idBonificacion}");
                throw new ApplicationException("Error al eliminar la bonificación", ex);
               
            }
        }
    }
}
