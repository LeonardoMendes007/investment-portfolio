
  

<h1> Portfolio investimento </h1>

  

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

* [Teste de carga](#carga) 

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

Caso você queira apenas utilizar a aplicação, pode preferir utilizar a imagem docker em sua última versão da disponível no [Docker Hub](https://hub.docker.com/repository/docker/leonardomendes/investment-portfolio/general)

Para utilizar este método, é necessário ter o Docker instalado previamente e executar o seguinte comando Docker para baixar a imagem da aplicação e executá-la.

````bash
    docker run -d -p 7200:80 --name investment-portfolio-api leonardomendes/investment-portfolio:latest
````



### 🛠 Utilizando a aplicação<a id="aplicacao"></a>

Para utilizar a aplicação basta seguir esse passo a passo:

1.  **Executar a Aplicação na IDE**: Inicie a aplicação na IDE e verifique se o servidor está funcionando.

2.  **Acessar a Interface do Swagger**: No navegador, vá para `http://localhost:7200/swagger/index.html`.

3.  **Selecionar o Método HTTP POST**: No contexto de `api/product`, clique no método POST para adicionar um novo produto.

4.  **Preencher as Informações do Api/Product**: Insira as informações necessárias de acordo com o tipo de dados especificado.

5.  **Utilizar o Método GET Api/Product**: Após adicionar o produto, use o método GET para ver todos os produtos e recuperar o ID do novo produto.

6. **Utilizar o Método GET Api/Product/{id}/Extract**: Para consultar o extrato baseado em um produto basta utilizar o seguinte endpoint `api/product/{id}/extract` .

7.  **Realizar Operações de Transação**: Para comprar ou vender, vá para os endpoints `/Buy` ou `/Sell`, fornecendo o ID do produto e do usuário e quantidade desejada.

  ````bash
# Observação sobre o ID do usuário

Para realizar as consultas de transações customizadas pelo usuário utilizar os seguintes ID's:

"FE232D84-BE96-4669-954C-215B65F6DBE4" : este usuário tem R$ 1.000,00 de Saldo.
"E981D6BA-4CC3-4BF8-B1CC-5F78A4E0578D" : este usuário tem R$ 1.000.000,00 de Saldo.
"427B9E92-A316-4AD6-853F-E488E3EE3972" : este usuário tem R$ 50,00 de Saldo.

````

8.  **Consultar Investimentos e Transações do Usuário**: Para ver os investimentos e transações de um usuário, utilize os endpoints `api/customer/{id}/investment`ou`api/customer/{id}/extract` de consulta com o ID do usuário. Para detalhes específicos, adicione o ID do produto `api/customer/{id}/investment/{productId}`ou`api/customer/{id}/extract/{productId}`.

9. **Disparo de e-mail**: Disparo de e-mail é um HostedService que realiza o envio diário, avisando sobre os produtos próximos da data de vencimento. 

````bash
# Configuração para o disparo de e-mail.

"EmailSettings": {
"SmtpServer": "{servidor smtp}",
"SmtpPort": "{servidor port}",
"Email": "{email credential}",
"Password": "{password credential}",
"To": "{email}"
}

````


  

### Tecnologias<a id="tecnologias"></a>

As seguintes ferramentas foram usadas na construção do projeto:

- [.NET ](https://learn.microsoft.com/pt-br/dotnet/) 

- [Entity Framework Core](https://learn.microsoft.com/pt-br/ef/core/)

- [JMeter](https://jmeter.apache.org/)

- [Docker](https://www.docker.com/)

### Teste de Carga<a id="carga"></a>

Testes de carga nos endpoints de consulta de produto e extrato, utilizando a ferramenta Apache JMeter.

![image](https://github.com/LeonardoMendes007/investment-portfolio/assets/57539940/f946c39f-43e6-4e00-b782-5db1a6dab864)

  ![flotResponseTimesPercentiles](https://github.com/LeonardoMendes007/investment-portfolio/assets/57539940/318e4b18-d453-4e2b-b053-37a20619bfa4)


### Autor <a id="autor">  </a>

  

---


<br/>

<span> Feito por Leonardo Mendes 👋 Entre em contato! </span>

</a>

  

[![Linkedin Badge](https://img.shields.io/badge/Leonardo%20Mendes%20-blue?Style=flat&logo=linkedin&labelColor=blue=https://www.linkedin.com/in/matheus-souza-4a4b19189/)](https://www.linkedin.com/in/leonardo-mendes-gomes/)

  

### :page_facing_up: **Licença**

  

Copyright © 2024 [Leonardo Mendes](https://www.linkedin.com/in/leonardo-mendes-gomes/).<br  />
