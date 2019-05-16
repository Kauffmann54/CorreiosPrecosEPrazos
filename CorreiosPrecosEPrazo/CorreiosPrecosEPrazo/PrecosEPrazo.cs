using CorreiosPrecosEPrazo.Correios;
using System;
using System.Windows.Forms;

namespace CorreiosPrecosEPrazo
{
    public partial class PrecosEPrazo : Form
    {
        public PrecosEPrazo()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panelSedex.Visible = false; // esconde a tela com os dados calculados
            // Mostra apenas as entradas obrigatórias para Caixa/Pacote
            tbLargura.Visible = true; 
            tbComprimento.Visible = true;
            tbAltura.Visible = true;
            tbDiametro.Visible = false;
            //////
        }


        /// <summary>
        ///     Click do botão buscar no Correio
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            /// Declaração de variáveis para passar como parâmetro
            string CdEmpresa = "";
            string DsSenha = "";
            int tipoDeServico = cbServico.SelectedIndex;
            if (cbServico.Text.Equals("")) tipoDeServico = -1;
            string servico = retornarServico(tipoDeServico);
            string cepDeOrigem = tbCepOrigem.Text;
            string cepDeDestino = tbCepDestino.Text;
            string peso = tbPeso.Text;
            int formato = 1;
            decimal comprimento = Helpers.returnDecimal(tbComprimento.Text);
            decimal altura = Helpers.returnDecimal(tbAltura.Text);
            decimal largura = Helpers.returnDecimal(tbLargura.Text);
            decimal diametro = Helpers.returnDecimal(tbDiametro.Text);
            string maoPropria = "N";
            decimal valorDeclarado = 0;
            string avisoDeRecebimento = "N";
            ////////////////////////////////////////
            
            /// Validação das entradas ////
            if (rbCaixa.Checked)
            {
                formato = 1;
            }
            else if (rbCilindro.Checked)
            {
                formato = 2;
            }
            else
            {
                formato = 3;
            }


            if (cbMaoPropria.Checked)
            {
                maoPropria = "S";
            }
            else
            {
                maoPropria = "N";
            }

            if (cbAvisoRecebimento.Checked)
            {
                avisoDeRecebimento = "S";
            }
            else
            {
                avisoDeRecebimento = "N";
            }

            if (cbDeclaracaoDeValor.Checked)
            {
                valorDeclarado = Helpers.returnDecimal(tbDeclaracaoValor.Text);
            }
            else
            {
                valorDeclarado = 0;
            }

            /////////////////////

            /// Chamada do procedimento para consultar preços e prazos nos correios
            cResultado precoEPrazo = Consulta.ConsultarPrecosEPrazos(CdEmpresa, DsSenha, servico, cepDeOrigem, cepDeDestino, peso, formato, comprimento, altura, largura, diametro, maoPropria, valorDeclarado, avisoDeRecebimento);

            if (precoEPrazo != null)
            {
                panelSedex.Visible = true; // Mostra o painel que estava oculto
                inicializarEntradas(precoEPrazo); // inicializa as entradas de acordo com o retorno dos Correios
            }
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        ///     Deixa a tela com os dados dos serviços oculto.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void btnFechar_Click(object sender, EventArgs e)
        {
            panelSedex.Visible = false;
            reiniciarEntradasDoSedex();
            reiniciarEntradasDoSedex10();
            reiniciarEntradasDoSedexHoje();
            reiniciarEntradasDoPAC();
        }

        /// <summary>
        ///     Mostra apenas as entradas obrigatórias para Caixa/Pacote
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void rbCaixa_Click(object sender, EventArgs e)
        {
            tbAltura.Visible = true;
            tbLargura.Visible = true;
            tbComprimento.Visible = true;
            tbDiametro.Visible = false;
        }

        /// <summary>
        ///     Mostra apenas as entradas obrigatórias para Envelope
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void rbEnvelope_Click(object sender, EventArgs e)
        {
            tbLargura.Visible = true;
            tbComprimento.Visible = true;
            tbAltura.Visible = false;
            tbDiametro.Visible = false;
        }

        /// <summary>
        ///     Mostra apenas as entradas obrigatórias para Rolo/Cilindro ou Esfera
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void rbCilindro_Click(object sender, EventArgs e)
        {
            tbLargura.Visible = false;
            tbComprimento.Visible = true;
            tbAltura.Visible = false;
            tbDiametro.Visible = true;
        }

