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

        /// <summary>
        /// Convierte un número decimal a letras en formato para nómina
        /// </summary>
        public static string ConvertirNumeroALetras(decimal numero)
        {
            if (numero == 0)
                return "CERO PESOS 00/100 M.N.";

            long entero = (long)Math.Floor(numero);
            int centavos = (int)Math.Round((numero - entero) * 100);

            string letras = NumeroALetras(entero) + $" PESOS {centavos:D2}/100 M.N.";
            return letras.ToUpper();
        }

        private static string NumeroALetras(long numero)
        {
            if (numero == 0) return "cero";

            if (numero < 0) return "menos " + NumeroALetras(Math.Abs(numero));

            string[] unidades = { "", "uno", "dos", "tres", "cuatro", "cinco", "seis", "siete", "ocho", "nueve" };
            string[] especiales = { "diez", "once", "doce", "trece", "catorce", "quince", "dieciséis", "diecisiete", "dieciocho", "diecinueve" };
            string[] decenas = { "", "diez", "veinte", "treinta", "cuarenta", "cincuenta", "sesenta", "setenta", "ochenta", "noventa" };
            string[] centenas = { "", "ciento", "doscientos", "trescientos", "cuatrocientos", "quinientos", "seiscientos", "setecientos", "ochocientos", "novecientos" };

            StringBuilder texto = new StringBuilder();

            if (numero >= 1000000)
            {
                if (numero / 1000000 == 1)
                    texto.Append("un millón ");
                else
                    texto.Append(NumeroALetras(numero / 1000000) + " millones ");

                numero %= 1000000;
            }

            if (numero >= 1000)
            {
                if (numero / 1000 == 1)
                    texto.Append("mil ");
                else
                    texto.Append(NumeroALetras(numero / 1000) + " mil ");

                numero %= 1000;
            }

            if (numero >= 100)
            {
                if (numero == 100)
                {
                    texto.Append("cien ");
                }
                else
                {
                    texto.Append(centenas[numero / 100] + " ");
                }
                numero %= 100;
            }

            if (numero >= 20)
            {
                texto.Append(decenas[numero / 10]);
                if ((numero % 10) != 0)
                    texto.Append(" y " + unidades[numero % 10]);
            }
            else if (numero >= 10)
            {
                texto.Append(especiales[numero - 10]);
            }
            else if (numero > 0)
            {
                texto.Append(unidades[numero]);
            }

            return texto.ToString().Trim();
        }

    }
}