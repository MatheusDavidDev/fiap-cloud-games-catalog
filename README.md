# FIAP Cloud Games - Catalog API

## Sobre

Microsserviço responsável pelo gerenciamento do catálogo de jogos e biblioteca dos usuários.

---

## Responsabilidades

- Cadastro e gerenciamento de jogos;
- Consulta do catálogo;
- Gerenciamento da biblioteca do usuário;
- Criação de pedidos de compra.

---

## Tecnologias

- .NET 9
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server
- RabbitMQ
- MassTransit
- Docker

---

## Arquitetura

O projeto utiliza Clean Architecture:

- API
- Application
- Domain
- Infrastructure
- Tests

---

## Banco de Dados

Utiliza SQL Server para persistência dos dados.

---

## Mensageria

Publica:

### OrderPlacedEvent

Evento enviado para a Payments API iniciar o processamento do pagamento.

---

Consome:

### PaymentProcessedEvent

Recebe o resultado do pagamento.

Quando aprovado, o jogo é adicionado à biblioteca do usuário.

---

## Executando

```bash
docker compose up -d
