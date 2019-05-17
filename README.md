# Correios Preços e Prazos

![](https://firebasestorage.googleapis.com/v0/b/whatsapp-541a0.appspot.com/o/Correios_logo.png?alt=media&token=95617299-d20f-4c8f-9d24-6263114644d4) 

Biblioteca para acessar o Webservice dos Correios e verificar o preço e prazo para as encomendas.

* Consulta via Webservice: A interface do WebService pode ser consultada em: http://ws.correios.com.br/calculador/CalcPrecoPrazo.asmx 

## Instalação

```
  // Package Manager
  PM> Install-Package Correios-Precos_e_Prazos -Version 1.0.0 
  
  // .NET CLI
  > dotnet add package Correios-Precos_e_Prazos --version 1.0.0
  
  // Packet CLI
  > paket add Correios-Precos_e_Prazos --version 1.0.0 
```
# Exemplos
## Calcular Preços e Prazos
Calcula o preço e o prazo com a data atual 

Pode testar a demonstração de preços e prazos, baixando esse projeto.
```c#
using CorreiosPrecosEPrazo.Correios;
...
var PrecosEPrazos = Consulta.ConsultarPrecosEPrazos("", "", "40010,40045,40215,40290,41106", "01310-940", "01431-010", "0.5", 1, 16, 11, 12, 0, "N", 150, "N");
foreach(var item in PrecosEPrazos.servicos.CServico)
{
    Console.WriteLine("Código: " + item.Codigo);
    Console.WriteLine("Entrega Domiciliar: " + item.EntregaDomiciliar);
    Console.WriteLine("Entrega sábado: " + item.EntregaSabado);
    Console.WriteLine("Prazo de entrega: " + item.PrazoEntrega + " dia(s) útil");
    Console.WriteLine("Valor: R$" + item.Valor);
    Console.WriteLine("Valor aviso de recebimento: R$" + item.ValorAvisoRecebimento);
    Console.WriteLine("Valor mão própria: R$" + item.ValorMaoPropria);
    Console.WriteLine("Valor sem adicionais: R$" + item.ValorSemAdicionais);
    Console.WriteLine("Valor declarado: R$" + item.ValorValorDeclarado);
    Console.WriteLine("Erro: " + item.Erro);
    Console.WriteLine("Mensagem de erro: " + item.MsgErro);
    Console.WriteLine("Obs: " + item.obsFim);
}
```
## Calcular preços
Calcula somente o preço com a data atual 

```c#
using CorreiosPrecosEPrazo.Correios;
...
var precos = Consulta.ConsultarPrecos("", "", "40010,40045,40215,40290,41106", "01310-940", "01431-010", "0.5", 1, 16, 11, 12, 0, "N", 150, "N");
foreach (var item in precos.servicos.CServico)
{
    Console.WriteLine("Código: " + item.Codigo);
    Console.WriteLine("Valor: R$" + item.Valor);
    Console.WriteLine("Valor aviso de recebimento: R$" + item.ValorAvisoRecebimento);
    Console.WriteLine("Valor mão própria: R$" + item.ValorMaoPropria);
    Console.WriteLine("Valor declarado: R$" + item.ValorValorDeclarado);
    Console.WriteLine("Erro: " + item.Erro);
    Console.WriteLine("Mensagem de erro: " + item.MsgErro);
}
```

## Calcular prazo
Calcula somente o prazo com a data atual

```c#
using CorreiosPrecosEPrazo.Correios;
...
var prazos = Consulta.ConsultarPrazo("01310-940", "01431-010", "40010,40045,40215,40290,41106");
foreach (var item in prazos.servicos.CServico)
{
    Console.WriteLine("Código: " + item.Codigo);
    Console.WriteLine("Data máxima de entrega " + item.DataMaxEntrega);
    Console.WriteLine("Entrega domiciliar " + item.EntregaDomicilar);
    Console.WriteLine("Entrega sábado " + item.EntregaSabado);
    Console.WriteLine("Prazo de entrega " + item.PrazoEntrega);
    Console.WriteLine("Obs " + item.obsFim);
    Console.WriteLine("Erro: " + item.Erro);
    Console.WriteLine("Mensagem de erro: " + item.MsgErro);
}
```

## Calcular prazo com data
Calcula somente o prazo com uma data especificada 

```c#
using CorreiosPrecosEPrazo.Correios;
...
var prazosComData = Consulta.ConsultarPrazoData("01310-940", "01431-010", "40010,40045,40215,40290,41106", "16/05/2019");
foreach (var item in prazosComData.servicos.CServico)
{
    Console.WriteLine("Código: " + item.Codigo);
    Console.WriteLine("Data máxima de entrega " + item.DataMaxEntrega);
    Console.WriteLine("Entrega domiciliar " + item.EntregaDomicilar);
    Console.WriteLine("Entrega sábado " + item.EntregaSabado);
    Console.WriteLine("Prazo de entrega " + item.PrazoEntrega);
    Console.WriteLine("Obs " + item.obsFim);
    Console.WriteLine("Erro: " + item.Erro);
    Console.WriteLine("Mensagem de erro: " + item.MsgErro);
}
```

## Consultar Lista de Serviços
Lista os serviços que estão disponíveis para cálculo de preço e/ou prazo 

```c#
using CorreiosPrecosEPrazo.Correios;
...
var listaServicos = Consulta.ConsultarListaServicos();
foreach (var item in listaServicos.servicosCalculo.CServicosCalculo)
{
    Console.WriteLine("Código: " + item.Codigo);
    Console.WriteLine("Prazo " + item.calcula_prazo);
    Console.WriteLine("Preço " + item.calcula_preco);
    Console.WriteLine("Descrição " + item.descricao);
    Console.WriteLine("Erro: " + item.erro);
    Console.WriteLine("Mensagem de erro: " + item.msgErro);
}
```

## Consultar Lista de Serviços STAR
Lista os serviços que são calculados pelo STAR 

```c#
using CorreiosPrecosEPrazo.Correios;
...
var listaServicosSTAR = Consulta.ConsultarListaServicosSTAR();
foreach (var item in listaServicosSTAR.servicosCalculo.CServicosCalculo)
{
    Console.WriteLine("Código: " + item.Codigo);
    Console.WriteLine("Preço " + item.calcula_preco);
    Console.WriteLine("Descrição " + item.descricao);
    Console.WriteLine("Erro: " + item.erro);
    Console.WriteLine("Mensagem de erro: " + item.msgErro);
}
```

## Consultar Modal
Método para mostrar se o trecho consultado utiliza modal aéreo ou terrestre 

```c#
using CorreiosPrecosEPrazo.Correios;
...
var modal = Consulta.ConsultarModal("40010,40045,40215,40290,41106", "01310-940", "01431-010");
foreach (var item in modal.servicosCalculo.CModal)
{
    Console.WriteLine("Código: " + item.codigo);
    Console.WriteLine("Modal " + item.modal);
    Console.WriteLine("Obs " + item.obs);
}
```

## Licença
  <p>O calculador de preços e prazos de encomendas dos Correios é destinado aos clientes que possuem **contrato** de SEDEX, e-SEDEX e PAC, que necessitam calcular, no seu ambiente e de forma **personalizada**, o preço e o prazo de entrega de uma encomenda.</p>
  
  É possível também a um cliente que **não possui contrato** de encomenda com os Correios realizar o cálculo, porém neste caso os preços apresentados serão aqueles praticados no **balcão da agência**. 

## Notas

Para mais informações sobre o Webservice dos Correios, baixe o PDF oficial com a documentação: [Baixar PDF dos Correios](https://correios.com.br/solucoes-empresariais/comercio-eletronico/palestras-correios-1/pdf/ManualdeImplementacaodoCalculoRemotodePrecosePrazos.pdf/at_download/file)
