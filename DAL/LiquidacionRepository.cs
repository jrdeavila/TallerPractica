using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using Newtonsoft.Json;
namespace DAL
{
    public class LiquidacionRepository
    {
        private string path = @"C:\Users\moren.LAPTOP-SHGMIPNO\source\repos\Parcial\DAL\Files\Liquidaciones.json";
        private List<LiquidacionCuotaModeradora> listLiquidaciones;

        public LiquidacionRepository() 
        {
            if (File.Exists(path)) Refrescar();
            else listLiquidaciones = new List<LiquidacionCuotaModeradora>();
        }

        public string Path { get => path; set => path = value; }
        public List<LiquidacionCuotaModeradora> ListLiquidaciones { get => listLiquidaciones; set => listLiquidaciones = value; }

        public void GuardarLiquidacion(LiquidacionCuotaModeradora Liquidacion) {  ListLiquidaciones.Add(Liquidacion); }
        public void GuardarList()
        {
            using(StreamWriter Repository = File.CreateText(path))
            {
                Repository.Write(CrearSerialJson());
            }
        }
        public string CrearSerialJson() { return JsonConvert.SerializeObject(ListLiquidaciones, Formatting.Indented); }
        public string GetSerialJson()
        {
            string Serial;
            try
            {
                using (StreamReader Repository = File.OpenText(path))
                {
                    Serial = Repository.ReadToEnd();
                }
            }
            catch(IOException e)
            {
                Serial = CrearSerialJson();
            }
            
            return Serial;
        }

        public List<LiquidacionCuotaModeradora> ExtraerListRepository() { return JsonConvert.DeserializeObject<List<LiquidacionCuotaModeradora>>(GetSerialJson()); }

        public void Refrescar() { ListLiquidaciones = ExtraerListRepository(); }

    }
}
