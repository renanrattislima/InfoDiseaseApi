# InfoDisease API

## 📌 Descrição

Uma API ASP.NET Core para análise epidemiológica com foco em arboviroses (como dengue). A aplicação permite gerar relatórios baseados em dados públicos de fontes como **InfoDengue** e **IBGE**, consultando casos por município, semanas epidemiológicas e indicadores como casos estimados e Rt (número de reprodução).

## 🗂️ Tabela de Conteúdos

1. [Pré-requisitos](#pré-requisitos)  
2. [Configuração do Banco de Dados](#configuração-do-banco-de-dados)  
3. [Aplicando Migrations](#aplicando-migrations)  
4. [Executando a Aplicação](#executando-a-aplicação)  
5. [Fontes de Dados Externas](#fontes-de-dados-externas)

---

## ✅ Pré-requisitos

Antes de começar, você precisa ter instalado:

- [.NET SDK](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) ou [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

---

1. **Criar Banco de Dados**: No seu Sql Server local, crie um novo banco de dados chamado 'InfoDiseaseDB' e, na camada de Infraestrutura, execute: :

dotnet ef migrations add InitialCreate --startup-project "../Presentation"
dotnet ef database update --startup-project "../Presentation"

Isso executará o script de migração e criará as tabelas no seu ambiente local.

2. **String de Conexão**: Atualize a string de conexão no `appsettings.json` ou `appsettings.Development.json` com os detalhes do seu SQL Server.

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Data Source=.;Initial Catalog=InfoDiseaseDB;Integrated Security=True;"
      },
      // ...
    }
    ```


