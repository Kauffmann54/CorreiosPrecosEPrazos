using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CorreiosPrecosEPrazo.Correios
{
    class Helpers
    {
        /// <summary>
        ///     Retorna um valor do tipo Double
        /// </summary>
        /// <param name="valor"></param>
        ///
        public static double returnDouble(string valor)
        {
            if (valor != null)
            {
                if (valor.Equals(""))
                {
                    return 0;
                }
                return Convert.ToDouble(valor.Replace("R$", "").Replace(" ", ""));
            }
            return 0;
        }

        /// <summary>
        ///     Retorna um valor do tipo Double
        /// </summary>
        /// <param name="valor"></param>
        ///
        public static double returnDouble(decimal valor)
        {
            return Convert.ToDouble(valor);
        }

        /// <summary>
        ///     Retorna um valor do tipo Decimal
        /// </summary>
        /// <param name="valor"></param>
        ///
        public static decimal returnDecimal(string valor)
        {
            if (valor != null)
            {
                if (valor.Equals(""))
                {
                    return 0;
                }
                return Convert.ToDecimal(valor.Replace("R$", "").Replace(" ", ""));
            }
            return 0;
        }

        /// <summary>
        ///     Retorna um valor formatado no padrão de dinheiro
        /// </summary>
        /// <param name="valor"></param>
        ///
        public static string returnDinheiro(double valor)
        {
            CultureInfo cultureInfo = new CultureInfo("pt-BR");
            return string.Format(cultureInfo, "{0:C}", valor);
        }

        /// <summary>
        ///     Retorna um valor formatado no padrão de dinheiro
        /// </summary>
        /// <param name="valor"></param>
        ///
        public static string returnDinheiro(string valor)
        {
            CultureInfo cultureInfo = new CultureInfo("pt-BR");
            return string.Format(cultureInfo, "{0:C}", returnDecimal(valor));
        }

        /// <summary>
        ///     Retorna uma resposta para os serviços dos Correios
        /// </summary>
        /// <param name="valor"></param>
        ///
        public static string returnResposta(string valor)
        {
            switch (valor)
            {
                case "S":
                    return "Sim";
                case "N":
                    return "Não";
                default:
                    return valor;
            }
        }

        /// <summary>
        ///     Retorna uma data para o dia atual
        /// </summary>
        ///
        public static string retornarDataSimples()
        {
            DateTime dataAtual = DateTime.Now;
            string data = dataAtual.ToString("dd/MM/yyyy");
            return data;
        }

        /// <summary>
        ///     Retorna o valor formatado para dinheiro
        /// </summary>
        /// <param name="txt"></param>
        ///
        public static void retornarMoedaFormatada(ref TextBox txt)
        {
            string n = string.Empty;
            double v = 0;
            try
            {
                n = txt.Text.Replace(",", "").Replace(".", "");
                if (n.Equals(""))
                {
                    n = "";
                }
                n = n.PadLeft(3, '0');
                if (n.Length > 3 && n.Substring(0, 1) == "0")
                    n = n.Substring(1, n.Length - 1);
                v = Convert.ToDouble(n) / 100;
                txt.Text = string.Format("{0:N}", v);
                txt.SelectionStart = txt.Text.Length;
            }
            catch (Exception)
            {

            }
        }

        /// <summary>
        ///     Retorna o valor no padrão decimal em string
        /// </summary>
        /// <param name="valor"></param>
        ///
        public static string retornarDecimalString(decimal valor)
        {
            string resultado = valor.ToString().Replace(",", ".");
            return resultado;
        }
    }
}
