using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CorreiosPrecosEPrazo.Correios
{
    [XmlRoot(ElementName = "cResultado", Namespace = "http://tempuri.org/")]
    public class cResultadoPreco
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
                public string Valor { get; set; }
                public string ValorMaoPropria { get; set; }
                public string ValorAvisoRecebimento { get; set; }
                public string ValorValorDeclarado { get; set; }
                public string Erro { get; set; }
                public string MsgErro { get; set; }
            }
        }
    }
}
