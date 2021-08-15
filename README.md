# API - Cadastro Simplificado

Nesse projeto, minha intenção é mostrar uma alternativa _bacana_ para criação de aplicações (desde as mais simples) com uma proposta de arquitetura limpa, orientada a domínio - **DDD**, com NetCore 5 e persistência em um banco de dados **NoSQL** (no caso MongoDb) e consultas baseadas no LINQ e LAMBDA e suas facilidades. 

Trata-se de aplicação _bem_ simples, apenas para cadastro de restaurantes e suas avaliações. Nada além  disso! 😅 Contudo, você verá o uso de boas práticas de desenvolvimento e a utilização de alguns padrões de projeto. 

## Para baixar:

> Clone repository:

`https://github.com/fabioborges-ti/webapi.netcore-mongodb-ddd`

## launchSettings.json

Você já deve ter instalado previamente o **MongoDb** em sua máquina. Feito isso, altere as variáveis de conexão, citadas abaixo:

```bash
1. "DefaultConnection": "<connectionString>"
2. "DataBaseName": "<database>"
```

## Documentação da API

Para acessar a documentação da API e seus recursos, acesse: 

```bash
https://localhost:5001/swagger/index.html
```


## 📚 Para mais informações:

Se você não conhece muito sobre este processo e quer mais detalhes, consulte em:

https://mongodb.github.io/mongo-csharp-driver/2.13/

E bom estudos! 🚀
