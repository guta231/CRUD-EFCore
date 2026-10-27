# CRUD-EFCore - Help Desk

Sistema de Help Desk desenvolvido em C# com Entity Framework Core, MySQL e operações CRUD para controle de Clientes, Técnicos, Chamados e Equipamentos.

## Tecnologias utilizadas

- [.NET 10.0](https://dotnet.microsoft.com/)
- [Entity Framework Core 9.0](https://learn.microsoft.com/ef/core/)
- [Pomelo.EntityFrameworkCore.MySql](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql)
- [MySQL](https://www.mysql.com/)

## Funcionalidades

### Menu Principal

| Opção | Funcionalidade |
|---|---|
| 1 | Cadastrar cliente |
| 2 | Listar clientes |
| 3 | Cadastrar técnico |
| 4 | Listar técnicos |
| 5 | Abrir chamado |
| 6 | Listar chamados |
| 7 | Atribuir técnico ao chamado |
| 8 | Fechar chamado |
| 9 | Remover chamado |
| 10 | Cadastrar equipamento |
| 11 | Listar equipamentos |
| 12 | Atualizar status do equipamento |
| 13 | Remver equipamento |
| 0 | Sair |

### CRUD de Equipamentos

- **Cadastrar equipamento** - Solicita: Patrimônio, Tipo, Modelo, Status, Localização
- **Listar equipamentos** - Exibe: Id, Patrimônio, Tipo, Modelo, Status, Localização
- **Atualizar status do equipamento** - Solicita o Id e o novo status
- **Remover equipamento** - Solicita o Id para remoção

**Status sugeridos:** `Disponível`, `Em manutenção`, `Emprestado`, `Descartado`

## Pré-requisitos

- [.NET SDK 10.0](https://dotnet.microsoft.com/download) instalado
- [MySQL Server](https://dev.mysql.com/downloads/mysql/) instalado e rodando
- [dotnet-ef](https://learn.microsoft.com/ef/core/cli/dotnet) instalado globalmente

Para instalar o `dotnet-ef` caso não tenha:

```bash
dotnet tool install --global dotnet-ef --version 9.0.0
```

## Configuração segura das credenciais

**NUNCA** commite sua senha real no repositório. Utilize uma das formas abaixo para proteger suas credenciais:

### Opção 1: Arquivo appsettings.json (Recomendado)

1. Crie um arquivo `appsettings.json` na pasta `CRUD-EFCore/` (raiz do projeto console):

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;database=HelpDeskDb;user=root;password=SUA_SENHA_AQUI;"
  }
}
```

2. Adicione o pacote necessário ao `.csproj`:

```xml
<PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="9.0.0" />
```

3. Atualize o `HelpDeskContext.cs` para ler a string de conexão:

```csharp
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CRUD_EFCore
{
    public class HelpDeskContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Tecnico> Tecnicos { get; set; }
        public DbSet<Chamado> Chamados { get; set; }
        public DbSet<Equipamento> Equipamentos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                    .Build();

                string? connectionString = configuration.GetConnectionString("DefaultConnection");

                if (string.IsNullOrEmpty(connectionString))
                {
                    throw new Exception("String de conexão não encontrada. Configure o arquivo appsettings.json.");
                }

                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            }
        }
    }
}
```

4. Adicione `appsettings.json` ao `.gitignore` para garantir que não seja commitado:

```gitignore
appsettings.json
appsettings.*.json
```

### Opção 2: Variáveis de ambiente

Configure a variável de ambiente `HELPDESK_CONNECTION` com sua string de conexão:

**Windows (PowerShell):**

```powershell
$env:HELPDESK_CONNECTION = "server=localhost;database=HelpDeskDb;user=root;password=SUA_SENHA_AQUI;"
```

**Linux/macOS:**

```bash
export HELPDESK_CONNECTION="server=localhost;database=HelpDeskDb;user=root;password=SUA_SENHA_AQUI;"
```

### Opção 3: User Secrets (Desenvolvimento local)

```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "server=localhost;database=HelpDeskDb;user=root;password=SUA_SENHA_AQUI;"
```

## Como executar o projeto

### 1. Clone o repositório

```bash
git clone https://github.com/SEU_USUARIO/CRUD-EFCore.git
cd CRUD-EFCore
```

### 2. Configure as credenciais

Siga uma das opções de configuração segura acima.

### 3. Restaurar e compilar

```bash
cd CRUD-EFCore
dotnet restore
dotnet build
```

### 4. Criar e atualizar o banco de dados

```bash
dotnet ef database update
```

As migrations `AddEquipamento` e `AddLocalizacaoEquipamento` já estão presentes na pasta `Migrations/`.

### 5. Executar a aplicação

```bash
dotnet run
```

## Verificação no MySQL

Após aplicar as migrations, você pode verificar a criação da tabela no MySQL Workbench ou CLI:

```sql
USE HelpDeskDb;
SHOW TABLES;
DESCRIBE Equipamentos;
```

## Estrutura do projeto

```
CRUD-EFCore/
├── CRUD-EFCore/
│   ├── Migrations/           # Migrations do Entity Framework Core
│   ├── Cliente.cs
│   ├── Tecnico.cs
│   ├── Chamado.cs
│   ├── Equipamento.cs
│   ├── HelpDeskContext.cs
│   ├── Program.cs
│   └── CRUD-EFCore.csproj
└── README.md
```

## Entregável

O projeto contém:

- `Equipamento.cs`
- `HelpDeskContext.cs` atualizado com `DbSet<Equipamento>`
- `Program.cs` com menu atualizado e CRUD de equipamentos
- Pasta `Migrations/` com as migrations `AddEquipamento` e `AddLocalizacaoEquipamento`
- CRUD de equipamentos funcionando
