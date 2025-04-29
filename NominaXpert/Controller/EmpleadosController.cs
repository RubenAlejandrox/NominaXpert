using NominaXpert.Data;
using NominaXpert.Model;
using System;
using NominaXpert.Utilities;

namespace NominaXpert.Controller
{
    public class EmpleadosController
    {
        private readonly EmpleadosDataAccess _empleadosData;

        public EmpleadosController()
        {
            _empleadosData = new EmpleadosDataAccess();
        }

        // Método para buscar empleado por matrícula
        public (string Nombre, decimal SueldoBase) BuscarEmpleadoPorMatricula(string matricula)
        {
            try
            {
                // Validar formato de matrícula (usa tu método de Validaciones)
                if (!Validaciones.EsNoMatriculaValido(matricula))
                    throw new Exception("Formato de matrícula inválido.");

                var (nombre, sueldo) = _empleadosData.ObtenerNombreYSueldoPorMatricula(matricula);

                if (string.IsNullOrEmpty(nombre))
                    throw new Exception("Empleado no encontrado.");

                return (nombre, sueldo);
            }
            catch (Exception ex)
            {
                // Loggear error si es necesario
                throw new Exception($"Error al buscar empleado: {ex.Message}");
            }
        }
    }
}