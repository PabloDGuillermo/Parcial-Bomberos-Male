using System;
using System.Numerics;
using System.Xml.Serialization;

namespace Entidades
{
    public delegate void FinDeSalida(int bomberoIndex);
    public class Bombero:IArchivo<string>,IArchivo<Bombero>
    {
        private string nombre;
        private List<Salida> salidas;
        private static string ruta;
        private static Random random;
        public event FinDeSalida MarcarFin;

        static Bombero()
        {
            ruta = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            ruta += @"\Bombero.xml";
            random = new Random();
        }
        public Bombero()
        {
            this.salidas = new List<Salida>();
        }
        public Bombero(string nombre):this()
        {
            this.nombre = nombre;
        }
        public string Nombre { get => nombre; set => nombre = value; }
        public List<Salida> Salidas { get => salidas; set => salidas = value; }

        public void Guardar(Bombero info) 
        {
            try
            {

                using (StreamWriter sw = new StreamWriter(ruta))
                {
                    XmlSerializer xmlserializer = new XmlSerializer(typeof(Bombero));//Se serializa un objeto en especifico por eso hay que aclararle el tipo de objeto
                    xmlserializer.Serialize(sw, info);//Toma el objeto y lo serializa.
                }

            }
            catch (Exception ex)
            {
                throw new Exception($"Error al escribir el archivo{ruta}", ex.InnerException);
            }
        }
        public Bombero Leer()
        {
            try
            {
                using (StreamReader sw = new StreamReader(ruta))
                {
                    XmlSerializer xmlserializer = new XmlSerializer(typeof(Bombero));//Se serializa un objeto en especifico por eso hay que aclararle el tipo de objeto
                    return (Bombero)xmlserializer.Deserialize(sw);//Toma el objeto y lo serializa.
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al leer el archivo{ruta}", ex.InnerException);
            }
        }
        void IArchivo<string>.Guardar(string info)
        {
            LogDAO.Guardar(info);
        }
        string IArchivo<string>.Leer()
        {
            return LogDAO.Leer();
        }
        public void AtenderSalida(int bomberoIndex)
        {
            //Agregará una nueva salida a la lista del bombero.
            Salida salida = new Salida();
            salidas.Add(salida);
            //Suspenderá el hilo entre 2 y 4 segundos.
            Thread.Sleep(random.Next(2000,4000));
            //Finalizará la salida.
            salida.FinalizarSalida();
            //Registrará la salida (horario de inicio, horario de fin y duración total) en la tabla log de la base de datos.
            string info = $"Salida finalizada. Inicio{salida.FechaInicio.ToLongTimeString()}. " + $"Fin: {salida.FechaFin.ToLongTimeString()}. Duracion: {salida.TiempoTotal} segundos.";
            ((IArchivo<string>)this).Guardar(info);
            //Avisará mediante el evento MarcarFin que se terminó la salida. Utilizar el parámetro bomberoIndex para informar al formulario cuál bombero fue.
            MarcarFin.Invoke(bomberoIndex);
        }
    }
}
