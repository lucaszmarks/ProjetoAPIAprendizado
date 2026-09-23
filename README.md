====================================================================
API DE GERENCIAMENTO - LABORATÓRIO DE EVOLUÇÃO .NET
====================================================================

Projeto desenvolvido como o meu principal ambiente de prática e evolução no ecossistema .NET. Mais do que um simples projeto, esta é uma zona de aprendizado contínuo. 

O que começou como uma API básica de CRUD está em constante expansão, incorporando gradualmente padrões arquiteturais corporativos, segurança avançada e boas práticas exigidas pelo mercado.


--------------------------------------------------------------------
TECNOLOGIAS E PADRÕES UTILIZADOS
--------------------------------------------------------------------
* C# e .NET 8 (ASP.NET Core Web API)
* Entity Framework Core (ORM) com SQLite
* Segurança: ASP.NET Core Identity & JWT (JSON Web Tokens) Bearer Authentication
* Arquitetura & Design Patterns:
  - Repository Pattern
  - DTOs (Data Transfer Objects) para evitar ataques de Over-posting
  - Separação de Responsabilidades (Contextos de banco de dados separados)
* xUnit (Testes Unitários)
* Swagger/OpenAPI (Documentação e testes interativos com suporte a JWT)


--------------------------------------------------------------------
FUNCIONALIDADES IMPLEMENTADAS (EVOLUÇÃO)
--------------------------------------------------------------------
[+] CRUD Completo: 
    Criação, Leitura, Atualização e Exclusão de Clientes.

[+] Autenticação e Autorização: 
    Sistema robusto de Registro e Login gerando tokens JWT temporários. Proteção de endpoints baseada em perfis de usuário (Roles).

[+] Upload de Arquivos Estáticos: 
    Endpoint preparado para receber arquivos físicos (multipart/form-data), armazená-los localmente no servidor e salvar apenas os metadados (URL, tamanho, extensão) no banco de dados.

[+] Múltiplos Bancos de Dados: 
    Isolamento total de segurança com o uso de dois bancos de dados independentes (meubanco.db para domínio de negócios e meubanco_auth.db exclusivo para credenciais do Identity).


--------------------------------------------------------------------
COMO RODAR O PROJETO
--------------------------------------------------------------------
PRÉ-REQUISITOS:
- .NET SDK 8.0 instalado.
- Visual Studio 2022 ou VS Code.

PASSO A PASSO:
1. Clone o repositório:
   git clone https://github.com/SEU-USUARIO/SEU-REPOSITORIO.git

2. Entre na pasta do projeto:
   cd ProjetoAPIAprendizado

3. Restaure os pacotes:
   dotnet restore

4. Gere os Bancos de Dados (Migrations):
   Como o projeto utiliza separação de contextos de dados, você precisa atualizar ambos os bancos. No Console do Gerenciador de Pacotes (Visual Studio), rode:
   Update-Database -Context AuthDbContext
   Update-Database -Context ApiDbContext

5. Execute a API:
   dotnet run --project ProjetoAPIAprendizado

6. Acesse o Swagger:
   Abra seu navegador em https://localhost:7276/swagger (ou a porta informada no seu terminal). Use o botão Authorize no topo da página para inserir seu token JWT (no formato "Bearer SEU_TOKEN") após fazer o Login.


--------------------------------------------------------------------
COMO RODAR OS TESTES
--------------------------------------------------------------------
Para verificar se tudo está funcionando como esperado, execute os testes automatizados com o xUnit rodando o comando abaixo:

   dotnet test


====================================================================
Desenvolvido com dedicação por Lucas Marques de Oliveira
LinkedIn: www.linkedin.com/in/lucas-marques-dev
====================================================================

