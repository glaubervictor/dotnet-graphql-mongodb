# dotnet-graphql-mongodb

Projeto desenvolvido utilizando .NET 6, utilizando as bibliotecas graphql-dotnet e mongodb e utilizando autenticação de mutations e queries por perfil

## Configuração

Altere o arquivo de configuração appsettings apontando para endereço do mongodb

## Autenticação

Após ter realizada a autenticação através da mutation, para as queries utilizar o token gerado de acordo com o exemplo abaixo em HTTP Headers:
```
{
  "Authorization": "Bearer ..."
}
```

### Cadastrando um novo usuário

```
mutation {
  usuario {
    create(input: {
      email: "seuemail@email.com",
      senha: "123456",
      pessoa: {
        nome: "Seu nome",
        dataNascimento: "1981-10-22",
        cep: "74000000",
        logradouro: "Sua Rua ...",
        numero: "000",
        complemento: "Seu complemento",
        bairro: "Seu bairro",
        cidade: "Sua cidade",
        telefone: "0000000000",
        celular: "0000000000",
        fotoUrl: "http://"
      }
    }) {
      id
      email
      dataCadastro
      pessoa {
        nome
      }
    }
  }
}
```
### Efetuando autenticação

```
mutation {
  autenticacao {
    user(login: { email: "seuemail@email.com", senha: "123456"}) {
      token
      validoAte
    }
  }
}
```

### Efetuando pesquisa paginada

```
{
  usuario {
    paged(index: 0, size: 50){
      usuarios {
        id
        email
      }
      pageCount
      size
      totalCount
    }
  }
}
```