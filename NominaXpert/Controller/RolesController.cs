using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NominaXpert.Data;
using NominaXpert.Model;

namespace NominaXpert.Controller
{
    class RolesController
    {
        private readonly RolesDataAccess _rolesRepo = new();

        public (bool exito, string mensaje) RegistrarRol(Rol rol)
        {
            if (string.IsNullOrWhiteSpace(rol.Nombre))
                return (false, "El nombre del rol no puede estar vacío.");

            bool creado = _rolesRepo.AgregarRol(rol);
            return creado
                ? (true, "Rol registrado correctamente.")
                : (false, "Error al registrar el rol.");
        }

        public (bool exito, string mensaje) ActualizarRol(Rol rol)
        {
            if (rol.Id <= 0)
                return (false, "ID de rol inválido.");

            bool actualizado = _rolesRepo.ActualizarRol(rol);
            return actualizado
                ? (true, "Rol actualizado correctamente.")
                : (false, "Error al actualizar el rol.");
        }

        public List<Rol> ObtenerTodosLosRoles()
        {
            return _rolesRepo.ObtenerTodosLosRoles();
        }

        public List<Permiso> ObtenerPermisosDisponibles()
        {
            return _rolesRepo.ObtenerPermisosPorRol(0); // 0 para obtener todos
        }

    }
}
