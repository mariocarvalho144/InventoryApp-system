Inventory Management System

Sistema de gerenciamento de estoque desenvolvido em C# com integração ao SQL Server, aplicando conceitos de arquitetura em camadas e boas práticas de desenvolvimento backend.

---

Funcionalidades

-  Cadastro de produtos
-  Busca de produtos por nome
-  Atualização de produtos
-  Remoção de produtos
-  Relatório inteligente de estoque
-  Identificação automática de produtos críticos

---

Regras de Negócio

- Produtos com mesmo **Nome + Marca + Origem** são consolidados (soma de quantidade)
- Atualização automática da data da última compra
- Validação de entradas do usuário
- Tratamento de erros (produto inexistente, entradas inválidas)

---

Arquitetura do Projeto

O sistema foi estruturado seguindo separação de responsabilidades:

- Models → Representação dos dados
- Repository → Acesso ao banco (SQL Server)
- Services → Regras de negócio
- Program → Interface e fluxo da aplicação


---

Tecnologias utilizadas

- C#
- .NET
- SQL Server
- ADO.NET

---

Exemplo de saída

    ===== RELATÓRIO DE ESTOQUE =====

    Mouse Razer Importado 5 CRÍTICO
    Monitor LG Nacional 50 OK

    ================ RESUMO ================

    Total de produtos: 2
    Quantidade total: 55
    Produtos críticos: 1

    Maior estoque: Monitor (50)
    Menor estoque: Mouse (5)


---

Como executar o projeto

1. Clone o repositório:
git clone https://github.com/mariocarvalho144/inventory-management-system.git


2. Configure a string de conexão no arquivo `Program.cs`

3. Execute o projeto: "dotnet run"


---

Aprendizados

Este projeto aborda conceitos importantes como:

- CRUD completo
- Integração com banco de dados
- Separação de responsabilidades
- Validação de dados
- Lógica de negócio aplicada
- Estruturação de backend

---

Próximos passos

- Transformar em API REST com ASP.NET
- Integração com Azure
- Criação de interface Web
- Aplicação de autenticação

---

Sobre o projeto

Projeto desenvolvido com foco em aprendizado prático e construção de portfólio voltado para backend, cloud e DevOps.

---

