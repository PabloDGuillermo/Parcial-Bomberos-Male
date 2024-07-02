using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public static class MetodosDeExtension
    {
        public static double CalcularDiferenciaSegundos(this DateTime fechaInicio, DateTime fechaFin)
        {
            return (fechaFin - fechaInicio).TotalSeconds;
        }
    }
}
