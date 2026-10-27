using CRUD_EFCore;
using Microsoft.EntityFrameworkCore;

class Program
{
    static void Main(string[] args)
    {
        using (var context = new HelpDeskContext())
        {
            context.Database.EnsureCreated();
        }

        bool continuar = true;

        while (continuar)
        {
            Console.WriteLine("=== SISTEMA HELP DESK ===");
            Console.WriteLine("1 - Cadastrar cliente");
            Console.WriteLine("2 - Listar clientes");
            Console.WriteLine("3 - Cadastrar técnico");
            Console.WriteLine("4 - Listar técnicos");
            Console.WriteLine("5 - Abrir chamado");
            Console.WriteLine("6 - Listar chamados");
            Console.WriteLine("7 - Atribuir técnico ao chamado");
            Console.WriteLine("8 - Fechar chamado");
            Console.WriteLine("9 - Remover chamado");
            Console.WriteLine("10 - Cadastrar equipamento");
            Console.WriteLine("11 - Listar equipamentos");
            Console.WriteLine("12 - Atualizar status do equipamento");
            Console.WriteLine("13 - Remover equipamento");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha uma opção: ");

            string opcao = Console.ReadLine() ?? string.Empty;

            switch (opcao)
            {
                case "1":
                    CadastrarCliente();
                    break;
                case "2":
                    ListarClientes();
                    break;
                case "3":
                    CadastrarTecnico();
                    break;
                case "4":
                    ListarTecnicos();
                    break;
                case "5":
                    AbrirChamado();
                    break;
                case "6":
                    ListarChamados();
                    break;
                case "7":
                    AtribuirTecnico();
                    break;
                case "8":
                    FecharChamado();
                    break;
                case "9":
                    RemoverChamado();
                    break;
                case "10":
                    CadastrarEquipamento();
                    break;
                case "11":
                    ListarEquipamentos();
                    break;
                case "12":
                    AtualizarStatusEquipamento();
                    break;
                case "13":
                    RemoverEquipamento();
                    break;
                case "0":
                    continuar = false;
                    Console.WriteLine("Encerrando o sistema...");
                    break;
                default:
                    Console.WriteLine("Opção inválida!");
                    break;
            }

            Console.WriteLine("\nPressione Enter para continuar...");
            Console.ReadLine();
            Console.Clear();
        }
    }

    static void CadastrarCliente()
    {
        using (var context = new HelpDeskContext())
        {
            Console.Write("Nome: ");
            string nome = Console.ReadLine() ?? string.Empty;
            Console.Write("CPF: ");
            string cpf = Console.ReadLine() ?? string.Empty;
            Console.Write("Telefone: ");
            string telefone = Console.ReadLine() ?? string.Empty;
            Console.Write("Email: ");
            string email = Console.ReadLine() ?? string.Empty;

            var cliente = new Cliente
            {
                Nome = nome,
                CPF = cpf,
                Telefone = telefone,
                Email = email
            };

            context.Clientes.Add(cliente);
            context.SaveChanges();
            Console.WriteLine("Cliente cadastrado com sucesso!");
        }
    }

    static void ListarClientes()
    {
        using (var context = new HelpDeskContext())
        {
            var clientes = context.Clientes.ToList();
            Console.WriteLine("\n=== LISTA DE CLIENTES ===");
            foreach (var cliente in clientes)
            {
                Console.WriteLine($"Id: {cliente.Id} | Nome: {cliente.Nome} | CPF: {cliente.CPF} | Telefone: {cliente.Telefone} | Email: {cliente.Email}");
            }
            if (!clientes.Any())
                Console.WriteLine("Nenhum cliente cadastrado.");
        }
    }

    static void CadastrarTecnico()
    {
        using (var context = new HelpDeskContext())
        {
            Console.Write("Nome: ");
            string nome = Console.ReadLine() ?? string.Empty;
            Console.Write("Especialidade: ");
            string especialidade = Console.ReadLine() ?? string.Empty;
            Console.Write("Telefone: ");
            string telefone = Console.ReadLine() ?? string.Empty;

            var tecnico = new Tecnico
            {
                Nome = nome,
                Especialidade = especialidade,
                Telefone = telefone
            };

            context.Tecnicos.Add(tecnico);
            context.SaveChanges();
            Console.WriteLine("Técnico cadastrado com sucesso!");
        }
    }

    static void ListarTecnicos()
    {
        using (var context = new HelpDeskContext())
        {
            var tecnicos = context.Tecnicos.ToList();
            Console.WriteLine("\n=== LISTA DE TÉCNICOS ===");
            foreach (var tecnico in tecnicos)
            {
                Console.WriteLine($"Id: {tecnico.Id} | Nome: {tecnico.Nome} | Especialidade: {tecnico.Especialidade} | Telefone: {tecnico.Telefone}");
            }
            if (!tecnicos.Any())
                Console.WriteLine("Nenhum técnico cadastrado.");
        }
    }

    static void AbrirChamado()
    {
        using (var context = new HelpDeskContext())
        {
            Console.Write("Id do Cliente: ");
            int clienteId = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Id do Equipamento (opcional - digite 0 se não houver): ");
            string equipamentoInput = Console.ReadLine() ?? "0";
            int? equipamentoId = null;
            if (int.TryParse(equipamentoInput, out int eqId) && eqId > 0)
                equipamentoId = eqId;

            Console.Write("Descrição do problema: ");
            string descricao = Console.ReadLine() ?? string.Empty;

            var chamado = new Chamado
            {
                ClienteId = clienteId,
                EquipamentoId = equipamentoId,
                Descricao = descricao,
                Status = "Aberto",
                DataAbertura = DateTime.Now
            };

            context.Chamados.Add(chamado);
            context.SaveChanges();
            Console.WriteLine("Chamado aberto com sucesso!");
        }
    }

