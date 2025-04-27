﻿using System;
using System.Collections.Generic;
using System.Data;
using NominaXpert.Model;
using NominaXpert.Data;
using NominaXpert.Utilities;
using NLog;
using Npgsql;
using ControlEscolar.Data;
using ControlEscolar.Utilities;

namespace NominaXpert.Data
{
    class RolesDataAccess
    {
        private static readonly Logger _logger = LoggingManager.GetLogger("NominaXpert.Data.RolRepository");
        private readonly PostgresSQLDataAccess _dbAccess;

        public RolesDataAccess()
        {
            _dbAccess = PostgresSQLDataAccess.GetInstance();
        }

        public bool AgregarRol(Rol rol)
        {
            try
            {
                string queryRol = @"
                    INSERT INTO seguridad.roles (codigo, nombre, descripcion, estatus)
                    VALUES (@Codigo, @Nombre, @Descripcion, @Estatus)
                    RETURNING id;";

                NpgsqlParameter paramCodigo = _dbAccess.CreateParameter("@Codigo", rol.Codigo);
                NpgsqlParameter paramNombre = _dbAccess.CreateParameter("@Nombre", rol.Nombre);
                NpgsqlParameter paramDescripcion = _dbAccess.CreateParameter("@Descripcion", rol.Descripcion);
                NpgsqlParameter paramEstatus = _dbAccess.CreateParameter("@Estatus", rol.Estatus);

                _dbAccess.Connect();
                int nuevoIdRol = (int)_dbAccess.ExecuteScalar(queryRol,
                    paramCodigo, paramNombre, paramDescripcion, paramEstatus);

                if (nuevoIdRol > 0)
                {
                    foreach (var permiso in rol.Permisos)
                    {
                        string queryPermisoRol = @"
                            INSERT INTO seguridad.permisos_rol (id_rol, id_permiso)
                            VALUES (@IdRol, @IdPermiso)";

                        NpgsqlParameter paramIdRol = _dbAccess.CreateParameter("@IdRol", nuevoIdRol);
                        NpgsqlParameter paramIdPermiso = _dbAccess.CreateParameter("@IdPermiso", permiso.Id);

                        _dbAccess.ExecuteNonQuery(queryPermisoRol, paramIdRol, paramIdPermiso);
                    }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                // loggear error si tienes logger
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        public bool ActualizarRol(Rol rol)
        {
            try
            {
                string query = @"
                    UPDATE seguridad.roles
                    SET codigo = @Codigo, nombre = @Nombre,
                        descripcion = @Descripcion, estatus = @Estatus
                    WHERE id = @Id";

                NpgsqlParameter paramId = _dbAccess.CreateParameter("@Id", rol.Id);
                NpgsqlParameter paramCodigo = _dbAccess.CreateParameter("@Codigo", rol.Codigo);
                NpgsqlParameter paramNombre = _dbAccess.CreateParameter("@Nombre", rol.Nombre);
                NpgsqlParameter paramDescripcion = _dbAccess.CreateParameter("@Descripcion", rol.Descripcion);
                NpgsqlParameter paramEstatus = _dbAccess.CreateParameter("@Estatus", rol.Estatus);

                _dbAccess.Connect();
                int filas = _dbAccess.ExecuteNonQuery(query,
                    paramId, paramCodigo, paramNombre, paramDescripcion, paramEstatus);

                if (filas > 0)
                {
                    // Eliminar permisos anteriores
                    string delete = "DELETE FROM seguridad.permisos_rol WHERE id_rol = @IdRol";
                    NpgsqlParameter paramDel = _dbAccess.CreateParameter("@IdRol", rol.Id);
                    _dbAccess.ExecuteNonQuery(delete, paramDel);

                    // Insertar nuevos permisos
                    foreach (var permiso in rol.Permisos)
                    {
                        string insert = @"
                            INSERT INTO seguridad.permisos_rol (id_rol, id_permiso)
                            VALUES (@IdRol, @IdPermiso)";
                        NpgsqlParameter paramIdRol = _dbAccess.CreateParameter("@IdRol", rol.Id);
                        NpgsqlParameter paramIdPermiso = _dbAccess.CreateParameter("@IdPermiso", permiso.Id);

                        _dbAccess.ExecuteNonQuery(insert, paramIdRol, paramIdPermiso);
                    }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                // loggear error si tienes logger
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        public List<Rol> ObtenerTodosLosRoles()
        {
            List<Rol> roles = new();

            try
            {
                string query = "SELECT id, codigo, nombre, descripcion, estatus FROM seguridad.roles";

                _dbAccess.Connect();
                DataTable dt = _dbAccess.ExecuteQuery_Reader(query);

                foreach (DataRow row in dt.Rows)
                {
                    int idRol = Convert.ToInt32(row["id"]);

                    Rol rol = new Rol
                    {
                        Id = idRol,
                        Codigo = row["codigo"].ToString(),
                        Nombre = row["nombre"].ToString(),
                        Descripcion = row["descripcion"].ToString(),
                        Estatus = Convert.ToBoolean(row["estatus"]),
                        Permisos = ObtenerPermisosPorRol(idRol)
                    };

                    roles.Add(rol);
                }

                return roles;
            }
            catch
            {
                return roles;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }

        public List<Permiso> ObtenerPermisosPorRol(int idRol)
        {
            List<Permiso> permisos = new();

            try
            {
                string query;
                NpgsqlParameter[] parametros;

                if (idRol == 0)
                {
                    // Traer todos los permisos
                    query = @"SELECT id, codigo, descripcion, estatus FROM seguridad.permisos WHERE estatus = true";
                    parametros = Array.Empty<NpgsqlParameter>();
                }
                else
                {
                    // Traer permisos asignados a un rol específico
                    query = @"
                SELECT p.id, p.codigo, p.descripcion, p.estatus
                FROM seguridad.permisos p
                INNER JOIN seguridad.permisos_rol pr ON p.id = pr.id_permiso
                WHERE pr.id_rol = @IdRol";

                    parametros = new[] { _dbAccess.CreateParameter("@IdRol", idRol) };
                }

                DataTable dt = _dbAccess.ExecuteQuery_Reader(query, parametros);

                foreach (DataRow row in dt.Rows)
                {
                    Permiso permiso = new Permiso(
                        Convert.ToInt32(row["id"]),
                        row["codigo"].ToString(),
                        row["descripcion"].ToString(),
                        Convert.ToBoolean(row["estatus"])
                    );

                    permisos.Add(permiso);
                }

                return permisos;
            }
            catch
            {
                return permisos;
            }
        }


        public List<string> ObtenerNombreRol()
        {
            List<string> nombresRoles = new List<string>();

            try
            {
                string query = "SELECT nombre FROM seguridad.roles WHERE estatus = true";

                DataTable dt = _dbAccess.ExecuteQuery_Reader(query, null);

                foreach (DataRow row in dt.Rows)
                {
                    string nombreRol = row["nombre"].ToString();
                    nombresRoles.Add(nombreRol);
                }

                return nombresRoles;
            }
            catch (Exception ex)
            {
                // Opcional: registrar el error (log.Error("Error al obtener roles", ex))
                return nombresRoles; // Devuelve lista vacía en caso de error
            }
        }
        public bool DarDeBajaRol(int idRol)
        {
            try
            {
                string query = "UPDATE seguridad.roles SET estatus = false WHERE id = @IdRol";
                var param = _dbAccess.CreateParameter("@IdRol", idRol);

                _dbAccess.Connect();
                int result = _dbAccess.ExecuteNonQuery(query, param);

                return result > 0;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al dar de baja el rol.");
                return false;
            }
            finally
            {
                _dbAccess.Disconnect();
            }
        }


    }
}