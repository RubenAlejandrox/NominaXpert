using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NominaXpert.Data;
using NominaXpert.Utilities;

namespace NominaXpert.Business
{
    class NominaNegocio
    {
        public static bool EsMatriculaValido(string matricula)
        {
            return Validaciones.EsNoMatriculaValido(matricula);
        }

        public static bool EsNumeroVakido(string salario)
        {
            return Validaciones.EsNumeroValido(salario);
        }

    }
}