        /// <summary>
        ///     Permite apenas números no TextBox altura
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void tbAltura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        ///     Permite apenas números no TextBox largura
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void tbLargura_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        ///     Permite apenas números no TextBox comprimento
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void tbComprimento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        ///     Permite apenas números no TextBox diâmetro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void tbDiametro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        ///     Permite apenas números no TextBox peso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void tbPeso_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        ///     Permite apenas números no TextBox declaração de valor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void tbDeclaracaoValor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        ///     Formata o texto para dinheiro
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void tbDeclaracaoValor_TextChanged(object sender, EventArgs e)
        {
            Helpers.retornarMoedaFormatada(ref tbDeclaracaoValor);
        }

        /// <summary>
        ///     Formata o texto para peso
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void tbPeso_TextChanged(object sender, EventArgs e)
        {
            Helpers.retornarMoedaFormatada(ref tbPeso);
        }

        /// <summary>
        ///     Formata o texto para decimal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void tbAltura_TextChanged(object sender, EventArgs e)
        {
            Helpers.retornarMoedaFormatada(ref tbAltura);
        }

        /// <summary>
        ///     Formata o texto para decimal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void tbLargura_TextChanged(object sender, EventArgs e)
        {
            Helpers.retornarMoedaFormatada(ref tbLargura);
        }

        /// <summary>
        ///     Formata o texto para decimal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void tbComprimento_TextChanged(object sender, EventArgs e)
        {
            Helpers.retornarMoedaFormatada(ref tbComprimento);
        }

        /// <summary>
        ///     Formata o texto para decimal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
        private void tbDiametro_TextChanged(object sender, EventArgs e)
        {
            Helpers.retornarMoedaFormatada(ref tbDiametro);
        }


        /// <summary>
        ///     Retorna o valor do serviço dos correios, de acordo com a entrada do index do item selecionado no comboBox de serviços.
        /// </summary>
        /// <param name="index"></param>
        ///
        private string retornarServico(int index)
        {
            switch (index)
            {
                case 0:
                    return "40010, 40215, 40290, 41106";
                case 1:
                    return "40010";
                case 2:
                    return "40045";
                case 3:
                    return "40215";
                case 4:
                    return "40290";
                case 5:
                    return "41106";
                default:
                    return "40010, 40215, 40290, 41106";
            }
        }

        /// <summary>
        ///     Inicializa as entradas com os valores de cada serviço requisitado dos correios 
        /// </summary>
        /// <param name="precoEPrazo"></param>
        ///
        private void inicializarEntradas(cResultado precoEPrazo)
        {
            lbDiaAtual.Text = Helpers.retornarDataSimples();
            foreach (var item in precoEPrazo.servicos.CServico)
            {
                escolherCodigo(item.Codigo, item);
            }
        }

        /// <summary>
        ///     De acordo com o valor recebido dos serviços dos Correios, inicializa cada componente da tela
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="precoEPrazo"></param>
        ///
        private void escolherCodigo(string codigo, cResultado.Servicos.cServico precoEPrazo)
        {
            switch (codigo)
            {
                case "40010":
                    inicializarSedex(precoEPrazo);
                    break;
                case "40045":
                    inicializarSedex(precoEPrazo);
                    break;
                case "40215":
                    inicializarSedex10(precoEPrazo);
                    break;
                case "40290":
                    inicializarSedexHoje(precoEPrazo);
                    break;
                case "41106":
                    inicializarSedexPAC(precoEPrazo);
                    break;
            }
        }

        /// <summary>
        ///     Inicializa as entradas dos componentes do Sedex
        /// </summary>
        /// <param name="precoEPrazo"></param>
        ///
        private void inicializarSedex(cResultado.Servicos.cServico precoEPrazo)
        {
            lbEntregaSedex.Text = "Dia da postagem + " + precoEPrazo.PrazoEntrega + " dia(s) útil";
            lbPrecoSedex.Text = Helpers.returnDinheiro(precoEPrazo.ValorSemAdicionais);
            lbMaoSedex.Text = Helpers.returnDinheiro(precoEPrazo.ValorMaoPropria);
            lbRecebimentoSedex.Text = Helpers.returnDinheiro(precoEPrazo.ValorAvisoRecebimento);
            lbValorDeclaradoSedex.Text = Helpers.returnDinheiro(precoEPrazo.ValorValorDeclarado);
            lbDomiciliarSedex.Text = Helpers.returnResposta(precoEPrazo.EntregaDomiciliar);
            lbSabadoSedex.Text = Helpers.returnResposta(precoEPrazo.EntregaSabado);
            lbErroSedex.Text = precoEPrazo.Erro + "";
            lbMsgErroSedex.Text = precoEPrazo.MsgErro;
            lbSemAdicionaisSedex.Text = Helpers.returnDinheiro(precoEPrazo.ValorSemAdicionais);
            rtbObsSedex.Text = precoEPrazo.obsFim;
            lbTotalSedex.Text = Helpers.returnDinheiro(precoEPrazo.Valor);
        }

