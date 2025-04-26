using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaXpert.Model
{
    public class Usuario
    {
        public int Id { get; set; }
        public int IdPersona { get; set; }
        public int IdRol { get; set; }
        public string NombreUsuario { get; set; }
        public string Contraseña { get; set; }  // Considerar encriptar la contraseña
        public bool Estatus { get; set; }


        // Constructor predeterminado
        public Usuario()
        {
            IdPersona = 0;
            IdRol = 0;
            NombreUsuario = string.Empty;
            Contraseña = string.Empty;
            Estatus = true; // Por defecto, el usuario está activo
        }

        // Constructor con campos obligatorios
        public Usuario(int idPersona, int idRol, string nombreUsuario, string contraseña)
        {
            IdPersona = idPersona;
            IdRol = idRol;
            NombreUsuario = nombreUsuario;
            Contraseña = contraseña;
            Estatus = true; // Por defecto, el usuario está activo
        }

        // Constructor con todos los campos
        public Usuario(int id, int idPersona, int idRol, string nombreUsuario, string contraseña, bool estatus)
        {
            Id = id;
            IdPersona = idPersona;
            IdRol = idRol;
            NombreUsuario = nombreUsuario;
            Contraseña = contraseña;
            Estatus = estatus;
        }
    }

}
