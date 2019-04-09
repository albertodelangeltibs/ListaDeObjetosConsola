using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Reflection;

namespace XmlToObjectPrueba01
{
    class Program
    {
        static void Main(string[] args)
        {

            Serializer ser = new Serializer();
            string path = string.Empty;
            string xmlInputData = string.Empty;
            string xmlOutputData = string.Empty;

            //string filepath = Directory.GetCurrentDirectory() + @"\Salida.xml";

            // Convert xml to Object
            path = Directory.GetCurrentDirectory() + @"\Salida.xml";
            xmlInputData = File.ReadAllText(path);

            Facturas factura = ser.Deserialize<Facturas>(xmlInputData);

            xmlOutputData = ser.Serialize<Facturas>(factura);

            List<PropiedadesFactura> listaFact = new List<PropiedadesFactura>();
            Concepto concepto = new Concepto();

            for (int i = 0; i < factura.Propiedades.Count; i++)
            {
                listaFact.Add(factura.Propiedades[i]);
            }

            IEnumerable<PropiedadesFactura> distint2 = listaFact.GroupBy(x => x.FolioABA).Select(y => y.First()).Where(element => element.FolioABA != "0").Distinct();

            // IDEA1
            IEnumerable<PropiedadesFactura> distint3 = listaFact.Select(
                    element => element).Where(
                    element => element.IdGeneral == 4);

            //IEnumerable<PropiedadesFactura> distint4 = distint3.GroupBy(x => x.IdConcepto)
            //    .Select(x => x.First()).Distinct();

            List<Concepto> IdConceptos = new List<Concepto>();

            foreach (PropiedadesFactura facturas in distint2) // Repeat twice
            {
                Console.WriteLine(facturas.FolioABA);

                IEnumerable<PropiedadesFactura> distint4 = listaFact.Select(
                   element => element).Where(
                   element => element.IdGeneral == 4 && element.FolioABA == facturas.FolioABA);

                IEnumerable<PropiedadesFactura> distint5 = distint4.GroupBy(x => x.IdConcepto)
                    .Select(y => y.First()).Distinct();

                foreach(PropiedadesFactura createObject in distint5){

                    IEnumerable<PropiedadesFactura> objectCreated = listaFact
                        .Where(e => e.FolioABA == facturas.FolioABA &&
                        e.IdConcepto == createObject.IdConcepto);

                    Concepto Conc = new Concepto();
                    PropertyInfo[] properties = typeof(Concepto).GetProperties();
                    int i = 0;
                    foreach (var Datos in objectCreated)
                    {
                        properties[i].SetValue(Conc, Datos.Dato) ;
                        i++;
                    }
                    i = 0;
                    IdConceptos.Add(Conc);    
                    //Concepto Conc = new Concepto();

                    //PropertyInfo[] properties = typeof(Concepto).GetProperties();
                    //foreach (PropertyInfo property in properties)
                    //{
                    //    //property.SetValue(record, value);
                    //    //Console.WriteLine(property);
                    //    property.SetValue(Conc, value);
                    //}
                }


            }


            //Console.WriteLine(filepath);
            Console.ReadLine();
        }
    }
}