        /// <summary>
        ///     Inicializa as entradas dos componentes do Sedex10
        /// </summary>
        /// <param name="precoEPrazo"></param>
        ///
        private void inicializarSedex10(cResultado.Servicos.cServico precoEPrazo)
        {
            lbEntregaSedex10.Text = "Dia da postagem + " + precoEPrazo.PrazoEntrega + " dia(s) útil";
            lbPrecoSedex10.Text = Helpers.returnDinheiro(precoEPrazo.ValorSemAdicionais);
            lbMaoSedex10.Text = Helpers.returnDinheiro(precoEPrazo.ValorMaoPropria);
            lbRecebimentoSedex10.Text = Helpers.returnDinheiro(precoEPrazo.ValorAvisoRecebimento);
            lbValorDeclaradoSedex10.Text = Helpers.returnDinheiro(precoEPrazo.ValorValorDeclarado);
            lbDomiciliarSedex10.Text = Helpers.returnResposta(precoEPrazo.EntregaDomiciliar);
            lbSabadoSedex10.Text = Helpers.returnResposta(precoEPrazo.EntregaSabado);
            lbErroSedex10.Text = precoEPrazo.Erro + "";
            lbMsgErroSedex10.Text = precoEPrazo.MsgErro;
            lbSemAdicionaisSedex10.Text = Helpers.returnDinheiro(precoEPrazo.ValorSemAdicionais);
            rtbObsSedex10.Text = precoEPrazo.obsFim;
            lbTotalSedex10.Text = Helpers.returnDinheiro(precoEPrazo.Valor);
        }

        /// <summary>
        ///     Inicializa as entradas dos componentes do SedexHoje
        /// </summary>
        /// <param name="precoEPrazo"></param>
        ///
        private void inicializarSedexHoje(cResultado.Servicos.cServico precoEPrazo)
        {
            lbEntregaSedexHoje.Text = "Dia da postagem + " + precoEPrazo.PrazoEntrega + " dia(s) útil";
            lbPrecoSedexHoje.Text = Helpers.returnDinheiro(precoEPrazo.ValorSemAdicionais);
            lbMaoSedexHoje.Text = Helpers.returnDinheiro(precoEPrazo.ValorMaoPropria);
            lbRecebimentoSedexHoje.Text = Helpers.returnDinheiro(precoEPrazo.ValorAvisoRecebimento);
            lbValorDeclaradoSedexHoje.Text = Helpers.returnDinheiro(precoEPrazo.ValorValorDeclarado);
            lbDomiciliarSedexHoje.Text = Helpers.returnResposta(precoEPrazo.EntregaDomiciliar);
            lbSabadoSedexHoje.Text = Helpers.returnResposta(precoEPrazo.EntregaSabado);
            lbErroSedexHoje.Text = precoEPrazo.Erro + "";
            lbMsgErroSedexHoje.Text = precoEPrazo.MsgErro;
            lbSemAdicionaisSedexHoje.Text = Helpers.returnDinheiro(precoEPrazo.ValorSemAdicionais);
            rtbObsSedexHoje.Text = precoEPrazo.obsFim;
            lbTotalSedexHoje.Text = Helpers.returnDinheiro(precoEPrazo.Valor);
        }

