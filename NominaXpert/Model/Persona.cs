using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaXpert.Model
{

    public class Persona
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public string Rfc { get; set; }
        public string Curp { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string Direccion { get; set; }
        public bool Estatus { get; set; }  // Nuevo campo agregado

        // Constructor predeterminado
        public Persona()
        {
            NombreCompleto = string.Empty;
            Correo = string.Empty;
            Telefono = string.Empty;
            Rfc = string.Empty;
            Curp = string.Empty;
            Direccion = string.Empty;
            Estatus = true; // Por defecto, la persona está activa
        }

        // Constructor con campos básicos
        public Persona(string nombreCompleto, string correo, string telefono, string rfc, string curp)
        {
            Id = 0;
            NombreCompleto = nombreCompleto;
            Correo = correo;
            Telefono = telefono;
            Rfc = rfc;
            Curp = curp;
            Direccion = string.Empty;
            FechaNacimiento = null;
            Estatus = true;
        }

        // Constructor completo
        public Persona(int id, string nombreCompleto, string correo, string telefono, string rfc, string curp, DateTime? fechaNacimiento, string direccion, bool estatus)
        {
            Id = id;
            NombreCompleto = nombreCompleto;
            Correo = correo;
            Telefono = telefono;
            Rfc = rfc;
            Curp = curp;
            FechaNacimiento = fechaNacimiento;
            Direccion = direccion;
            Estatus = estatus;
        }

    }
}