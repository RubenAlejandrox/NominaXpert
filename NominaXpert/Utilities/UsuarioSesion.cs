using NominaXpert.Model;

namespace NominaXpert.Utilities
{
    public static class UsuarioSesion
    {
        public static int UsuarioId { get; private set; }
        public static string Correo { get; private set; }
        public static string NombreUsuario { get; private set; }
        public static string Contrasena { get; private set; }
        public static string RolNombre { get; private set; }
        public static List<Permiso> Permisos { get; private set; }

        public static void ActualizarSesion(Usuario usuario)
        {
            UsuarioId = usuario.Id;
            Correo = usuario.Nombre_Usuario;
            NombreUsuario = usuario.Nombre_Usuario;
            Contrasena = usuario.Contrasena;
            RolNombre = usuario.Rol?.Nombre;
            Permisos = usuario.Rol?.Permisos ?? new List<Permiso>();
        }

        public static void CerrarSesion()
        {
            UsuarioId = 0;
            Correo = null;
            NombreUsuario = null;
            Contrasena = null;
            RolNombre = null;
            Permisos = null;
        }
    }
}
