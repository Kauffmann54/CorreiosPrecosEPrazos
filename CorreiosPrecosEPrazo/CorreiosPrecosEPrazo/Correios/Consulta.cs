using System.Windows.Forms;

namespace CorreiosPrecosEPrazo.Correios
{
    class Consulta
    {
        /// <summary>
        ///     Calcula o preço e o prazo com a data atual 
        /// </summary>
        /// <param name="nCdEmpresa"></param>
        /// <param name="sDsSenha"></param>
        /// <param name="nCdServico"></param>
        /// <param name="sCepOrigem"></param>
        /// <param name="sCepDestino"></param>
        /// <param name="nVlPeso"></param>
        /// <param name="nCdFormato"></param>
        /// <param name="nVlComprimento"></param>
        /// <param name="nVlAltura"></param>
        /// <param name="nVlLargura"></param>
        /// <param name="nVlDiametro"></param>
        /// <param name="sCdMaoPropria"></param>
        /// <param name="nVlValorDeclarado"></param>
        /// <param name="sCdAvisoRecebimento"></param>
        /// 
        public static cResultado ConsultarPrecosEPrazos(string nCdEmpresa, string sDsSenha, string nCdServico, string sCepOrigem, string sCepDestino, string nVlPeso, int nCdFormato, decimal nVlComprimento, decimal nVlAltura, decimal nVlLargura, decimal nVlDiametro, string sCdMaoPropria, decimal nVlValorDeclarado, string sCdAvisoRecebimento)
        {
            sCepOrigem = StringFormatter.FormatarCEP(sCepOrigem);
            sCepDestino = StringFormatter.FormatarCEP(sCepDestino);
            string valorDeclarado = "0";
            string valorAltura = "0";
            string valorLargura = "0";
            string valorComprimento = "0";
            string valorDiametro = "0";
            
            if (sCepOrigem.Length != 8)
            {
                MessageBox.Show("O CEP de origem é inválido");
                return null;
            }
            if (sCepDestino.Length != 8)
            {
                MessageBox.Show("O CEP de destino é inválido");
                return null;
            }

            if(nVlPeso.Equals(""))
            {
                MessageBox.Show("O peso é inválido");
                return null;
            } else if(Helpers.returnDecimal(nVlPeso) <= 0)
            {
                MessageBox.Show("O peso precisa ser maior que zero");
                return null;
            }

            if(nCdFormato == 3)
            {
                if(Helpers.returnDouble(nVlPeso) > 1)
                {
                    MessageBox.Show("O peso para envelope, não pode ser maior que 1Kg");
                    return null;
                }
            }

            if(nVlComprimento < 16 || nVlComprimento > 105)
            {
                MessageBox.Show("O comprimento da embalegem tem que estar entre 16 cm e 105 cm");
                return null;
            }

            if (nVlLargura < 11 || nVlLargura > 105)
            {
                MessageBox.Show("A largura da embalegem tem que estar entre 11 cm e 105 cm");
                return null;
            }

            if (nVlAltura < 2 || nVlAltura > 105)
            {
                MessageBox.Show("A altura da embalegem tem que estar entre 2 cm e 105 cm");
                return null;
            }

            decimal soma = nVlComprimento + nVlLargura + nVlAltura;

            if (soma < 29 || soma > 200)
            {
                MessageBox.Show("A soma resultante do comprimento + largura + altura não deve superar 200 cm");
                return null;
            }

            decimal soma2 = nVlComprimento + (2 * nVlDiametro);

            if(soma2 < 28 && nVlDiametro > 0)
            {
                MessageBox.Show("A soma resultante do comprimento + o dobro do diâmetro não pode ser menor que 28 cm");
                return null;
            }

            if(nVlValorDeclarado > 0)
            {
                if (Helpers.returnDouble(nVlValorDeclarado) < 19.50)
                {
                    MessageBox.Show("O valor declarado precisa ser maior que R$19,50");
                    return null;
                }
            }
            
            valorDeclarado = Helpers.retornarDecimalString(nVlValorDeclarado);
            valorAltura = Helpers.retornarDecimalString(nVlAltura);
            valorLargura = Helpers.retornarDecimalString(nVlLargura);
            valorComprimento = Helpers.retornarDecimalString(nVlComprimento);
            valorDiametro = Helpers.retornarDecimalString(nVlDiametro);

            string url = "http://ws.correios.com.br/calculador/CalcPrecoPrazo.asmx/CalcPrecoPrazo?nCdEmpresa=" + nCdEmpresa + "&sDsSenha=" + sDsSenha + "&nCdServico=" + nCdServico + "&sCepOrigem=" + sCepOrigem + "&sCepDestino=" + sCepDestino + "&nVlPeso=" + nVlPeso + "&nCdFormato=" + nCdFormato + "&nVlComprimento=" + valorComprimento + "&nVlAltura=" + valorAltura + "&nVlLargura=" + valorLargura + "&nVlDiametro=" + valorDiametro + "&sCdMaoPropria=" + sCdMaoPropria + "&nVlValorDeclarado=" + valorDeclarado + "&sCdAvisoRecebimento=" + sCdAvisoRecebimento + "&StrRetorno=xml";
            string retornoPrecosEPrazos = Conexao.consultarCorreios(url);
            return CorreiosSerialization.GetObject<cResultado>(retornoPrecosEPrazos);
        }


