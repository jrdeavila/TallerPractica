using BLL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentacion
{
    class GestionCuotaModeradora
    {
        private LiquidacionServices ServiciosDeLiquidacion = new LiquidacionServices();
        public GestionCuotaModeradora() { EscogerEntreOpciones(MostrarOpcioneDeBusqueda()); }
        public int MostrarOpcioneDeBusqueda()
        {
            Console.Clear();
            Console.WriteLine("\t\tOpciones\n");
            Console.WriteLine("Generar Liquidacion...........................(1)");
            Console.WriteLine("Eliminar Liquidacion..........................(2)");
            Console.WriteLine("Modificar Liquidacion.........................(3)");
            Console.WriteLine("Busqueda Interactiva..........................(4)\n");
            Console.WriteLine("Salir.........................................(0)\n");
            return (int)VariousServices.EsNumero("Seleccione: ");
        }
        public void EscogerEntreOpciones(int OpcionEscogida)
        {
            switch (OpcionEscogida)
            {
                case 0: { break; }
                case 1: { OpcionDeGuardado(GenerarLiquidacion(CrearPaciente())); Console.ReadKey(); new GestionCuotaModeradora(); break; }
                case 2: { EliminarLiquidacion(); Console.ReadKey(); new GestionCuotaModeradora(); break; }
                case 3: { ModificarLiquidacion(); Console.ReadKey(); new GestionCuotaModeradora(); break; }
                case 4: { new BusquedaInteractiva(true); Console.ReadKey(); new GestionCuotaModeradora(); break; }
                default: { new GestionCuotaModeradora(); break; }
            }

        }
        public Paciente CrearPaciente()
        {
            string Nombre, TipoAfiliacion;
            long Cedula, Telefono;
            double Salario;
            Console.Clear();
            Console.WriteLine("\tInformacion del paciente");
            Console.Write("Nombre del paciente: "); Nombre = Console.ReadLine();
            Cedula = (long)VariousServices.EsNumero("Cedula: ");
            Telefono = (long) VariousServices.EsNumero("Telefono: ");
            Console.WriteLine("\tEscoga el tipo de afiliacion");
            Console.Write("(C) Contributivo (S) Subcidiado: "); TipoAfiliacion = Console.ReadLine();
            TipoAfiliacion = TipoAfiliacion.ToUpper();

            if (TipoAfiliacion.Equals("C"))
            {
                Salario = VariousServices.EsNumero("Salario del paciente: ");
                return new Paciente(Nombre, Cedula, Telefono, Salario);
            }
            else if (TipoAfiliacion.Equals("S"))
            {
                return new Paciente(Nombre, Cedula, Telefono);
            }
            else Console.WriteLine("La opcion no es valida"); return CrearPaciente();
        }
        public LiquidacionCuotaModeradora GenerarLiquidacion(Paciente PacienteAtendido)
        {
            Console.WriteLine();
            double ValorServicio = VariousServices.EsNumero("Valor del servicio realizado: ");
            return new LiquidacionCuotaModeradora(PacienteAtendido, ValorServicio);
        }

        public void OpcionDeGuardado(LiquidacionCuotaModeradora LiquidacionAGuadar)
        {
            string OpcionEscogida;
            Console.Write("Desea guardar esta informacion? S/N: "); OpcionEscogida = Console.ReadLine();
            OpcionEscogida = OpcionEscogida.ToUpper();
            if (OpcionEscogida.Equals("S"))
            {
                ServiciosDeLiquidacion.GuardarLiquidacionRepository(LiquidacionAGuadar);
                Console.WriteLine("La informacion se guardo correctamente");
            }
            else if (OpcionEscogida.Equals("N"))
            {
                Console.WriteLine("La informacion no se guardo");
            }
            else { Console.WriteLine("La opcion no es valida"); OpcionDeGuardado(LiquidacionAGuadar); }
        }

        public void EliminarLiquidacion()
        {
            LiquidacionCuotaModeradora Liquidacion = new BusquedaInteractiva().BuscarLiquidacionPorNumero();
            if(Liquidacion != null)
            {
                bool tr = ServiciosDeLiquidacion.EliminarLiquidacion(Liquidacion);
                if (tr) Console.WriteLine("La liquidacion fue eliminada con exito");
                else Console.WriteLine("La liquidacion no se elimino");
            }
        }

        public void ModificarLiquidacion()
        {
            LiquidacionCuotaModeradora Liquidacion = new BusquedaInteractiva().BuscarLiquidacionPorNumero();
            if (Liquidacion != null)
            {
                double nuevoValorServicio = VariousServices.EsNumero("\nNuevo valor del servicio: ");
                Liquidacion.ValorServicio = nuevoValorServicio;
                if (ServiciosDeLiquidacion.EliminarLiquidacion(Liquidacion))
                {
                    ServiciosDeLiquidacion.GuardarLiquidacionRepository(Liquidacion); 
                    Console.WriteLine("Modificacion realizada con exito");
                }
                else Console.WriteLine("Ocurrio un error en el proceso");
            }
        }
    }
}
