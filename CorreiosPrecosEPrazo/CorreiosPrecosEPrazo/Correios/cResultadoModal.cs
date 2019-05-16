using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CorreiosPrecosEPrazo.Correios
{
    [XmlRoot(ElementName = "cResultadoModal", Namespace = "http://tempuri.org/")]
    public class cResultadoModal
    {
        [XmlElement(ElementName = "ServicosModal")]
        public ServicosModal servicosCalculo { get; set; }

        public class ServicosModal
        {
            [XmlElement(ElementName = "cModal")]
            public List<cModal> CModal { get; set; }

            public class cModal
            {
                public string codigo { get; set; }
                public string modal { get; set; }
                public string obs { get; set; }
            }
        }
    }
}
