# Guia do desenvolvedor

## ASP.NET Web API

### Criação de Usuários

No PocketPay, todo usuário deve ter uma conta ([Account](./backend/pocketpay/Models/AccountModel.cs)).
Essa conta contém apenas email, senha e o tipo de usuário (Seller ou Client, definido em AccountRole).

Cada conta possui um **perfil**, podendo ser Vendedor ([Seller](./backend/pocketpay/Models/SellerModel.cs)) ou Cliente ([Client](./backend/pocketpay/Models/ClientModel.cs)).

Além do **perfil**, todo usuário também possui uma carteira ([Wallet](./backend/pocketpay/Model0/WalletModel.cs)).
Ela contém o saldo daquele usuário dentro do PocketPay.

Todos esses atributos são criados no momento de inscrição na plataforma (em [ClientController](./backend/pocketpay/Controllers/ClientController.cs) para Clientes, 
em [SellerController](./backend/pocketpay/Controllers/SellerController.cs) para Vendedores).

### Autenticação e Autorização

A autenticação é feita através da tecnologia [JWT (JSON Web Token)](https://jwt.io/). 

De forma resumida, o usuário entra com as suas credenciais em um dos _endpoints_ de login e recebe um `access_token`, 
com o qual poderá acessar as informações da sua conta, fazer saques, depósitos e transferências.

Esse token de acesso possui o email e o tipo do usuário, assinada por criptografia forte (RSA) pelo PocketPay.

Em rotas restritas (que possuem a anotação [`[Authorize]`](https://learn.microsoft.com/en-us/aspnet/core/security/authorization/simple?view=aspnetcore-7.0)), o token deve ser passado no header [`Authorization`](https://developer.mozilla.org/en-US/docs/Web/HTTP/Headers/Authorization), usando o atributo Bearer:

```
Authorization: Bearer JzdWIiOiIxMjM0NTY3 [...]
```

### Transações

Transação é o tipo mais básico de operação bancária dentro do PocketPay. 
Ela é usada para compor todas as outras transações.

### Depósitos e Saques

Para usar dinheiro dentro do PocketPay, o usuário deverá depositar dinheiro em sua conta. Esse tipo de 
transação é chamada de **depósito** (Deposit).

O usuário também pode retirar dinheiro de sua conta, criando uma transação do tipo **saque** (Withdraw).

### Transferências

Os usuários também podem fazer transações para outras contas, usando o email como referência para o destinatário.

