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

1. **Create Database**: On your local Sql Server create a new database called 'InfoDiseaseDB' and on the Infrastructure layer execute :

dotnet ef migrations add InitialCreate --startup-project "../Presentation"
dotnet ef database update --startup-project "../Presentation"

this will execute the migration script and create the tables on your local environment

2. **Connection String**: Update the connection string in `appsettings.json` or `appsettings.Development.json` with your SQL Server details.

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Data Source=.;Initial Catalog=InfoDiseaseDB;Integrated Security=True;"
      },
      // ...
    }
    ```


