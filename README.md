  

� <h1> Portfolio investimento </h1>

  

🎁 Implementação do projeto com .NET utilizando a versão 8

  

![GitHub license](https://img.shields.io/github/license/LeonardoMendes007/investment-portfolio)

![](https://img.shields.io/badge/languege-Portuguese-yellow)

[![GitHub stars](https://img.shields.io/github/stars/LeonardoMendes007/investment-portfolio?color=FFF300&style=social)](https://github.com/LeonardoMendes007/investment-portfolio)

  
  

Tabela de conteúdos

=================

<!--ts-->

* [Sobre](#Sobre)

* [Pre Requisitos](#pre-requisitos)

* [Executando a aplicação](#rodando)

* [Utilizando a aplicação](#aplicacao)

* [Tecnologias](#tecnologias)

* [Autor](#autor)

<!--te-->

## :computer: Sobre<a id="sobre"></a>

  

Foi desenvolvido um sistema de gestão de portfólio de investimentos para consultoria financeira. Permite aos usuários gerenciarem investimentos e aos clientes comprarem, venderem e acompanharem suas carteiras. Oferecendo também uma análise detalhada sobre, transações e acompanhamento de investimento.

### Pré-requisitos<a id="pre-requisitos"></a>

  

Antes de começar, você vai precisar ter instalado em sua máquina a versão 8 do .NET

[.NET 8](https://dotnet.microsoft.com/pt-br/download/dotnet/8.0),

  

Além disto é necessário ter um editor para acompanhar o código, é recomendado o [Visual Studio 2022 ou VsCode](https://visualstudio.microsoft.com/pt-br/downloads/)

### 🎲Executando a Aplicação<a id="rodando"></a>

````bash

# Clone este repositório na pasta de sua preferencia

git  clone  https://github.com/LeonardoMendes007/investment-portfolio.git

# Acesse a pasta do projeto clonado que deve esta com esse nome

investment-portfolio

# Abrindo a aplicação

Abra  a  pasta  e  clique  com  o  botão  direito  do  mouse  nesse  arquivo  "InvestmentPortfolio.sln"

Clique  em  "Abrir Com"  e  selecione  a  IDE  de  sua  preferencia.

# Executando a aplicação no VsCode

1.Navegue  até  o  diretório  onde  está  localizado  o  projeto  .NET.

2.Abra  o  terminal  integrado  no  Visual  Studio  Code (pode ser  acessado  através  de  "Terminal" > "Novo Terminal").

3.No  terminal,  digite  os  seguintes  comandos  nessa  ordem:

"dotnet restore"

"dotnet build"

"dotnet run"

# Executando a aplicação no Visual Studio 2022

Assim  que  a  aplicação  carregar  no  visual  studio,  clique  em  Executar

````

### 🛠 Utilizando a aplicação<a id="aplicacao"></a>

Para utilizar a aplicação basta seguir esse passo a passo:

1.  **Executar a Aplicação na IDE**: Inicie a aplicação na IDE e verifique se o servidor está funcionando.

2.  **Acessar a Interface do Swagger**: No navegador, vá para `https://localhost:7200/swagger/index.html`.

3.  **Selecionar o Método HTTP POST**: No contexto de `Api/Product`, clique no método POST para adicionar um novo produto.

4.  **Preencher as Informações do Api/Product**: Insira as informações necessárias de acordo com o tipo de dados especificado.

5.  **Utilizar o Método GET Api/Product**: Após adicionar o produto, use o método GET para ver todos os produtos e recuperar o ID do novo produto.

6.  **Realizar Operações de Transação**: Para comprar ou vender, vá para os endpoints `/Buy` ou `/Sell`, fornecendo o ID do produto e do usuário e quantidade desejada.

7.  **Consultar Investimentos e Transações do Usuário**: Para ver os investimentos e transações de um usuário, utilize os endpoints `api/customer/{id}/investments`ou`api/customer/{id}/transactions` de consulta com o ID do usuário. Para detalhes específicos, adicione o ID do produto `api/customer/{id}/investments/{productId}`ou`api/customer/{id}/transactions/{productId}`.

  
  

### Tecnologias<a id="tecnologias"></a>

As seguintes ferramentas foram usadas na construção do projeto:

- [.NET ](https://learn.microsoft.com/pt-br/dotnet/)

- [Entity Framework Core](https://learn.microsoft.com/pt-br/ef/core/)

  
  
  
  
  

### Autor <a id="autor">  </a>

  

---

  

<br  />

<span> Feito por Leonardo Mendes 👋 Entre em contato! </span>

</a>

  

[![Linkedin Badge](https://img.shields.io/badge/Leonardo%20Mendes%20-blue?Style=flat&logo=linkedin&labelColor=blue=https://www.linkedin.com/in/matheus-souza-4a4b19189/)](https://www.linkedin.com/in/leonardo-mendes-gomes/)

  

### :page_facing_up: **Licença**

  

Copyright © 2024 [Leonardo Mendes](https://www.linkedin.com/in/leonardo-mendes-gomes/).<br  />
