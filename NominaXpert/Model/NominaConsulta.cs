using System;

namespace NominaXpert.Model
{
    public class NominaConsulta
    {
        public int IdNomina { get; set; }
        public int IdEmpleado { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string EstadoPago { get; set; }
        public Empleado DatosEmpleado { get; set; }

        // Constructor predeterminado
        public NominaConsulta()
        {
            IdNomina = 0; // ID por defecto
            IdEmpleado = 0; // ID empleado por defecto
            FechaInicio = DateTime.Now.Date; // Fecha actual sin hora
            FechaFin = DateTime.Now.Date; // Fecha actual sin hora
            EstadoPago = "Pendiente"; // Estado inicial por defecto
            DatosEmpleado = null; // Relación no cargada inicialmente
        }

        // Constructor parcial (con datos clave sin relaciones)
        public NominaConsulta(int idNomina, int idEmpleado, DateTime fechaInicio, DateTime fechaFin, string estadoPago)
        {
            IdNomina = idNomina;
            IdEmpleado = idEmpleado;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            EstadoPago = estadoPago;
            DatosEmpleado = null; // Se puede asignar después
        }

        // Constructor completo (incluyendo el objeto Empleado relacionado)
        public NominaConsulta(int idNomina, int idEmpleado, DateTime fechaInicio, DateTime fechaFin, string estadoPago, Empleado datosEmpleado)
        {
            IdNomina = idNomina;
            IdEmpleado = idEmpleado;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            EstadoPago = estadoPago;
            DatosEmpleado = datosEmpleado; // Se pasa el empleado relacionado
        }

        public string NombreEmpleado => DatosEmpleado?.DatosPersonales?.NombreCompleto ?? "Sin Nombre";
        public string DepartamentoEmpleado => DatosEmpleado?.Departamento ?? "Sin Departamento";
        public string RfcEmpleado => DatosEmpleado?.DatosPersonales?.Rfc ?? "Sin RFC";
        public decimal SueldoBase => DatosEmpleado?.Sueldo ?? 0;

    }
}