        /// <summary>
        ///     Inicializa as entradas dos componentes do PAC
        /// </summary>
        /// <param name="precoEPrazo"></param>
        ///
        private void inicializarSedexPAC(cResultado.Servicos.cServico precoEPrazo)
        {
            lbEntregaPAC.Text = "Dia da postagem + " + precoEPrazo.PrazoEntrega + " dia(s) útil";
            lbPrecoPAC.Text = Helpers.returnDinheiro(precoEPrazo.ValorSemAdicionais);
            lbMaoPAC.Text = Helpers.returnDinheiro(precoEPrazo.ValorMaoPropria);
            lbRecebimentoPAC.Text = Helpers.returnDinheiro(precoEPrazo.ValorAvisoRecebimento);
            lbValorDeclaradoPAC.Text = Helpers.returnDinheiro(precoEPrazo.ValorValorDeclarado);
            lbDomiciliarPAC.Text = Helpers.returnResposta(precoEPrazo.EntregaDomiciliar);
            lbSabadoPAC.Text = Helpers.returnResposta(precoEPrazo.EntregaSabado);
            lbErroPAC.Text = precoEPrazo.Erro + "";
            lbMsgErroPAC.Text = precoEPrazo.MsgErro;
            lbSemAdicionaisPAC.Text = Helpers.returnDinheiro(precoEPrazo.ValorSemAdicionais);
            rtbObsPAC.Text = precoEPrazo.obsFim;
            lbTotalPAC.Text = Helpers.returnDinheiro(precoEPrazo.Valor);
        }

        /// <summary>
        ///     Reinicia as entradas dos componentes do Sedex
        /// </summary>
        private void reiniciarEntradasDoSedex()
        {
            lbEntregaSedex.Text = String.Empty;
            lbPrecoSedex.Text = String.Empty;
            lbMaoSedex.Text = String.Empty;
            lbRecebimentoSedex.Text = String.Empty;
            lbValorDeclaradoSedex.Text = String.Empty;
            lbDomiciliarSedex.Text = String.Empty;
            lbSabadoSedex.Text = String.Empty;
            lbErroSedex.Text = String.Empty;
            lbMsgErroSedex.Text = String.Empty;
            lbSemAdicionaisSedex.Text = String.Empty;
            rtbObsSedex.Text = String.Empty;
            lbTotalSedex.Text = String.Empty;
        }

        /// <summary>
        ///     Reinicia as entradas dos componentes do Sedex10
        /// </summary>
        private void reiniciarEntradasDoSedex10()
        {
            lbEntregaSedex10.Text = String.Empty;
            lbPrecoSedex10.Text = String.Empty;
            lbMaoSedex10.Text = String.Empty;
            lbRecebimentoSedex10.Text = String.Empty;
            lbValorDeclaradoSedex10.Text = String.Empty;
            lbDomiciliarSedex10.Text = String.Empty;
            lbSabadoSedex10.Text = String.Empty;
            lbErroSedex10.Text = String.Empty;
            lbMsgErroSedex10.Text = String.Empty;
            lbSemAdicionaisSedex10.Text = String.Empty;
            rtbObsSedex10.Text = String.Empty;
            lbTotalSedex10.Text = String.Empty;
        }

        /// <summary>
        ///     Reinicia as entradas dos componentes do Sedex Hoje
        /// </summary>
        private void reiniciarEntradasDoSedexHoje()
        {
            lbEntregaSedexHoje.Text = String.Empty;
            lbPrecoSedexHoje.Text = String.Empty;
            lbMaoSedexHoje.Text = String.Empty;
            lbRecebimentoSedexHoje.Text = String.Empty;
            lbValorDeclaradoSedexHoje.Text = String.Empty;
            lbDomiciliarSedexHoje.Text = String.Empty;
            lbSabadoSedexHoje.Text = String.Empty;
            lbErroSedexHoje.Text = String.Empty;
            lbMsgErroSedexHoje.Text = String.Empty;
            lbSemAdicionaisSedexHoje.Text = String.Empty;
            rtbObsSedexHoje.Text = String.Empty;
            lbTotalSedexHoje.Text = String.Empty;
        }

        /// <summary>
        ///     Reinicia as entradas dos componentes do PAC
        /// </summary>
        private void reiniciarEntradasDoPAC()
        {
            lbEntregaPAC.Text = String.Empty;
            lbPrecoPAC.Text = String.Empty;
            lbMaoPAC.Text = String.Empty;
            lbRecebimentoPAC.Text = String.Empty;
            lbValorDeclaradoPAC.Text = String.Empty;
            lbDomiciliarPAC.Text = String.Empty;
            lbSabadoPAC.Text = String.Empty;
            lbErroPAC.Text = String.Empty;
            lbMsgErroPAC.Text = String.Empty;
            lbSemAdicionaisPAC.Text = String.Empty;
            rtbObsPAC.Text = String.Empty;
            lbTotalPAC.Text = String.Empty;
        }

    }
}
