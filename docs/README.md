# Documentação PocketPay

## Índice

- [Escopo](#escopo)
- [Funcionalidades](#funcionalidades)
- [Links Úteis](#links-úteis)
- [Estrutura do Projeto](#estrutura-do-projeto)
  - [Criação de Usuários](#criação-de-usuários)
  - [Autenticação e Autorização](#autenticação-e-autorização)
  - [Transações](#transações)
  - [Depósitos](#depósitos)
  - [Saques](#saques)
  - [Transferências](#transferências)

## Escopo

Uma WebAPI onde pessoas físicas e jurídicas possam sacar, depositar e fazer transferências entre si.

## Funcionalidades

Os usuários podem:

1. Se inscrever no PocketPay;
2. Logar no aplicativo;
3. Sacar dinheiro da sua carteira;
4. Depositar dinheiro na sua carteira;
5. Transferir dinheiro para outros usuários;
6. Receber dinheiro de outros usuários;
7. Consultar todas as suas transferências, saques e depósitos;

## Links Úteis

- [Collection Postman](./Postman)
- [Diagrama de Classes](./drawio)

## Estrutura do Projeto

O Pocketpay usa o ASP.NET MVC e o Entity Framework em sua construção. O projeto é divido em diversas classes, sendo elas:

- [Controllers](../backend/pocketpay/Controllers/): recebem as requisições do usuário e enviam respostas;
- [DTO's](../backend/pocketpay/DTOs/): representam o corpo das requisições e respostas;
- [Models](../backend/pocketpay/Models/): representam as tabelas do banco de dados;
- [Repositories](../backend/pocketpay/Repositories/): possuem métodos para interação com o banco de dados (FindBy, Create, Update, etc.);
- [Interfaces](../backend/pocketpay/Interfaces/): usadas para definir os métodos das repositories;
- [Enums](../backend/pocketpay/Enums/): conjunto de opções para algum atributo (eg. tipo de usuário);
- [Services](../backend/pocketpay/Services/): serviços da aplicação (autorização JWT e _logging_);
  
### Criação de Usuários

#### Endpoints

```
/api/v1/client/register
/api/v1/seller/register
```

#### Arquivos Relacionados

- [ClientController.cs](../backend/pocketpay/Controllers/ClientController.cs)
- [SellerController.cs](../backend/pocketpay/Controllers/ClientController.cs)


No PocketPay, todo usuário deve ter uma conta ([Account](./backend/pocketpay/Models/AccountModel.cs)).
Essa conta contém apenas **email**, **senha** e o **tipo de usuário** (Seller ou Client, definido em AccountRole).

Cada conta possui um **perfil**, podendo ser Vendedor ([Seller](./backend/pocketpay/Models/SellerModel.cs)) ou Cliente ([Client](./backend/pocketpay/Models/ClientModel.cs)). Esse perfil contém as informações pessoais do usuário, como nome, sobrenome, cpf, etc.

Além do **perfil**, todo usuário também possui uma carteira ([Wallet](./backend/pocketpay/Model0/WalletModel.cs)), que contém o saldo da pessoa dentro do app.

Todos esses atributos são criados no momento de inscrição do usuário na plataforma.

Depois de criado o usuário, ele deverá se autenticar para conseguir um token de acesso.

### Autenticação e Autorização

#### Endpoints

```
/api/v1/client/login
/api/v1/seller/login
```

#### Arquivos Relacionados

- [ClientController.cs](../backend/pocketpay/Controllers/ClientController.cs)
- [SellerController.cs](../backend/pocketpay/Controllers/ClientController.cs)
- [AuthenticationService.cs](../backend/pocketpay/Services/AuthenticationService.cs)


A autenticação é feita através da tecnologia [JWT (JSON Web Token)](https://jwt.io/). 

De forma resumida, o usuário entra com as suas credenciais em um dos _endpoints_ de login e recebe um `access_token`, 
com o qual poderá acessar as informações da sua conta, fazer saques, depósitos e transferências.

Esse token possui o email e o tipo do usuário, ambos validados e assinados digitalmente pelo servidor.

Em rotas restritas (que possuem a anotação [`[Authorize]`](https://learn.microsoft.com/en-us/aspnet/core/security/authorization/simple?view=aspnetcore-7.0)), o token deve ser passado no header [`Authorization`](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Authorization), usando o atributo Bearer:

```
Authorization: Bearer JzdWIiOiIxMjM0NTY3 [...]
```

### Transações

#### Endpoints

```
/api/v1/transaction
```

#### Arquivos Relacionados

- [TransactionController.cs](../backend/pocketpay/Controllers/TransactionController.cs)

Transação é o tipo mais básico de operação bancária dentro do PocketPay. Ela é usada para construir todas as outras transações (saques, depósitos e transferências).

Toda transação possui um dono (Owner) e uma data de criação (Timestamp).

Obs: **uma transação nunca é criada sozinha.** Sempre é necessário atrelar ela a alguma operação.

### Depósitos 

#### Endpoints

```
/api/v1/deposit
```

#### Arquivos Relacionados

- [DepositController.cs](../backend/pocketpay/Controllers/DepositController.cs)

Para começar a usar dinheiro dentro do PocketPay, o usuário deve depositar dinheiro em sua carteira. No atual estágio de desenvolvimento, o usuário apenas deve passar a quantia especificada no corpo da requisição. 

### Saques

#### Endpoints

```
/api/v1/withdraw
```

#### Arquivos Relacionados

- [WithdrawController.cs](../backend/pocketpay/Controllers/WithdrawController.cs)

Após receber algumas transferências (ou após estar de saco cheio do PocketPay), o usuário poderá sacar o dinheiro disponível na sua carteira. O Pocketpay apenas permitirá tal transação se o usuário tiver saldo suficiente em conta.  

### Transferências

#### Endpoints

```
/api/v1/transferences
```

#### Arquivos Relacionados

- [TransferenceController.cs](../backend/pocketpay/Controllers/TransferenceController.cs)

Aqui reside o charme do PocketPay - as transferências. Dentro da plataforma, os usuários poderão trasnferir dinheiro uns para os outros (independente do tipo da conta!). Da mesma forma que no saque, os usuários somente poderão realizá-la se tiverem dinheiro suficiente em conta.

Para transferir para outro usuário, deve-se informar o endereço de email e o valor desejado.

### Tecnologias Utilizadas

#### Backend

1. [Dotnet](https://dotnet.microsoft.com/pt-br/)
2. [ASP.NET](https://dotnet.microsoft.com/pt-br/apps/aspnet)
3. [Entity Framework](https://learn.microsoft.com/pt-br/ef/)
4. [JWT (JSON Web Token)](https://jwt.io/)
4. [SQLite3](https://www.sqlite.org/index.html)
5. [Bcrypt](https://pt.wikipedia.org/wiki/Bcrypt)

#### Frontend

1. [Angular](https://angular.io/) 
