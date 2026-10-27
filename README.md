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
| 13 | Remover equipamento |
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

## Como executar o projeto

### 1. Clone o repositório

```bash
git clone https://github.com/guta231/CRUD-EFCore.git
cd CRUD-EFCore
```

### 2. Configurar a string de conexão

Na pasta `CRUD-EFCore/` copie o arquivo de exemplo e renomeie para `appsettings.json`:

```bash
copy appsettings.json.example appsettings.json
```

Abra o `appsettings.json` e coloque a senha e usuario correto do seu MySQL:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=localhost;database=HelpDeskDb;user=USUARIO;password=SUA_SENHA_AQUI;"
  }
}
```

### 3. Restaurar, compilar e atualizar o banco de dados

```bash
cd CRUD-EFCore
dotnet restore
dotnet build
dotnet ef database update
```

### 4. Executar a aplicação

```bash
dotnet run
```

Ou abra o arquivo `CRUD-EFCore.slnx` no Visual Studio e pressione `F5`.

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
│   ├── appsettings.json.example
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