        /// <summary>
        ///     Calcula somente o preço com a data atual
        /// </summary>
        /// <param name="nCdEmpresa"></param>
        /// <param name="sDsSenha"></param>
        /// <param name="nCdServico"></param>
        /// <param name="sCepOrigem"></param>
        /// <param name="sCepDestino"></param>
        /// <param name="nVlPeso"></param>
        /// <param name="nCdFormato"></param>
        /// <param name="nVlComprimento"></param>
        /// <param name="nVlAltura"></param>
        /// <param name="nVlLargura"></param>
        /// <param name="nVlDiametro"></param>
        /// <param name="sCdMaoPropria"></param>
        /// <param name="nVlValorDeclarado"></param>
        /// <param name="sCdAvisoRecebimento"></param>
        /// 
        public static cResultadoPreco ConsultarPrecos(string nCdEmpresa, string sDsSenha, string nCdServico, string sCepOrigem, string sCepDestino, string nVlPeso, int nCdFormato, decimal nVlComprimento, decimal nVlAltura, decimal nVlLargura, decimal nVlDiametro, string sCdMaoPropria, decimal nVlValorDeclarado, string sCdAvisoRecebimento)
        {
            sCepOrigem = StringFormatter.FormatarCEP(sCepOrigem);
            sCepDestino = StringFormatter.FormatarCEP(sCepDestino);
            string valorDeclarado = "0";
            string valorAltura = "0";
            string valorLargura = "0";
            string valorComprimento = "0";
            string valorDiametro = "0";

            if (sCepOrigem.Length != 8)
            {
                MessageBox.Show("O CEP de origem é inválido");
                return null;
            }
            if (sCepDestino.Length != 8)
            {
                MessageBox.Show("O CEP de destino é inválido");
                return null;
            }

            if (nVlPeso.Equals(""))
            {
                MessageBox.Show("O peso é inválido");
                return null;
            }
            else if (Helpers.returnDecimal(nVlPeso) <= 0)
            {
                MessageBox.Show("O peso precisa ser maior que zero");
                return null;
            }

            if (nCdFormato == 3)
            {
                if (Helpers.returnDouble(nVlPeso) > 1)
                {
                    MessageBox.Show("O peso para envelope, não pode ser maior que 1Kg");
                    return null;
                }
            }

            if (nVlComprimento < 16 || nVlComprimento > 105)
            {
                MessageBox.Show("O comprimento da embalegem tem que estar entre 16 cm e 105 cm");
                return null;
            }

            if (nVlLargura < 11 || nVlLargura > 105)
            {
                MessageBox.Show("A largura da embalegem tem que estar entre 11 cm e 105 cm");
                return null;
            }

            if (nVlAltura < 2 || nVlAltura > 105)
            {
                MessageBox.Show("A altura da embalegem tem que estar entre 2 cm e 105 cm");
                return null;
            }

            decimal soma = nVlComprimento + nVlLargura + nVlAltura;

            if (soma < 29 || soma > 200)
            {
                MessageBox.Show("A soma resultante do comprimento + largura + altura não deve superar 200 cm");
                return null;
            }

            decimal soma2 = nVlComprimento + (2 * nVlDiametro);

            if (soma2 < 28 && nVlDiametro > 0)
            {
                MessageBox.Show("A soma resultante do comprimento + o dobro do diâmetro não pode ser menor que 28 cm");
                return null;
            }

            if (nVlValorDeclarado > 0)
            {
                if (Helpers.returnDouble(nVlValorDeclarado) < 19.50)
                {
                    MessageBox.Show("O valor declarado precisa ser maior que R$19,50");
                    return null;
                }
            }

            valorDeclarado = Helpers.retornarDecimalString(nVlValorDeclarado);
            valorAltura = Helpers.retornarDecimalString(nVlAltura);
            valorLargura = Helpers.retornarDecimalString(nVlLargura);
            valorComprimento = Helpers.retornarDecimalString(nVlComprimento);
            valorDiametro = Helpers.retornarDecimalString(nVlDiametro);

            string url = "http://ws.correios.com.br/calculador/CalcPrecoPrazo.asmx/CalcPreco?nCdEmpresa=" + nCdEmpresa + "&sDsSenha=" + sDsSenha + "&nCdServico=" + nCdServico + "&sCepOrigem=" + sCepOrigem + "&sCepDestino=" + sCepDestino + "&nVlPeso=" + nVlPeso + "&nCdFormato=" + nCdFormato + "&nVlComprimento=" + valorComprimento + "&nVlAltura=" + valorAltura + "&nVlLargura=" + valorLargura + "&nVlDiametro=" + valorDiametro + "&sCdMaoPropria=" + sCdMaoPropria + "&nVlValorDeclarado=" + valorDeclarado + "&sCdAvisoRecebimento=" + sCdAvisoRecebimento + "&StrRetorno=xml";
            string retornoPrecos = Conexao.consultarCorreios(url);
            return CorreiosSerialization.GetObject<cResultadoPreco>(retornoPrecos);
        }

