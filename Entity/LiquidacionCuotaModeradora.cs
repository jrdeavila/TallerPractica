using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Entity
{
    public class LiquidacionCuotaModeradora
    {
        private Paciente pacienteAtentido;
        private double tarifa, valorServicio;
        private long numeroLiquidacion;
        private DateTime fechaLiquidacion;

        [JsonConstructor]
        public LiquidacionCuotaModeradora(Paciente pacienteAtentido, double valorServicio)
        {
            this.pacienteAtentido = pacienteAtentido;
            this.valorServicio = valorServicio;
            this.tarifa = CalcularTarifa();
            this.numeroLiquidacion = new Random().Next(100000,999999);
            this.fechaLiquidacion = DateTime.Now;
        }

        public Paciente PacienteAtentido { get => pacienteAtentido; set => pacienteAtentido = value; }
        public double Tarifa { get => tarifa; set => tarifa = value; }
        public double ValorServicio { get => valorServicio; set => valorServicio = value; }
        public long NumeroLiquidacion { get => numeroLiquidacion; set => numeroLiquidacion = value; }
        public DateTime FechaLiquidacion { get => fechaLiquidacion; set => fechaLiquidacion = value; }

        public double CalcularSalarios() { return PacienteAtentido.Salario / 900000; }

        public double CalcularTarifa()
        {
            if (CalcularSalarios() <= 0) return 0.05;
            else if (CalcularSalarios() <= 2) return 0.15;
            else if (CalcularSalarios() > 2 && CalcularSalarios() <= 5) return 0.20;
            else return 0.25;
        }

        public double CalcularTopeMaximo()
        {
            if (Tarifa == 0.05) return 200000;
            else if (Tarifa == 0.15) return 250000;
            else if (Tarifa == 0.20) return 900000;
            else return 1500000;
        }

        public double CalcularCuotaModeradora() { return ValorServicio * Tarifa; }

        public string AplicabilidadTopeMaximo()
        {
            if (CalcularCuotaModeradora() > CalcularTopeMaximo()) return "SI";
            else return "NO";
        }
        public double CalcularTotal()
        {
            if (AplicabilidadTopeMaximo().Equals("SI")) { return ValorServicio + CalcularTopeMaximo(); }
            else { return ValorServicio + CalcularCuotaModeradora() ; }
        }
        
        public void MostrarInformacionCuotaModeradora()
        {
            Console.WriteLine("N. Liquidacion: {0}", NumeroLiquidacion);
            Console.WriteLine("Fecha de Liquidacion: {0}", FechaLiquidacion);
            Console.WriteLine("Costo total para liquidar: ${0}", CalcularTotal());
            Console.WriteLine("\t Informacion del paciente");
            PacienteAtentido.MostrarInformacionPaciente();
            Console.WriteLine("\tInformacion de facturacion");
            Console.WriteLine("Valor del servicio: ${0}", ValorServicio);
            Console.WriteLine("Tarifa: {0}%", Tarifa*100);
            Console.WriteLine("Cuota Moderadora: ${0}", CalcularCuotaModeradora());
            Console.WriteLine("Aplicabilidad de tope maximo: {0}", AplicabilidadTopeMaximo());
            Console.WriteLine("Valor del tope maximo: ${0}", CalcularTopeMaximo());
            Console.WriteLine("Costo total: ${0}", CalcularTotal());

        }
    }
}
