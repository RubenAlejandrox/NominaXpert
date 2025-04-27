using NLog;
using NominaXpert.Data;
using NominaXpert.Utilities;
using NominaXpert.Model;
using System.Data;
using ControlEscolar.Utilities;
namespace NominaXpert.Controller
{
    class UsuariosController
    {
        private static readonly Logger _logger = LoggingManager.GetLogger("NominaXpert.Controller.UsuariosController");
        private readonly UsuariosDataAccess _usuariosData;
        private readonly PersonasDataAccess _personasData;

        public UsuariosController()
        {
            try
            {
                _usuariosData = new UsuariosDataAccess();
                _personasData = new PersonasDataAccess();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "");
                throw;
            }
        }
        public (int id, string mensaje) RegistrarUsuario(Usuario usuario)
        {
            try
            {
                // Verificar si la usuario ya existe
                if (_usuariosData.ExisteNombreUsuario(usuario.Nombre_Usuario))
                {
                    _logger.Warn($"Intento de registrar usuario con nombre de usuario duplicado: {usuario.Nombre_Usuario}");
                    return (-3, $"El usuario {usuario.Nombre_Usuario} ya está registrado en el sistema");
                }

                // Verificar si el CURP ya existe
                if (_personasData.ExisteCurp(usuario.DatosPersonales.Curp))
                {
                    _logger.Warn($"Intento de registrar usuario con CURP duplicado: {usuario.DatosPersonales.Curp}");
                    return (-2, $"El CURP {usuario.DatosPersonales.Curp} ya está registrado en el sistema");
                }

                // Verificar si el RFC ya existe
                if (_personasData.ExisteCurp(usuario.DatosPersonales.Curp))
                {
                    _logger.Warn($"Intento de registrar usuario con RFC duplicado: {usuario.DatosPersonales.Rfc}");
                    return (-2, $"RFC {usuario.DatosPersonales.Rfc} ya está registrado en el sistema");
                }

                // Registrar el usuario
                _logger.Info($"Registrando nuevo usuario: {usuario.DatosPersonales.NombreCompleto}, Nombre_Usuario: {usuario.Nombre_Usuario}");
                int idUsuario = _usuariosData.InsertarUsuario(usuario);

                if (idUsuario <= 0)
                {
                    return (-4, "Error al registrar el usuario en la base de datos");
                }

                _logger.Info($"Usuario registrado exitosamente con ID: {idUsuario}");
                return (idUsuario, "Usuario registrado exitosamente");
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al registrar Usuario: {usuario.DatosPersonales?.NombreCompleto ?? "Sin nombre"}, Matrícula: {usuario.Nombre_Usuario}");
                return (-5, $"Error inesperado: {ex.Message}");
            }
        }
        public List<Usuario> ObtenerUsuarios(bool soloActivos = true)
        {
            try
            {
                var usuarios = _usuariosData.ObtenerTodosLosUsuarios(soloActivos);
                _logger.Info($"Se obtuvieron {usuarios.Count} usuarios con información de roles");
                return usuarios;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener la lista de usuarios con roles");
                throw;
            }
        }



        public Usuario? ObtenerDetalleUsuario(int idUsuario)
        {
            try
            {
                _logger.Debug($"Solicitando detalle del estudiante con ID: {idUsuario}");
                return _usuariosData.ObtenerUsuarioPorId(idUsuario);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al obtener detalles del estudiante con ID: {idUsuario}");
                throw;
            }
        }

        public (bool exito, string mensaje) ActualizarUsuario(Usuario usuario)
        {
            try
            {
                if (usuario == null)
                    return (false, "Usuario no válido.");
                if (usuario.Id <= 0)
                    return (false, "ID de estudiante no válido");
                if (usuario.DatosPersonales == null)
                    return (false, "No se proporcionaron los datos personales del estudiante");


                Usuario? usuarioExistente = _usuariosData.ObtenerUsuarioPorId(usuario.Id);
                if (usuarioExistente == null)
                    return (false, $"No se encontró el usuario con ID {usuario.Id}");

                if (usuario.Nombre_Usuario != usuarioExistente.Nombre_Usuario &&
                    _usuariosData.ExisteNombreUsuario(usuario.Nombre_Usuario))
                {
                    return (false, $"El nombre de usuario {usuario.Nombre_Usuario} ya está registrado.");
                }
                if (usuario.DatosPersonales.Curp != usuarioExistente.DatosPersonales.Curp)
                {
                    // Buscar si existe otra persona con el mismo CURP que no sea la persona de este estudiante
                    bool personaConMismoCurp = _personasData.ExisteCurp(usuario.DatosPersonales.Curp);
                    if (personaConMismoCurp)
                    {
                        return (false, $"El CURP {usuario.DatosPersonales.Curp} ya está registrado para otra persona");
                    }
                }
                if (usuario.DatosPersonales.Rfc != usuarioExistente.DatosPersonales.Rfc)
                {
                    // Buscar si existe otra persona con el mismo RFC que no sea la persona de este estudiante
                    bool personaConMismoRFC = _personasData.ExisteRFC(usuario.DatosPersonales.Rfc);
                    if (personaConMismoRFC)
                    {
                        return (false, $"RFC {usuario.DatosPersonales.Rfc} ya está registrado para otra persona");
                    }
                }
                _logger.Info($"Actualizando estudiante con ID: {usuario.Id}, Nombre: {usuario.DatosPersonales.NombreCompleto}");

                bool actualizado = _usuariosData.ActualizarUsuario(usuario);
                return actualizado
                    ? (true, "Usuario actualizado correctamente.")
                    : (false, "Error al actualizar usuario.");

            }
            catch (Exception ex)
            {
                return (false, $"Error inesperado: {ex.Message}");
            }
        }

        public (bool Autenticado, string Rol, string Mensaje) AutenticarUsuario(string correo, string contrasena)
        {
            if (!Validaciones.EsCorreoValido(correo))
                return (false, null, "Formato inválido de correo electrónico.");

            var usuario = _usuariosData.ObtenerPorCredenciales(correo, contrasena);

            if (usuario == null)
                return (false, null, "Credenciales incorrectas.");
            if (!usuario.Estatus)
                return (false, "INACTIVO", "Usuario inactivo.");

            UsuarioSesion.ActualizarSesion(usuario);
            return (true, UsuarioSesion.RolNombre, "Autenticación exitosa.");
        }

        public bool TienePermiso(string codigoPermiso)
        {
            return UsuarioSesion.Permisos?.Any(p => p.Codigo == codigoPermiso && p.Estatus) ?? false;
        }


    }
}
