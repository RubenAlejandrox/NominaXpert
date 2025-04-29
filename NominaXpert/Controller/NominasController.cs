using NominaXpert.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NominaXpert.Model;


namespace NominaXpert.Controller
{
    public class NominasController
    {

        private readonly NominaDataAccess _nominaDataAccess;
        private readonly AuditoriaDataAccess _auditoriaDataAccess;
        private readonly EmpleadosDataAccess _empleadosDataAccess; // Para verificar el estatus del empleado
        private readonly UsuariosDataAccess _usuariosDataAccess; // Para verificar el rol del usuario
        private readonly RegistroJornadaController _registroJornadaController; // Para consultar las horas trabajadas

        public NominasController()
        {
            // Inicialización de las clases de acceso a datos
            _nominaDataAccess = new NominaDataAccess();
            _auditoriaDataAccess = new AuditoriaDataAccess();
            _empleadosDataAccess = new EmpleadosDataAccess();
            _usuariosDataAccess = new UsuariosDataAccess(); // Para validar el rol del usuario
            _registroJornadaController = new RegistroJornadaController(); // Inicializamos el controlador para consultar horas
        }

        // Crear una nueva nómina
        public bool CrearNomina(int idEmpleado, int idUsuario, DateTime periodoInicio, DateTime periodoFin)
        {
            try
            {
                // 1. Validar que el empleado esté activo
                bool empleadoActivo = _empleadosDataAccess.VerificarEmpleadoActivo(idEmpleado);
                if (!empleadoActivo)
                {
                    throw new Exception("El empleado no está activo.");
                }

                // 2. Consultar el total de horas trabajadas para este empleado en el periodo
                decimal totalHoras = _registroJornadaController.ConsultarTotalHorasTrabajadas(idEmpleado, periodoInicio, periodoFin, idUsuario);
                if (totalHoras == 0)
                {
                    throw new Exception("No se encontraron registros de jornada para este empleado en el periodo especificado.");
                }

                //// 3. Verificar que el usuario tenga permisos para registrar la auditoría (ADMIN o OPERADOR)
                //bool usuarioAutorizado = _usuariosDataAccess.VerificarUsuarioAutorizado(idUsuario);
                //if (!usuarioAutorizado)
                //{
                //    throw new Exception("El usuario no tiene permisos suficientes para registrar la auditoría.");
                //}

                // 4. Registrar la nómina
                _nominaDataAccess.RegistrarNomina(idEmpleado, idUsuario, periodoInicio, periodoFin);

                // 5. Registrar la auditoría de la acción de alta de la nómina
                string detalleAccion = $"Se creó una nómina para el empleado con ID {idEmpleado} en el periodo {periodoInicio.ToShortDateString()} - {periodoFin.ToShortDateString()} con un total de {totalHoras} horas trabajadas.";
                _auditoriaDataAccess.RegistrarAuditoria(idUsuario, "alta nómina", detalleAccion);

                return true;
            }
            catch (Exception ex)
            {
                // Manejo de errores y registro
                Console.WriteLine($"Error al crear la nómina: {ex.Message}");
                return false;
            }
        }
    }
}