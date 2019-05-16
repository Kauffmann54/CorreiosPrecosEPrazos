using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CorreiosPrecosEPrazo.Correios
{
    [XmlRoot(ElementName = "cResultado", Namespace = "http://tempuri.org/")]
    public class cResultadoPrazo
    {
        [XmlElement(ElementName = "Servicos")]
        public Servicos servicos { get; set; }

        public class Servicos
        {
            [XmlElement(ElementName = "cServico")]
            public List<cServico> CServico { get; set; }

            public class cServico
            {
                public string Codigo { get; set; }
                public string PrazoEntrega { get; set; }
                public string EntregaDomicilar { get; set; }
                public string EntregaSabado { get; set; }
                public string Erro { get; set; }
                public string MsgErro { get; set; }
                public string obsFim { get; set; }
                public string DataMaxEntrega { get; set; }
            }
        }
    }
}
