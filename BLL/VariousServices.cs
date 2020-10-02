using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class VariousServices
    {
        public static double EsNumero(string Cadena)
        {
            double Dato;
            try
            {
                Console.Write(Cadena);   Dato = double.Parse(Console.ReadLine());
            }
            catch(FormatException e)
            {
                Console.WriteLine("El dato ingresado no es numerico");
                Dato = EsNumero(Cadena);
            }

            return Dato;
        }
    }
}
