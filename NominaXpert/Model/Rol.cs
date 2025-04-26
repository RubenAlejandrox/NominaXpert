using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaXpert.Model
{
    public class Rol
    {
        public int Id { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Estatus { get; set; }

        // Constructor predeterminado
        public Rol()
        {
            Codigo = string.Empty;
            Nombre = string.Empty;
            Descripcion = string.Empty;
            Estatus = true; // Por defecto, los roles están activos
        }

        // Constructor con los campos necesarios
        public Rol(string codigo, string nombre, string descripcion)
        {
            Codigo = codigo;
            Nombre = nombre;
            Descripcion = descripcion;
            Estatus = true; // Por defecto, el rol está activo
        }

        // Constructor completo
        public Rol(int id, string codigo, string nombre, string descripcion, bool estatus)
        {
            Id = id;
            Codigo = codigo;
            Nombre = nombre;
            Descripcion = descripcion;
            Estatus = estatus;
        }
    }

}
