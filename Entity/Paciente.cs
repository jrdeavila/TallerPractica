using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Paciente
    {
        private string nombre, afiliacion;
        private double salario;
        private long cedula, telefono;

        public string Nombre { get => nombre; set => nombre = value; }
        public string Afiliacion { get => afiliacion; set => afiliacion = value; }
        public double Salario { get => salario; set => salario = value; }
        public long Cedula { get => cedula; set => cedula = value; }
        public long Telefono { get => telefono; set => telefono = value; }

        [JsonConstructor]
        public Paciente(string nombre, string afiliacion, double salario, long cedula, long telefono)
        {
            this.nombre = nombre;
            this.afiliacion = afiliacion;
            this.salario = salario;
            this.cedula = cedula;
            this.telefono = telefono;
        }

        public Paciente(string nombre, long cedula, long telefono)
        {
            this.nombre = nombre;
            this.cedula = cedula;
            this.telefono = telefono;
            this.salario = 0;
            this.afiliacion = "Subcidiado";
        }

        public Paciente(string nombre, long cedula, long telefono, double salario)
        {
            this.nombre = nombre;
            this.salario = salario;
            this.cedula = cedula;
            this.telefono = telefono;
            this.afiliacion = "Contributivo";
        }

        public void MostrarInformacionPaciente()
        {
            if (Afiliacion.Equals("Subcidiado")) MostrarInformacionPacienteSubcidiado();
            else MostrarInformacionPacienteContributivo();
        }

        public void MostrarInformacionPacienteSubcidiado()
        {
            Console.WriteLine("Nombre: {0}", Nombre);
            Console.WriteLine("Cedula: {0}", Cedula);
            Console.WriteLine("Telefono: {0}", Telefono);
            Console.WriteLine("Afiliacion: {0}", Afiliacion);
        }
        public void MostrarInformacionPacienteContributivo()
        {
            Console.WriteLine("Nombre: {0}", Nombre);
            Console.WriteLine("Cedula: {0}", Cedula);
            Console.WriteLine("Telefono: {0}", Telefono);
            Console.WriteLine("Afiliacion: {0}", Afiliacion);
            Console.WriteLine("Salario: {0}", Salario);
        }

    }
}
