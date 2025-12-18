#  API de Gerenciamento de Clientes

Projeto desenvolvido como parte da minha jornada de aprendizado no ecossistema .NET. Trata-se de uma **API RESTful** completa que implementa operações CRUD, persistência de dados e testes automatizados.

## Tecnologias Utilizadas

* **C#** e **.NET 8**
* **ASP.NET Core** (Web API)
* **Entity Framework Core** (ORM)
* **SQLite** (Banco de Dados Relacional)
* **xUnit** (Testes Unitários)
* **Swagger/OpenAPI** (Documentação Interativa)

##  Funcionalidades

* ✅ **CRUD Completo:** Criação, Leitura, Atualização e Exclusão de Clientes.
* 💾 **Persistência de Dados:** Uso do padrão *Code-First* com Entity Framework para criar e gerenciar o banco de dados SQLite automaticamente.
* 🛡️ **Testes Automatizados:** Testes unitários para garantir a integridade da lógica de negócios (Domínio).
* 📚 **Documentação:** Interface Swagger integrada para testar endpoints visualmente.
* 🏗️ **Arquitetura:** Organização em *Controllers*, *Models* e *Contexto* (MVC/Layered).

## Como Rodar o Projeto

### Pré-requisitos
* [.NET SDK 8.0](https://dotnet.microsoft.com/download) instalado.
* Visual Studio 2022 ou VS Code.

### Passo a Passo
1.  **Clone o repositório:**
    ```bash
    git clone [https://github.com/SEU-USUARIO/SEU-REPOSITORIO.git](https://github.com/SEU-USUARIO/SEU-REPOSITORIO.git)
    ```
2.  **Entre na pasta do projeto:**
    ```bash
    cd ProjetoAPIAprendizado
    ```
3.  **Restaure os pacotes:**
    ```bash
    dotnet restore
    ```
4.  **Gere o Banco de Dados (Migrations):**
    O projeto está configurado para criar o arquivo `meubanco.db` automaticamente se ele não existir, mas você pode garantir rodando:
    ```bash
    dotnet ef database update
    ```
5.  **Execute a API:**
    ```bash
    dotnet run --project ProjetoAPIAprendizado
    ```
6.  **Acesse o Swagger:**
    Abra seu navegador em `http://localhost:5XXX/swagger` (a porta aparecerá no terminal) para testar os endpoints.

##  Como Rodar os Testes

Para verificar se tudo está funcionando como esperado, execute os testes automatizados com o xUnit:

```bash
dotnet test
```

---
Desenvolvido por [Lucas Marques de Oliveira](linkedin.com/in/lucas-marques-903234286)