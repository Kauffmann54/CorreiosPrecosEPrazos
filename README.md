# Correios Preços e Prazos

Biblioteca para acessar o Webservice dos Correios e verificar o preço e prazo para as encomendas.

## Preços e Prazos
Calcula o preço e o prazo com a data atual 
```
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
