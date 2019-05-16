using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorreiosPrecosEPrazo.Correios
{
    class StringFormatter
    {
        /// <summary>
        ///     Formata o valor para o padrão de CEP dos Correios
        /// </summary>
        /// <param name="cep"></param>
        ///
        public static string FormatarCEP(string cep)
        {
            return (cep.Replace("-", "").Replace(" ", ""));
        }
    }
}
