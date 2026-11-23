# 🎮 FIAP Cloud Games - Catalog API

## 📋 Sobre

Microsserviço responsável pelo gerenciamento do catálogo de jogos, biblioteca dos usuários e criação de pedidos de compra da plataforma **FIAP Cloud Games**.

A aplicação utiliza persistência poliglota, cache distribuído, mensageria assíncrona e busca avançada, com implantação automatizada na AWS.

---

## 🚀 Responsabilidades

- Cadastro, atualização e consulta de jogos.
- Gerenciamento da biblioteca dos usuários.
- Criação de pedidos de compra.
- Publicação e consumo de eventos de pagamento.
- Armazenamento dos detalhes dos jogos no MongoDB.
- Cache de consultas utilizando Redis.
- Busca de jogos utilizando Amazon OpenSearch.

---

## 🛠️ Tecnologias

- .NET 9 / ASP.NET Core Web API
- Entity Framework Core / SQL Server
- MongoDB
- Redis
- Amazon OpenSearch
- RabbitMQ / MassTransit
- Docker / Kubernetes
- AWS EKS / ECR
- GitHub Actions

---

## 🏗️ Arquitetura

O projeto utiliza **Clean Architecture**, dividido nas seguintes camadas:

- **API:** endpoints e requisições HTTP.
- **Application:** casos de uso e regras de aplicação.
- **Domain:** entidades e regras de negócio.
- **Infrastructure:** persistência e integrações externas.
- **Tests:** testes automatizados.

---

## 🗄️ Persistência Poliglota

A Catalog API utiliza diferentes tecnologias de armazenamento:

| Tecnologia | Responsabilidade |
|---|---|
| SQL Server | Persistência principal dos jogos e biblioteca |
| MongoDB | Armazenamento dos detalhes dos jogos |
| Redis | Cache distribuído de consultas |
| Amazon OpenSearch | Indexação e busca avançada de jogos |

O MongoDB foi integrado na fase 4 para permitir o armazenamento flexível dos detalhes dos jogos, complementando os dados relacionais do SQL Server.

---

## ⚡ Cache Distribuído — Redis

O Redis é utilizado para armazenar temporariamente os resultados das consultas ao catálogo, reduzindo acessos repetitivos ao banco de dados.

A aplicação também implementa **invalidação do cache** quando informações dos jogos são alteradas, evitando o retorno de dados desatualizados.

---

## 🔎 Busca Avançada — Amazon OpenSearch

Na fase 4, foi integrado o **Amazon OpenSearch** para realizar pesquisas de jogos de forma independente das consultas tradicionais ao SQL Server.

A solução utiliza indexação para otimizar as pesquisas e contempla os recursos de busca avançada previstos no desafio, como Fuzzy Search e ordenação por relevância.

---

## 📨 Mensageria — RabbitMQ e MassTransit

A comunicação assíncrona entre os microsserviços utiliza RabbitMQ e MassTransit.

### OrderPlacedEvent

Publicado pela Catalog API quando um usuário inicia uma compra.

Consumidor: **Payments API**.

### PaymentProcessedEvent

Consumido pela Catalog API após o processamento do pagamento.

Quando aprovado, o jogo adquirido é adicionado à biblioteca do usuário.

---

## ☁️ CI/CD — GitHub Actions e AWS

A Catalog API possui pipelines de integração e entrega contínua utilizando **GitHub Actions**, com dois workflows:

### Catalog - CI

Responsável pela validação do código:

1. Checkout do repositório.
2. Configuração do .NET 9.
3. Restauração das dependências.
4. Compilação da aplicação.
5. Execução dos testes unitários.

### Catalog - CD

Responsável pela implantação na AWS:

1. Checkout do commit validado.
2. Autenticação na AWS utilizando **OIDC**.
3. Construção e publicação da imagem Docker no **Amazon ECR**.
4. Atualização da Catalog API no cluster **Amazon EKS**.

Os workflows foram executados com sucesso, automatizando a validação e implantação da aplicação.

---

## 🐳 Executando Localmente

A execução local é centralizada pelo repositório de orquestração:

[fiap-cloud-games-orchestration](https://github.com/MatheusDavidDev/fiap-cloud-games-orchestration)

Para iniciar os serviços:

```bash
docker compose up -d
```

Para executar os testes da solução .NET:

```bash
dotnet test
```

---

## 🔗 Repositórios Relacionados

- [Orchestration](https://github.com/MatheusDavidDev/fiap-cloud-games-orchestration)
- [Users API](https://github.com/MatheusDavidDev/fiap-cloud-games-users)
- [Payments API](https://github.com/MatheusDavidDev/fiap-cloud-games-payments)
- [Notifications API](https://github.com/MatheusDavidDev/fiap-cloud-games-notifications)

---

## 🎓 FIAP Cloud Games

Projeto desenvolvido durante a **Pós-graduação em Arquitetura de Sistemas .NET — FIAP**.

O projeto aplica conceitos de microsserviços, Clean Architecture, mensageria assíncrona, persistência poliglota, cache distribuído, busca avançada e CI/CD em ambiente AWS.
