using NominaXpert.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NominaXpert.Model;
using NLog;
using NominaXpert.Utilities;
using ControlEscolar.Utilities;


namespace NominaXpert.Controller
{
    public class NominasController
    {

        private readonly NominaDataAccess _nominaDataAccess;
        private readonly AuditoriaDataAccess _auditoriaDataAccess;
        private readonly EmpleadosDataAccess _empleadosDataAccess; // Para verificar el estatus del empleado
        private readonly UsuariosDataAccess _usuariosDataAccess; // Para verificar el rol del usuario
        private readonly RegistroJornadaController _registroJornadaController; // Para consultar las horas trabajadas

        // Logger para la clase
        private static readonly Logger _logger = LoggingManager.GetLogger("NominaXpert.Controller.NominasController");

        public NominasController()
        {
            // Inicialización de las clases de acceso a datos
            _nominaDataAccess = new NominaDataAccess();
            _auditoriaDataAccess = new AuditoriaDataAccess();
            _empleadosDataAccess = new EmpleadosDataAccess();
            _usuariosDataAccess = new UsuariosDataAccess(); // Para validar el rol del usuario
            _registroJornadaController = new RegistroJornadaController(); // Inicializamos el controlador para consultar horas
        }

        // Método para obtener la última nómina generada para un empleado
        public int ObtenerUltimaNominaGenerada(int idEmpleado)
        {
            return _nominaDataAccess.ObtenerUltimaNominaGenerada(idEmpleado); // Acceder a la base de datos para obtener la ID
        }

        public bool CrearNomina(int idEmpleado, int idUsuario, DateTime periodoInicio, DateTime periodoFin)
        {
            try
            {
                // 1. Verificar que el empleado esté activo
                bool empleadoActivo = _empleadosDataAccess.VerificarEmpleadoActivo(idEmpleado);
                if (!empleadoActivo)
                {
                    throw new Exception("El empleado no está activo.");
                }

                // 2. Consultar el total de horas trabajadas
                decimal totalHoras = _registroJornadaController.ConsultarTotalHorasTrabajadas(idEmpleado, periodoInicio, periodoFin, idUsuario);
                if (totalHoras == 0)
                {
                    throw new Exception("Las horas trabajadas son 0. No se puede generar la nómina.");
                }

                // 3. Registrar la nómina en la base de datos y obtener la ID generada
                int idNominaGenerada = _nominaDataAccess.RegistrarNomina(idEmpleado, idUsuario, periodoInicio, periodoFin);  // Aquí obtenemos la ID generada

                // 4. Registrar la auditoría de la acción de alta de la nómina
                string detalleAccion = $"Se creó una nómina para el empleado con ID {idEmpleado} en el periodo {periodoInicio.ToShortDateString()} - {periodoFin.ToShortDateString()} con un total de {totalHoras} horas trabajadas.";
                _auditoriaDataAccess.RegistrarAuditoria(idUsuario, "alta nómina", detalleAccion);

                // Devolver la ID de la nómina generada si todo fue exitoso
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al crear la nómina.");
                return false;
            }
        }

        public bool VerificarPermisosUsuario(int idUsuario)
        {
            try
            {
                // Verificar que el usuario tenga permiso para generar la nómina (rol 1: ADMIN, rol 2: OPERADOR)
                bool usuarioAutorizado = _usuariosDataAccess.PermisoUsuarioGenerarNomina(idUsuario);

                if (!usuarioAutorizado)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al verificar los permisos del usuario con ID {idUsuario}");
                return false;
            }
        }

        // Propiedad estática para almacenar el valor de totalHoras
        public static decimal TotalHoras { get; set; }

        // Método para calcular las horas trabajadas y almacenarlas en la propiedad estática
        public void CalcularTotalHoras(int idEmpleado, DateTime fechaInicio, DateTime fechaFin, int idUsuario)
        {
            decimal totalHoras = _registroJornadaController.ConsultarTotalHorasTrabajadas(idEmpleado, fechaInicio, fechaFin, idUsuario);

            // Almacenamos el valor de las horas trabajadas en la propiedad estática
            TotalHoras = totalHoras;
        }

    }
}