        /// <summary>
        ///     Calcula somente o prazo com a data atual 
        /// </summary>
        /// <param name="sCepOrigem"></param>
        /// <param name="sCepDestino"></param>
        /// <param name="nCdServico"></param>
        ///
        public static cResultadoPrazo ConsultarPrazo(string sCepOrigem, string sCepDestino, string nCdServico)
        {
            sCepOrigem = StringFormatter.FormatarCEP(sCepOrigem);
            sCepDestino = StringFormatter.FormatarCEP(sCepDestino);

            if (sCepOrigem.Length != 8)
            {
                MessageBox.Show("O CEP de origem é inválido");
                return null;
            }
            if (sCepDestino.Length != 8)
            {
                MessageBox.Show("O CEP de destino é inválido");
                return null;
            }

            string url = "http://ws.correios.com.br/calculador/CalcPrecoPrazo.asmx/CalcPrazo?sCepOrigem=" + sCepOrigem + "&sCepDestino=" + sCepDestino + "&nCdServico=" + nCdServico + "&StrRetorno=xml";
            string retornoPrazos = Conexao.consultarCorreios(url);
            return CorreiosSerialization.GetObject<cResultadoPrazo>(retornoPrazos);
        }

        /// <summary>
        ///     Calcula somente o prazo com uma data especificada 
        /// </summary>
        /// <param name="sCepOrigem"></param>
        /// <param name="sCepDestino"></param>
        /// <param name="nCdServico"></param>
        /// <param name="sDtCalculo"></param>
        ///
        public static cResultadoPrazo ConsultarPrazoData(string sCepOrigem, string sCepDestino, string nCdServico, string sDtCalculo)
        {
            sCepOrigem = StringFormatter.FormatarCEP(sCepOrigem);
            sCepDestino = StringFormatter.FormatarCEP(sCepDestino);

            if (sCepOrigem.Length != 8)
            {
                MessageBox.Show("O CEP de origem é inválido");
                return null;
            }
            if (sCepDestino.Length != 8)
            {
                MessageBox.Show("O CEP de destino é inválido");
                return null;
            }

            if (sDtCalculo.Length != 10)
            {
                MessageBox.Show("A data é inválida. Ex: 13/05/2019");
                return null;
            }

            string url = "http://ws.correios.com.br/calculador/CalcPrecoPrazo.asmx/CalcPrazoData?nCdServico=" + nCdServico + "&sCepOrigem=" + sCepOrigem + "&sCepDestino=" + sCepDestino + "&sDtCalculo=" + sDtCalculo + "&StrRetorno=xml";
            string retornoPrazoData = Conexao.consultarCorreios(url);
            return CorreiosSerialization.GetObject<cResultadoPrazo>(retornoPrazoData);
        }

        /// <summary>
        ///     Lista os serviços que estão disponíveis para cálculo de preço e/ou prazo 
        /// </summary>
        ///
        public static cResultadoServicos ConsultarListaServicos()
        {
            string url = "http://ws.correios.com.br/calculador/CalcPrecoPrazo.asmx/ListaServicos";
            string retornoListaServicos = Conexao.consultarCorreios(url);
            return CorreiosSerialization.GetObject<cResultadoServicos>(retornoListaServicos);
        }

        /// <summary>
        ///     Lista os serviços que são calculados pelo STAR 
        /// </summary>
        ///
        public static cResultadoServicosSTAR ConsultarListaServicosSTAR()
        {
            string url = "http://ws.correios.com.br/calculador/CalcPrecoPrazo.asmx/ListaServicosSTAR";
            string retornoListaServicosSTAR = Conexao.consultarCorreios(url);
            return CorreiosSerialization.GetObject<cResultadoServicosSTAR>(retornoListaServicosSTAR);
        }

        /// <summary>
        ///     Método para mostrar se o trecho consultado utiliza modal aéreo ou terrestre 
        /// </summary>
        /// <param name="nCdServico"></param>
        /// <param name="sCepOrigem"></param>
        /// <param name="sCepDestino"></param>
        ///
        public static cResultadoModal ConsultarModal(string nCdServico, string sCepOrigem, string sCepDestino)
        {
            sCepOrigem = StringFormatter.FormatarCEP(sCepOrigem);
            sCepDestino = StringFormatter.FormatarCEP(sCepDestino);

            if (sCepOrigem.Length != 8)
            {
                MessageBox.Show("O CEP de origem é inválido");
                return null;
            }
            if (sCepDestino.Length != 8)
            {
                MessageBox.Show("O CEP de destino é inválido");
                return null;
            }

            string url = "http://ws.correios.com.br/calculador/CalcPrecoPrazo.asmx/VerificaModal?nCdServico=" + nCdServico + "&sCepOrigem=" + sCepOrigem + "&sCepDestino=" + sCepDestino + "&StrRetorno=xml";
            string retornoModal = Conexao.consultarCorreios(url);
            return CorreiosSerialization.GetObject<cResultadoModal>(retornoModal);
        }
    }
}
