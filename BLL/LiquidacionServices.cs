using DAL;
using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class LiquidacionServices
    {
        private LiquidacionCuotaModeradora liquidacion;
        private LiquidacionRepository repository;

        public LiquidacionServices(LiquidacionCuotaModeradora liquidacion)
        {
            this.liquidacion = liquidacion;
            this.repository = new LiquidacionRepository();
        }
        public LiquidacionServices() : this(null) { }

        public LiquidacionCuotaModeradora Liquidacion { get => liquidacion; set => liquidacion = value; }
        public LiquidacionRepository Repository { get => repository; set => repository = value; }

        public void GuardarLiquidacionRepository() { Repository.GuardarLiquidacion(Liquidacion); Repository.GuardarList(); }
        
        public void GuardarLiquidacionRepository(LiquidacionCuotaModeradora Liquidacion)
        {
            Repository.GuardarLiquidacion(Liquidacion);
            Repository.GuardarList();
        }

        public bool EliminarLiquidacion(LiquidacionCuotaModeradora LiquidacionEliminar)
        {
            bool tr = false;
            List<LiquidacionCuotaModeradora> NewList = new List<LiquidacionCuotaModeradora>();
            foreach (LiquidacionCuotaModeradora i in Repository.ListLiquidaciones)
            {
                if (i.NumeroLiquidacion != LiquidacionEliminar.NumeroLiquidacion)
                {
                    NewList.Add(i);
                }
                else
                {
                    tr = true;
                }
            }
            Repository.ListLiquidaciones = NewList;
            Repository.GuardarList();
            Repository.Refrescar();
            return tr;
        }

        public LiquidacionCuotaModeradora BuscarPorNumero(long NumeroLiquidacion)
        {
            LiquidacionCuotaModeradora Liquidacion = null;
            foreach(LiquidacionCuotaModeradora i in Repository.ListLiquidaciones)
            {
                if (i.NumeroLiquidacion == NumeroLiquidacion) Liquidacion = i;
            }
            return Liquidacion;
        }

        public List<LiquidacionCuotaModeradora> BuscarPorFecha(int Mes, int Año)
        {
            List<LiquidacionCuotaModeradora> ListLiquidacion = new List<LiquidacionCuotaModeradora>();
            foreach (LiquidacionCuotaModeradora i in Repository.ListLiquidaciones)
            {
                if (i.FechaLiquidacion.Month == Mes && i.FechaLiquidacion.Year == Año) ListLiquidacion.Add(i);
            }
            return ListLiquidacion;
        }

        public List<LiquidacionCuotaModeradora> BuscarPorNombre(String NombrePaciente)
        {
            List<LiquidacionCuotaModeradora> ListLiquidacion = new List<LiquidacionCuotaModeradora>();
            foreach (LiquidacionCuotaModeradora i in Repository.ListLiquidaciones)
            {
                if (i.PacienteAtentido.Nombre.Contains(NombrePaciente)) ListLiquidacion.Add(i);
            }
            return ListLiquidacion;
        }

        public List<LiquidacionCuotaModeradora> BuscarPorAfiliacion(string Afiliacion)
        {
            List<LiquidacionCuotaModeradora> ListLiquidacion = new List<LiquidacionCuotaModeradora>();
            foreach (LiquidacionCuotaModeradora i in Repository.ListLiquidaciones)
            {
                if (i.PacienteAtentido.Afiliacion.Equals(Afiliacion)) ListLiquidacion.Add(i);
            }
            return ListLiquidacion;
        }

        public List<LiquidacionCuotaModeradora> TodasLasLiquidaciones() { return Repository.ListLiquidaciones; }

    }
}
