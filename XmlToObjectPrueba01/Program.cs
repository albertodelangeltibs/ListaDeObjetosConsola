using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;

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

            Factura factura = ser.Deserialize<Factura>(xmlInputData);
            
            xmlOutputData = ser.Serialize<Factura>(factura);

            //Console.WriteLine(filepath);
            Console.ReadLine();
        }
    }
}
