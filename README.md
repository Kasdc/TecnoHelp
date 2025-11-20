# TecnoHelp - Sistema de Gestão de Chamados Técnicos 🚀

[![CSharp](https://img.shields.io/badge/C%23-11-blueviolet?logo=c-sharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-8.0-blue?logo=dotnet)](https://dotnet.microsoft.com/en-us/apps/aspnet)
[![SQL Server](https://img.shields.io/badge/SQL%20Server-grey?logo=microsoftsqlserver)](https://www.microsoft.com/pt-br/sql-server)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-5-purple?logo=bootstrap)](https://getbootstrap.com/)

## 📝 Descrição

[cite_start]**TecnoHelp** é um sistema web integrado para gestão de chamados e suporte técnico[cite: 54]. [cite_start]Desenvolvido como Projeto Integrado Multidisciplinar (PIM) para o curso de Análise e Desenvolvimento de Sistemas da Universidade Paulista (UNIP) [cite: 1, 13][cite_start], o sistema visa otimizar o fluxo de trabalho de suporte em ambientes corporativos, facilitando o registro, acompanhamento e resolução de solicitações técnicas[cite: 82].

## ✨ Funcionalidades Implementadas

* **Gestão de Usuários:**
    * [cite_start]Cadastro, visualização, edição e exclusão de usuários[cite: 536].
    * [cite_start]Diferenciação por perfis: `colaborador`, `técnico`, `admin`[cite: 641, 685].
* **Gestão de Chamados:**
    * [cite_start]Criação, visualização, edição e exclusão de chamados técnicos[cite: 174, 175].
    * [cite_start]Associação de chamados a solicitantes, categorias, status e prioridades[cite: 648].
    * [cite_start]Atribuição (opcional) de chamados a técnicos específicos[cite: 648].
* **Autenticação e Autorização:**
    * [cite_start]Sistema de Login/Logout seguro baseado em cookies[cite: 205].
    * [cite_start]Proteção de páginas por nível de acesso (ex: apenas `admin` gerencia usuários)[cite: 518].
    * Filtragem de chamados na listagem baseada no perfil do usuário logado (colaborador vê seus chamados, técnico vê os atribuídos/sem atribuição, admin vê todos).
* **Interface:**
    * [cite_start]Design responsivo (adaptável a desktop e mobile)[cite: 204].
    * [cite_start]Tema escuro moderno inspirado nos protótipos do projeto[cite: 221].
    * Interface traduzida para o Português.
* **Dashboard (Admin):**
    * [cite_start]Página de resumo com contagem de chamados por status (Total, Abertos, Em Andamento, Resolvidos)[cite: 176, 182, 361].

## 🛠️ Tecnologias Utilizadas

* [cite_start]**Backend:** C# com ASP.NET Core MVC (.NET 8.0) [cite: 202]
* [cite_start]**Frontend:** HTML5, CSS3, JavaScript, Bootstrap 5 [cite: 204]
* [cite_start]**Banco de Dados:** MS SQL Server [cite: 56, 119, 203] (com opção para In-Memory ou SQLite configurável no `Program.cs`)
* **ORM:** Entity Framework Core 8.0

## 🔧 Pré-requisitos

Para executar este projeto localmente, você precisará ter instalado:

1.  **Git:** Para clonar o repositório ([git-scm.com](https://git-scm.com/)).
2.  **SDK do .NET 8.0 (ou superior):** Kit de desenvolvimento .NET ([dotnet.microsoft.com/download](https://dotnet.microsoft.com/download)).
3.  **Visual Studio 2022 (Community Edition ou superior):** Com as cargas de trabalho "Desenvolvimento ASP.NET e para a Web" e "Armazenamento e processamento de dados" selecionadas.
4.  **MS SQL Server (Express ou Developer Edition):** O sistema de gerenciamento de banco de dados.
5.  **SQL Server Management Studio (SSMS):** Ferramenta para gerenciar o banco de dados.

*(Alternativa para Banco de Dados: Se não puder instalar o SQL Server, o projeto está configurado para usar o Banco de Dados Em Memória por padrão. Para usar SQLite, instale o pacote `Microsoft.EntityFrameworkCore.Sqlite` e ajuste o `Program.cs`)*

## ⚙️ Configuração do Ambiente Local

1.  **Clone o Repositório:**
    ```bash
    git clone [https://github.com/Kasdc/TecnoHelp.git](https://github.com/Kasdc/TecnoHelp.git)
    cd TecnoHelp
    ```

2.  **Restaure as Dependências:**
    ```bash
    dotnet restore
    ```

3.  **Configure o Banco de Dados (SQL Server):**
    * Abra o **SSMS** e conecte-se à sua instância local do SQL Server.
    * Localize o arquivo `database_script.sql` (se você o criou e adicionou ao repo) ou use o script fornecido durante o desenvolvimento para criar o banco de dados `TecnoHelp` e suas tabelas.
    * Execute o script para criar a estrutura e popular os dados iniciais.

4.  **Configure a Connection String:**
    * Abra o arquivo `appsettings.json`.
    * Localize a seção `"ConnectionStrings"`.
    * Altere o valor de `"DefaultConnection"` para apontar para a sua instância local do SQL Server (ex: `"Data Source=(localdb)\\mssqllocaldb;Database=TecnoHelp;Integrated Security=True;Encrypt=False"`).
    * **Importante:** Não envie (commit) suas alterações no `appsettings.json` se estiver compartilhando o projeto publicamente. Considere usar `appsettings.Development.json` para configurações locais.

5.  **(Opcional: Se for usar Banco em Memória)**
    * Certifique-se que no `Program.cs` a linha `options.UseInMemoryDatabase("TecnoHelpDb")` está ativa e a linha `options.UseSqlServer(...)` está comentada. A `ConnectionString` no `appsettings.json` será ignorada.

## ▶️ Executando a Aplicação

1.  Abra a solução (`TecnoHelp.sln`) no **Visual Studio 2022**.
2.  Certifique-se que a configuração está definida como **"Debug"**.
3.  Pressione **F5** ou clique no botão de "play" verde (▶️ HTTPS) para iniciar a aplicação.
4.  O navegador será aberto no endereço local (ex: `https://localhost:7002`).

**Usuários de Teste (se usando Banco em Memória ou script inicial):**
* **Admin:** `admin@email.com` / `senha123`
* **Técnico:** `carlos.dias@email.com` / `senha123`
* **Colaborador:** `ana.clara@email.com` / `senha123`

---

Este README cobre os pontos essenciais. Você pode adicionar mais seções, como "Funcionalidades Futuras", "Estrutura do Projeto" ou "Como Contribuir", se desejar.