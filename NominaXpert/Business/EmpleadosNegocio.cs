using NominaXpert.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NominaXpert.Business
{
    internal class EmpleadosNegocio
    {
        public static bool EsSalarioValido(string salario)
        {
            return Validaciones.EsCorreoValido(salario);
        }

        public static bool EsMatriculaValido(string matricula)
        {
            return Validaciones.EsNoMatriculaValido(matricula);
        }

    }
}