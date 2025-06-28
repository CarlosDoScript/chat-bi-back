# 🤖 chat-bi — Chat com Acesso Inteligente à Base de Dados

`chat-bi` é uma API modular e escalável desenvolvida com **.NET 9**, que permite consultar informações diretamente da base de dados usando **linguagem natural** com apoio de **LLMs (Large Language Models)**.  

Projetado com foco em **Clean Architecture**, **DDD**, **observabilidade completa**, e suporte a **microsserviços**, o sistema está preparado para ambientes de produção com alto nível técnico e modularização.

---

## 🧠 Objetivo

Permitir que **usuários façam perguntas em linguagem natural** (ex: "Qual o total de vendas da última semana?") e o sistema **responda com dados reais diretamente do banco**, aplicando **RAG (Retrieval-Augmented Generation)** com uma LLM local via **Ollama**.

---

## 🛠️ Tecnologias e Ferramentas Utilizadas

| Categoria              | Ferramenta/Stack                             |
|------------------------|----------------------------------------------|
| Linguagem              | .NET 9 (C#)                                  |
| Banco de Dados         | PostgreSQL                                   |
| ORM                    | Entity Framework Core                        |
| LLM (AI)               | Ollama                                       |
| RAG (contexto dinâmico)| Implementado no domínio via C#               |
| Microsserviços         | RabbitMQ                                     |
| Observabilidade        | OpenTelemetry + Grafana + Loki + Tempo       |
| Containerização        | Docker                                       |
| Ambiente de execução   | Cluster local com **Kind (Kubernetes in Docker)** |
| Autenticação           | JWT com claims customizadas (`TiposClaims`)  |

---

## 🧱 Arquitetura do Projeto

- **Clean Architecture**: separação clara entre `Core`, `Application`, `Infrastructure` e `API`
- **DDD**: entidades ricas, protegidas, com regras de negócio e métodos fábrica (`Usuario.Criar(...)`)
- **CQRS com MediatR**: comandos e queries desacoplados
- **Retorno padrão com `Resultado<T>`**: centraliza controle de sucesso/falha e mensagens

---

## ⚙️ Funcionamento

1. O usuário envia uma pergunta via `POST /chat/perguntar`
2. A API publica a requisição no **RabbitMQ**
3. Um microsserviço consome a pergunta, busca os dados necessários no PostgreSQL
4. O contexto é processado com RAG e enviado para a LLM via **Ollama**
5. A resposta é devolvida ao usuário por SSE (Server-Sent Events)

---

## 🔍 Exemplo de pergunta e resposta

**Input:**
```json
{
  "pergunta": "Quantos pedidos foram entregues ontem?"
}
