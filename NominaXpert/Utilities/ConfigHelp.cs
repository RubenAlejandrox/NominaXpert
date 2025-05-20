using System;
using System.Configuration;
using NLog;

namespace NominaXpert.Utilities
{
    public static class ConfigHelp
    {
        private static readonly Logger _logger = ControlEscolar.Utilities.LoggingManager.GetLogger("NominaXpert.Utilities.ConfigHelp");

        /// <summary>
        /// Obtiene el valor del sueldo mínimo desde App.config
        /// </summary>
        /// <returns>Sueldo mínimo configurado o valor por defecto</returns>
        public static decimal ObtenerSueldoMinimo()
        {
            try
            {
                string sueldoMinimoStr = ConfigurationManager.AppSettings["SueldoMinimo"];
                if (string.IsNullOrEmpty(sueldoMinimoStr))
                {
                    _logger.Warn("La configuración del sueldo mínimo no está definida en App.config. Se usará el valor predeterminado.");
                    return 8480.17m; // Valor predeterminado
                }

                if (decimal.TryParse(sueldoMinimoStr, out decimal sueldoMinimo))
                {
                    return sueldoMinimo;
                }
                else
                {
                    _logger.Error($"El valor de sueldo mínimo en App.config no es un número válido: {sueldoMinimoStr}");
                    return 8480.17m; // Valor predeterminado
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error al obtener el sueldo mínimo desde App.config");
                return 8480.17m; // Valor predeterminado en caso de error
            }
        }
    }
}