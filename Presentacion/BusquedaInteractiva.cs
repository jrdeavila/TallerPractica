using BLL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion
{
    public class BusquedaInteractiva
    {
        public BusquedaInteractiva(bool tr) { EscogerEntreOpciones(MostrarOpcioneDeBusqueda()); }
        public BusquedaInteractiva() { }
        public int MostrarOpcioneDeBusqueda()
        {
            Console.Clear();
            Console.WriteLine("\t\tOpciones\n");
            Console.WriteLine("Buscar por numero de liquidacion..............(1)");
            Console.WriteLine("Buscar por fecha de realizacion...............(2)");
            Console.WriteLine("Buscar por nombre del paciente................(3)");
            Console.WriteLine("Buscar por tipo de afiliacion.................(4)");
            Console.WriteLine("Ver todas las liquidaciones...................(5)\n");
            Console.WriteLine("Salir.........................................(0)\n");
            return (int)VariousServices.EsNumero("Seleccione: ");
        }
        public void EscogerEntreOpciones(int OpcionEscogida)
        {
            switch (OpcionEscogida)
            {
                case 0: { break; }
                case 1: { BuscarLiquidacionPorNumero(); Console.ReadKey(); new BusquedaInteractiva(true); break; }
                case 2: { BuscarLiquidacionPorFecha(); Console.ReadKey(); new BusquedaInteractiva(true); break; }
                case 3: { BuscarLiquidacionPorNombre(); Console.ReadKey(); new BusquedaInteractiva(true); break; }
                case 4: { BuscarLiquidacionPorAfiliacion(); Console.ReadKey(); new BusquedaInteractiva(true); break; }
                case 5: { MostrarTodasLasLiquidaciones(); Console.ReadKey(); ; new BusquedaInteractiva(true); break; }
                default: { new BusquedaInteractiva(true); break; }
            }
        }
        public LiquidacionCuotaModeradora BuscarLiquidacionPorNumero()
        {
            int NumeroLiquidacion;
            Console.Clear();
            Console.WriteLine("\tLiquidaciones Registradas\n");
            MostrarTodasLasLiquidaciones();
            Console.WriteLine();
            NumeroLiquidacion = (int)VariousServices.EsNumero("N. liquidacion: ");
            LiquidacionCuotaModeradora Liquidacion = new LiquidacionServices().BuscarPorNumero(NumeroLiquidacion);
            Console.WriteLine();
            if (Liquidacion != null) Liquidacion.MostrarInformacionCuotaModeradora();
            else Console.WriteLine("No hay liquidacion con este numero");
            return Liquidacion;
        }
        public List<LiquidacionCuotaModeradora> BuscarLiquidacionPorFecha()
        {
            int Mes, Año;
            Mes = (int)VariousServices.EsNumero("Mes: ");
            Año = (int)VariousServices.EsNumero("Año: ");
            List<LiquidacionCuotaModeradora> Liquidacion = new LiquidacionServices().BuscarPorFecha(Mes, Año);
            MostrarTodasLasLiquidaciones(Liquidacion);
            return Liquidacion;
        }

        public List<LiquidacionCuotaModeradora> BuscarLiquidacionPorNombre()
        {
            string NombrePaciente;
            Console.Write("Nombre del paciente: "); NombrePaciente = Console.ReadLine();
            List<LiquidacionCuotaModeradora> Liquidacion = new LiquidacionServices().BuscarPorNombre(NombrePaciente);
            MostrarTodasLasLiquidaciones(Liquidacion);
            return Liquidacion;
        }
        public List<LiquidacionCuotaModeradora> BuscarLiquidacionPorAfiliacion()
        {
            string OpcionEscogida;
            List<LiquidacionCuotaModeradora> Liquidacion = null;
            Console.WriteLine("\tEscoge una tipo de afiliacion");
            Console.Write("(C) Contributivo (S) subcidiado: "); OpcionEscogida = Console.ReadLine();
            OpcionEscogida = OpcionEscogida.ToUpper();
            if (OpcionEscogida.Equals("C"))
            {
                Liquidacion = new LiquidacionServices().BuscarPorAfiliacion("Contributivo");
            }
            else if (OpcionEscogida.Equals("S"))
            {
                Liquidacion = new LiquidacionServices().BuscarPorAfiliacion("Subcidiado");
            }
            else Liquidacion = BuscarLiquidacionPorAfiliacion();
            MostrarTodasLasLiquidaciones(Liquidacion);
            return Liquidacion;
        }
        public void MostrarTodasLasLiquidaciones()
        {
            List<LiquidacionCuotaModeradora> ListLiquidaciones = new LiquidacionServices().TodasLasLiquidaciones();
            MostrarTodasLasLiquidaciones(ListLiquidaciones);   
        }
        public void MostrarTodasLasLiquidaciones(List<LiquidacionCuotaModeradora> ListLiquidacion)
        {
            if (ListLiquidacion.Count() > 0)
            {


                foreach (LiquidacionCuotaModeradora i in ListLiquidacion)
                {
                    Console.WriteLine("{0}  Afiliacion: {1}  Salario: ${2} Servicio: ${3} Tarifa: {4}% Cuota: ${5} Tope: {6}",
                        i.NumeroLiquidacion, i.PacienteAtentido.Afiliacion, i.PacienteAtentido.Salario, i.ValorServicio,
                        i.Tarifa*100, i.CalcularCuotaModeradora(), i.AplicabilidadTopeMaximo());
                }
            }
            else Console.WriteLine("No hay liquidaciones con estas caracteristicas");
        }
    }
}