    static void ListarChamados()
    {
        using (var context = new HelpDeskContext())
        {
            var chamados = context.Chamados
                .Include(c => c.Cliente)
                .Include(c => c.Tecnico)
                .Include(c => c.Equipamento)
                .ToList();

            Console.WriteLine("\n=== LISTA DE CHAMADOS ===");
            foreach (var chamado in chamados)
            {
                Console.WriteLine($"Id: {chamado.Id} | Status: {chamado.Status} | Cliente: {chamado.Cliente?.Nome} | Técnico: {chamado.Tecnico?.Nome ?? "Não atribuído"} | Equipamento: {chamado.Equipamento?.Patrimonio ?? "N/A"} | Descrição: {chamado.Descricao}");
            }
            if (!chamados.Any())
                Console.WriteLine("Nenhum chamado cadastrado.");
        }
    }

    static void AtribuirTecnico()
    {
        using (var context = new HelpDeskContext())
        {
            Console.Write("Id do Chamado: ");
            int chamadoId = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Id do Técnico: ");
            int tecnicoId = int.Parse(Console.ReadLine() ?? "0");

            var chamado = context.Chamados.FirstOrDefault(c => c.Id == chamadoId);
            if (chamado == null)
            {
                Console.WriteLine("Chamado não encontrado!");
                return;
            }

            chamado.TecnicoId = tecnicoId;
            chamado.Status = "Em atendimento";
            context.SaveChanges();
            Console.WriteLine("Técnico atribuído com sucesso!");
        }
    }

    static void FecharChamado()
    {
        using (var context = new HelpDeskContext())
        {
            Console.Write("Id do Chamado: ");
            int chamadoId = int.Parse(Console.ReadLine() ?? "0");

            var chamado = context.Chamados.FirstOrDefault(c => c.Id == chamadoId);
            if (chamado == null)
            {
                Console.WriteLine("Chamado não encontrado!");
                return;
            }

            chamado.Status = "Fechado";
            chamado.DataFechamento = DateTime.Now;
            context.SaveChanges();
            Console.WriteLine("Chamado fechado com sucesso!");
        }
    }

    static void RemoverChamado()
    {
        using (var context = new HelpDeskContext())
        {
            Console.Write("Id do Chamado: ");
            int chamadoId = int.Parse(Console.ReadLine() ?? "0");

            var chamado = context.Chamados.FirstOrDefault(c => c.Id == chamadoId);
            if (chamado == null)
            {
                Console.WriteLine("Chamado não encontrado!");
                return;
            }

            context.Chamados.Remove(chamado);
            context.SaveChanges();
            Console.WriteLine("Chamado removido com sucesso!");
        }
    }

    static void CadastrarEquipamento()
    {
        using (var context = new HelpDeskContext())
        {
            Console.Write("Patrimônio: ");
            string patrimonio = Console.ReadLine() ?? string.Empty;
            Console.Write("Tipo: ");
            string tipo = Console.ReadLine() ?? string.Empty;
            Console.Write("Modelo: ");
            string modelo = Console.ReadLine() ?? string.Empty;
            Console.Write("Status (Disponível / Em manutenção / Emprestado / Descartado): ");
            string status = Console.ReadLine() ?? string.Empty;
            Console.Write("Localização: ");
            string localizacao = Console.ReadLine() ?? string.Empty;

            var equipamento = new Equipamento
            {
                Patrimonio = patrimonio,
                Tipo = tipo,
                Modelo = modelo,
                Status = status,
                Localizacao = localizacao
            };

            context.Equipamentos.Add(equipamento);
            context.SaveChanges();
            Console.WriteLine("Equipamento cadastrado com sucesso!");
        }
    }

    static void ListarEquipamentos()
    {
        using (var context = new HelpDeskContext())
        {
            var equipamentos = context.Equipamentos.ToList();
            Console.WriteLine("\n=== LISTA DE EQUIPAMENTOS ===");
            foreach (var equipamento in equipamentos)
            {
                Console.WriteLine($"Id: {equipamento.Id} | Patrimônio: {equipamento.Patrimonio} | Tipo: {equipamento.Tipo} | Modelo: {equipamento.Modelo} | Status: {equipamento.Status} | Localização: {equipamento.Localizacao}");
            }
            if (!equipamentos.Any())
                Console.WriteLine("Nenhum equipamento cadastrado.");
        }
    }

    static void AtualizarStatusEquipamento()
    {
        using (var context = new HelpDeskContext())
        {
            Console.Write("Id do equipamento: ");
            int id = int.Parse(Console.ReadLine() ?? "0");
            Console.Write("Novo status (Disponível / Em manutenção / Emprestado / Descartado): ");
            string novoStatus = Console.ReadLine() ?? string.Empty;

            var equipamento = context.Equipamentos.FirstOrDefault(e => e.Id == id);
            if (equipamento == null)
            {
                Console.WriteLine("Equipamento não encontrado!");
                return;
            }

            equipamento.Status = novoStatus;
            context.SaveChanges();
            Console.WriteLine("Status do equipamento atualizado com sucesso!");
        }
    }

    static void RemoverEquipamento()
    {
        using (var context = new HelpDeskContext())
        {
            Console.Write("Id do equipamento: ");
            int id = int.Parse(Console.ReadLine() ?? "0");

            var equipamento = context.Equipamentos.FirstOrDefault(e => e.Id == id);
            if (equipamento == null)
            {
                Console.WriteLine("Equipamento não encontrado!");
                return;
            }

            context.Equipamentos.Remove(equipamento);
            context.SaveChanges();
            Console.WriteLine("Equipamento removido com sucesso!");
        }
    }
}
