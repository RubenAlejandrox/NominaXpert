using System.Data;
using Npgsql;
using NominaXpert.Model;
using NLog;
using NominaXpert.Data;
using NominaXpert.Utilities;
using ControlEscolar.Data;
namespace NominaXpert.Data
{
    public class UsuariosDataAccess
    {
        private static readonly Logger _logger = LogManager.GetLogger("Proyecto1.Data.PostgreSQLDataAccess");
        private readonly PostgresSQLDataAccess _dbAccess; //para el acceso a la logica de los 3 elementos osea conexion, command y los 3 metodos
        private readonly PersonasDataAccess _personasData;


        /// <summary>
        /// Contructor de la clase EstudiantesDataAccess
        /// </summary>
        public UsuariosDataAccess()
        {
            try
            {
                _dbAccess = PostgresSQLDataAccess.GetInstance();
                _personasData = new PersonasDataAccess(); // Inicializa PersonasDataAccess
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al iniciazlizar UsuariosRepository");
                throw;
            }
        }

        public int InsertarUsuario(Usuario usuario)
        {
            try
            {
                // Verificación de seguridad
                if (_personasData == null)
                {
                    _logger.Error("PersonasDataAccess no fue inicializado");
                    throw new InvalidOperationException("Dependencia PersonasDataAccess no configurada");
                }

                // Insertar persona primero
                int idPersona = _personasData.InsertarPersonaSeguridad(usuario.DatosPersonales);

                if (idPersona <= 0)
                {
                    _logger.Error($"Error al insertar persona para usuario: {usuario.Nombre_Usuario}");
                    return -1;
                }

                // Insertar estudiante
                string query = @"
        INSERT INTO seguridad.usuarios
        (id_persona, id_rol, nombre_usuario, contraseña, estatus)
        VALUES (@IdPersona, @IdRol, @Nombre_Usuario, @Contraseña, @Estatus)
        RETURNING id";

                var parametros = new NpgsqlParameter[] {
            _dbAccess.CreateParameter("@IdPersona", idPersona),
            _dbAccess.CreateParameter("@IdRol", usuario.IdRol),
            _dbAccess.CreateParameter("@Nombre_Usuario", usuario.Nombre_Usuario),
            _dbAccess.CreateParameter("@Contraseña", usuario.Contrasena),
            _dbAccess.CreateParameter("@Estatus", usuario.Estatus)
        };

                _dbAccess.Connect();
                var idGenerado = Convert.ToInt32(_dbAccess.ExecuteScalar(query, parametros));

                _logger.Info($"Usuario insertado correctamente. ID: {idGenerado}");
                return idGenerado;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al insertar usuario. NombreUsuario: {usuario.Nombre_Usuario}");
                return -1;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        public bool ExisteNombreUsuario(string nombre_usuario)
        {
            try
            {
                string query = "SELECT COUNT(*) FROM seguridad.usuarios WHERE nombre_usuario = @Nombre_Usuario";

                // Crea el parámetro
                NpgsqlParameter paramNomUsuario = _dbAccess.CreateParameter("@Nombre_Usuario", nombre_usuario);

                // Establece la conexión a la BD
                _dbAccess.Connect();

                // Ejecuta la consulta
                object? resultado = _dbAccess.ExecuteScalar(query, paramNomUsuario);

                int cantidad = Convert.ToInt32(resultado);
                bool existe = cantidad > 0;

                return existe;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al verificar la existencia del usuario {nombre_usuario}");
                return false;
            }
            finally
            {
                // Asegura que se cierre la conexión
                _dbAccess.Disconnect();
            }
        }

        public List<Usuario> ObtenerTodosLosUsuarios(bool soloActivos = true)
        {
            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                string query = @"
        SELECT 
            u.id, 
            u.id_persona, 
            u.id_rol, 
            u.nombre_usuario, 
            u.contraseña, 
            u.estatus AS estatus_usuario,
            r.nombre AS nombre_rol,
            r.descripcion AS descripcion_rol,
            p.nombre_completo, 
            p.correo, 
            p.telefono, 
            p.direccion, 
            p.rfc, 
            p.curp, 
            p.fecha_nacimiento, 
            p.estatus AS estatus_persona
        FROM seguridad.usuarios u
        INNER JOIN seguridad.personas p ON u.id_persona = p.id
        INNER JOIN seguridad.roles r ON u.id_rol = r.id
        WHERE 1=1";

                if (soloActivos)
                {
                    query += " AND u.estatus = true";
                }

                query += " ORDER BY u.id";

                _dbAccess.Connect();
                DataTable resultado = _dbAccess.ExecuteQuery_Reader(query);

                foreach (DataRow row in resultado.Rows)
                {
                    Persona persona = new Persona(
                    id: Convert.ToInt32(row["id_persona"]),
                    nombreCompleto: row["nombre_completo"].ToString() ?? "",
                    correo: row["correo"].ToString() ?? "",
                    telefono: row["telefono"].ToString() ?? "",
                    rfc: row["rfc"].ToString() ?? "",
                    curp: row["curp"].ToString() ?? "",
                    fechaNacimiento: row["fecha_nacimiento"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["fecha_nacimiento"]) : null,
                    direccion: row["direccion"].ToString() ?? "",
                    estatus: Convert.ToBoolean(row["estatus_persona"])
);


                    Usuario usuario = new Usuario(
                        idPersona: Convert.ToInt32(row["id_persona"]),
                        id: Convert.ToInt32(row["id"]),
                        nombre_usuario: row["nombre_usuario"].ToString() ?? string.Empty,
                        contrasena: row["contraseña"].ToString() ?? string.Empty,
                        estatus: Convert.ToBoolean(row["estatus_usuario"]),
                        idrol: Convert.ToInt32(row["id_rol"])
                    )
                    {
                        DatosPersonales = persona,
                        Rol = new Rol()
                        {
                            Id = Convert.ToInt32(row["id_rol"]),
                            Nombre = row["nombre_rol"].ToString(),
                            Descripcion = row["descripcion_rol"].ToString()
                        }
                    };

                    usuarios.Add(usuario);
                }

                _logger.Debug($"Se obtuvieron {usuarios.Count} registros de usuarios");
                return usuarios;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener usuarios con información de roles");
                throw;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }




        public Usuario ObtenerPorCredenciales(string correo, string contrasena)
        {
            try
            {
                string query = @"
            SELECT u.id, u.id_persona, u.nombre_usuario, u.contraseña, u.estatus,
                   r.id AS rol_id, r.codigo, r.nombre, r.descripcion, r.estatus AS rol_estatus,
                   p.id AS permiso_id, p.codigo AS permiso_codigo, p.descripcion AS permiso_desc, p.estatus AS permiso_estatus,
                   per.nombre_completo, per.correo, per.telefono, per.curp, per.fecha_nacimiento, 
                   per.estatus AS persona_estatus, per.direccion, per.rfc
            FROM seguridad.usuarios u
            JOIN seguridad.roles r ON u.id_rol = r.id
            LEFT JOIN seguridad.permisos_rol pr ON r.id = pr.id_rol
            LEFT JOIN seguridad.permisos p ON pr.id_permiso = p.id
            JOIN seguridad.personas per ON u.id_persona = per.id
            WHERE per.correo = @correo AND u.contraseña = @contraseña";

                var parametros = new[] {
            _dbAccess.CreateParameter("@correo", correo),
            _dbAccess.CreateParameter("@contraseña", contrasena)
        };

                _dbAccess.Connect();
                var table = _dbAccess.ExecuteQuery(query, parametros);
                if (table.Rows.Count == 0)
                {
                    _logger.Warn($"No se encontró ningún usuario con correo {correo}");
                    return null;
                }

                Usuario u = null;
                foreach (DataRow row in table.Rows)
                {
                    if (u == null)
                    {
                        u = new Usuario(
                            idPersona: Convert.ToInt32(row["id_persona"]),
                            id: Convert.ToInt32(row["id"]),
                            nombre_usuario: row["nombre_usuario"].ToString(),
                            contrasena: row["contraseña"].ToString(),
                            estatus: Convert.ToBoolean(row["estatus"]),
                            idrol: Convert.ToInt32(row["rol_id"])
                        );

                        // Asignar los datos de la persona
                        u.Persona = new Persona(
                        id: Convert.ToInt32(row["id_persona"]),
                        nombreCompleto: row["nombre_completo"].ToString(),
                        correo: row["correo"].ToString(),
                        telefono: row["telefono"].ToString(),
                        rfc: row["rfc"].ToString(),
                        curp: row["curp"].ToString(),
                        fechaNacimiento: row["fecha_nacimiento"] as DateTime?,
                        direccion: row["direccion"].ToString(),
                        estatus: Convert.ToBoolean(row["persona_estatus"])
                        );


                        u.Rol = new Rol(
                            id: Convert.ToInt32(row["rol_id"]),
                            codigo: row["codigo"].ToString(),
                            nombre: row["nombre"].ToString(),
                            descripcion: row["descripcion"].ToString(),
                            estatus: Convert.ToBoolean(row["rol_estatus"]),
                            permisos: new List<Permiso>()
                        );
                    }
                    if (row["permiso_id"] != DBNull.Value)
                    {
                        u.Rol.Permisos.Add(new Permiso(
                            id: Convert.ToInt32(row["permiso_id"]),
                            codigo: row["permiso_codigo"].ToString(),
                            descripcion: row["permiso_desc"].ToString(),
                            estatus: Convert.ToBoolean(row["permiso_estatus"])
                        ));
                    }
                }

                return u;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener el usuario");
                return null;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }


        public bool ActualizarUsuario(Usuario usuario)
        {
            try
            {
                bool actualizacionPersonaExitosa = _personasData.ActualizarPersonaSeguridad(usuario.DatosPersonales);
                if (!actualizacionPersonaExitosa)
                    return false;

                string query = @"
            UPDATE seguridad.usuarios
            SET nombre_usuario = @NombreUsuario,
                contraseña = @Contrasena,
                estatus = @Estatus,
                id_rol = @IdRol
            WHERE id = @Id";

                NpgsqlParameter paramId = _dbAccess.CreateParameter("@Id", usuario.Id);
                NpgsqlParameter paramNombreUsuario = _dbAccess.CreateParameter("@NombreUsuario", usuario.Nombre_Usuario);
                NpgsqlParameter paramContrasena = _dbAccess.CreateParameter("@Contrasena", usuario.Contrasena);
                NpgsqlParameter paramEstatus = _dbAccess.CreateParameter("@Estatus", usuario.Estatus);
                NpgsqlParameter paramIdRol = _dbAccess.CreateParameter("@IdRol", usuario.IdRol);

                _dbAccess.Connect();

                int filasAfectadas = _dbAccess.ExecuteNonQuery(query,
                    paramId, paramNombreUsuario, paramContrasena, paramEstatus, paramIdRol);
                bool exito = filasAfectadas > 0;
                if (!exito)
                {
                    _logger.Warn($"No se pudo actualizar el usuario con ID {usuario.Id}. No se encontró el registro");
                }
                else
                {
                    _logger.Debug($"Estudiante con ID {usuario.Id} actualizado correctamente");
                }
                return exito;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al actualizar el usuario con ID {usuario.Id}");
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        public Usuario? ObtenerUsuarioPorId(int id)
        {
            try
            {
                string query = @"
                    SELECT u.id, u.id_persona, u.nombre_usuario, u.contraseña, u.estatus AS estatus_usuario,
                           r.id AS id_rol, r.nombre AS nombre_rol, r.descripcion AS descripcion_rol,
                           p.nombre_completo, p.correo, p.telefono, p.direccion, p.rfc, p.curp,
                           p.fecha_nacimiento, p.estatus AS estatus_persona
                    FROM seguridad.usuarios u
                    INNER JOIN seguridad.personas p ON u.id_persona = p.id
                    INNER JOIN seguridad.roles r ON u.id_rol = r.id
                    WHERE u.id = @Id";

                NpgsqlParameter paramId = _dbAccess.CreateParameter("@Id", id);

                _dbAccess.Connect();
                DataTable resultado = _dbAccess.ExecuteQuery_Reader(query, paramId);

                if (resultado.Rows.Count == 0)
                {
                    _logger.Warn($"No se encontró ningún usuario con ID {id}");
                    return null;
                }

                DataRow row = resultado.Rows[0];

                Persona persona = new Persona(
                    id: Convert.ToInt32(row["id_persona"]),
                    nombreCompleto: row["nombre_completo"].ToString() ?? "",
                    correo: row["correo"].ToString() ?? "",
                    telefono: row["telefono"].ToString() ?? "",
                    rfc: row["rfc"].ToString() ?? "",
                    curp: row["curp"].ToString() ?? "",
                    fechaNacimiento: row["fecha_nacimiento"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["fecha_nacimiento"]) : null,
                    direccion: row["direccion"].ToString() ?? "",
                    estatus: Convert.ToBoolean(row["estatus_persona"])
                );

                Usuario usuario = new Usuario(
                    idPersona: Convert.ToInt32(row["id_persona"]),
                    id: Convert.ToInt32(row["id"]),
                    nombre_usuario: row["nombre_usuario"].ToString() ?? "",
                    contrasena: row["contraseña"].ToString() ?? "",
                    estatus: Convert.ToBoolean(row["estatus_usuario"]),
                    idrol: Convert.ToInt32(row["id_rol"])
                )
                {
                    DatosPersonales = persona
                };

                return usuario;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener usuario por ID");
                return null;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }
        public int ObtenerIdPersonaPorUsuario(int idUsuario)
        {
            try
            {
                string query = "SELECT id_persona FROM seguridad.usuarios WHERE id = @IdUsuario";

                // Crea el parámetro
                var paramIdUsuario = _dbAccess.CreateParameter("@IdUsuario", idUsuario);

                _dbAccess.Connect(); // Se conecta usando tu PostgresSQLDataAccess
                object result = _dbAccess.ExecuteScalar(query, paramIdUsuario);

                if (result != null && result != DBNull.Value)
                {
                    return Convert.ToInt32(result);
                }

                return 0; // No encontrado
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al obtener ID de persona para el usuario con ID {idUsuario}");
                return 0;
            }
            finally
            {
                _dbAccess.Disconnect(); // Siempre desconectar al final
            }
        }

        public bool DarDeBajaUsuario(int idUsuario, string motivo)
        {
            _logger.Info($"Iniciando proceso de baja para usuario ID: {idUsuario}");

            try
            {
                _dbAccess.Connect();

                // 1. Obtener información del usuario (con parámetro fresco)
                string queryUsuario = "SELECT id_persona, estatus FROM seguridad.usuarios WHERE id = @IdUsuario";
                var paramUsuario1 = _dbAccess.CreateParameter("@IdUsuario", idUsuario);

                DataTable dtUsuario = _dbAccess.ExecuteQuery(queryUsuario, paramUsuario1);

                if (dtUsuario.Rows.Count == 0)
                {
                    _logger.Warn($"Usuario ID {idUsuario} no encontrado");
                    return false;
                }

                int idPersona = Convert.ToInt32(dtUsuario.Rows[0]["id_persona"]);
                bool usuarioActivo = Convert.ToBoolean(dtUsuario.Rows[0]["estatus"]);

                // 2. Obtener estado de la persona (con parámetro fresco)
                string queryPersona = "SELECT estatus FROM seguridad.personas WHERE id = @IdPersona";
                var paramPersona1 = _dbAccess.CreateParameter("@IdPersona", idPersona);

                DataTable dtPersona = _dbAccess.ExecuteQuery(queryPersona, paramPersona1);

                if (dtPersona.Rows.Count == 0)
                {
                    _logger.Warn($"Persona ID {idPersona} no encontrada");
                    return false;
                }

                bool personaActiva = Convert.ToBoolean(dtPersona.Rows[0]["estatus"]);

                _logger.Info($"Estado actual - Usuario {idUsuario}: {(usuarioActivo ? "ACTIVO" : "INACTIVO")}, Persona {idPersona}: {(personaActiva ? "ACTIVA" : "INACTIVA")}");

                bool cambiosRealizados = false;

                // 3. Eliminar permisos (con parámetro completamente nuevo)
                if (usuarioActivo)
                {
                    string deletePermisos = "DELETE FROM seguridad.permisos_rol WHERE id_rol IN (SELECT id_rol FROM seguridad.usuarios WHERE id = @IdUsuarioPermisos)";
                    var paramUsuarioPermisos = _dbAccess.CreateParameter("@IdUsuarioPermisos", idUsuario);
                    int permisosEliminados = _dbAccess.ExecuteNonQuery(deletePermisos, paramUsuarioPermisos);
                    _logger.Info($"Permisos eliminados: {permisosEliminados}");
                }

                // 4. Baja de usuario (con parámetro completamente nuevo)
                if (usuarioActivo)
                {
                    string updateUsuario = "UPDATE seguridad.usuarios SET estatus = FALSE WHERE id = @IdUsuarioUpdate";
                    var paramUsuarioUpdate = _dbAccess.CreateParameter("@IdUsuarioUpdate", idUsuario);
                    int filasUsuario = _dbAccess.ExecuteNonQuery(updateUsuario, paramUsuarioUpdate);

                    if (filasUsuario > 0)
                    {
                        cambiosRealizados = true;
                        _logger.Info($"Usuario ID {idUsuario} dado de baja");
                    }
                }

                // 5. Baja de persona (con parámetro completamente nuevo)
                if (personaActiva)
                {
                    string updatePersona = "UPDATE seguridad.personas SET estatus = FALSE WHERE id = @IdPersonaUpdate";
                    var paramPersonaUpdate = _dbAccess.CreateParameter("@IdPersonaUpdate", idPersona);
                    int filasPersona = _dbAccess.ExecuteNonQuery(updatePersona, paramPersonaUpdate);

                    if (filasPersona > 0)
                    {
                        cambiosRealizados = true;
                        _logger.Info($"Persona ID {idPersona} dada de baja");
                    }
                }

                if (!usuarioActivo && !personaActiva)
                {
                    _logger.Info($"Usuario {idUsuario} y persona {idPersona} ya estaban inactivos");
                    return true;
                }

                return cambiosRealizados;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, $"Error al dar de baja usuario ID: {idUsuario}");
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }
    }
}