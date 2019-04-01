using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XmlToObjectPrueba01
{
    public class Factura
    {
        [XmlArray("PropiedadesFactura")]
        public List<PropiedadesFactura> Facturas { get; set; }

        //public PropiedadesFactura[] Factura { get; set; }
    }
}
