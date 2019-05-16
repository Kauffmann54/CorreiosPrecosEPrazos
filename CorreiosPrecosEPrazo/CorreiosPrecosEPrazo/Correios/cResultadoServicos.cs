using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CorreiosPrecosEPrazo.Correios
{
    [XmlRoot(ElementName = "cResultadoServicos", Namespace = "http://tempuri.org/")]
    public class cResultadoServicos
    {
        [XmlElement(ElementName = "ServicosCalculo")]
        public ServicosCalculo servicosCalculo { get; set; }

        public class ServicosCalculo
        {
            [XmlElement(ElementName = "cServicosCalculo")]
            public List<cServicosCalculo> CServicosCalculo { get; set; }

            public class cServicosCalculo
            {
                public string Codigo { get; set; }
                public string descricao { get; set; }
                public string calcula_preco { get; set; }
                public string calcula_prazo { get; set; }
                public string erro { get; set; }
                public string msgErro { get; set; }
            }
        }
    }
}
