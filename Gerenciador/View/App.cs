using Gerenciador.Control;
using Gerenciador.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador.View
{
    public class App
    {
        private readonly TarefasControl tarefasControl;

        public App()
        {
            tarefasControl = new TarefasControl();
        }

        public void Limpar()
        {
            Console.Clear();
        }
        public void MenuInicial()
        {
            Limpar();
            Console.WriteLine("=== Menu Inicial ===");
            ListarOpcoes();
        }



        public void Add()
        {
            var tarefa = new Tarefa();
            Limpar();
            Console.WriteLine("=== Nova Tarefa ===");
            Console.WriteLine();
            Console.Write("Insira o nome da tarefa: ");
            tarefa.Nome = Console.ReadLine();
            Console.WriteLine();
            Console.Write("Insira a descrição da tarefa: ");
            tarefa.Descricao = Console.ReadLine();

            tarefa.DataCriacao = DateTime.Now;
            tarefasControl.Add(tarefa);

            Console.WriteLine();
            Console.WriteLine("Tarefa adicionada! Aperte qualquer tecla para continuar.");
            Console.ReadKey();
            MenuInicial();
        }
        public void Delete()
        {
            Limpar();
            ListarTarefas();

            try
            {
                Console.Write("Escolha uma tarefa para excluir (insira o id): ");
                var tarefaId = int.Parse(Console.ReadLine());
                tarefasControl.Delete(tarefaId);
                Console.WriteLine($"Tarefa {tarefaId} excluída com sucesso!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao excluir tarefa. " + e.Message);
                Console.WriteLine();
            }
            Console.WriteLine("Pressione qualquer tecla para continuar.");
            Console.ReadKey();
            MenuInicial();
        }
        public void Edit()
        {
            Limpar();
            ListarTarefas();
            Console.WriteLine();
            try
            {
                Console.Write("Insira o id da tarefa que deseja editar: ");
                var id = int.Parse(Console.ReadLine());

                var tarefa = tarefasControl.Get(id);

                if (tarefa != null && tarefa.Id != 0)
                {
                    Console.WriteLine();
                    Console.Write("Insira o novo nome: ");
                    tarefa.Nome = Console.ReadLine();
                    Console.WriteLine();
                    Console.Write("Insira a nova descrição: ");
                    tarefa.Descricao = Console.ReadLine();

                    tarefasControl.Update(tarefa);

                    Console.WriteLine("Pressione qualquer tecla para continuar.");
                    Console.ReadKey();
                    MenuInicial();
                }
                else
                {
                    throw new Exception("Id inexistente.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao editar item. " + e.Message);
                Console.WriteLine("Pressione qualquer tecla para continuar.");
                Console.ReadKey();
                MenuInicial();
            }

            Console.WriteLine();
        }
        public void AlterStatus()
        {
            Limpar();
            ListarTarefasDetalhadas();
            Console.WriteLine();

            try
            {
                Console.Write("Insira o id da tarefa que deseja editar: ");
                var id = int.Parse(Console.ReadLine());

                var tarefa = tarefasControl.Get(id);

                if (tarefa != null && tarefa.Id != 0)
                {
                    var novoStatus = tarefa.Status == 1 ? 2 : 1;
                    var dataConclusao = novoStatus == 2 ? DateTime.Now : tarefa.DataVcto;
                    var statusNome = novoStatus == 1 ? "pendente" : "concluída";

                    tarefa.DataVcto = dataConclusao;
                    tarefa.Status = novoStatus;
                    tarefasControl.Update(tarefa);

                    Console.WriteLine();
                    Console.WriteLine($"Você alterou o status da tarefa {id} para {statusNome}.");

                    Console.WriteLine("Pressione qualquer tecla para continuar.");
                    Console.ReadKey();
                    MenuInicial();
                }
                else
                {
                    throw new Exception("Id inexistente.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao alterar status. " + e.Message);
                Console.WriteLine("Pressione qualquer tecla para continuar.");
                Console.ReadKey();
                MenuInicial();
            }
        }



        public void ListarTarefas(int filtro = 3)
        {
            var palavra = filtro == 1 ? "" : (filtro == 2 ? "concluidas " : "pendentes ");
            var tarefas = FiltrarLista(filtro);

            try
            {
                Limpar();
                Console.WriteLine($"=== Tarefas {palavra}===");
                Console.WriteLine();
                if (tarefas.Count > 0)
                {
                    Console.WriteLine("ID - TAREFA");
                    foreach (var item in tarefas)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(item.Id);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($" - {item.Nome}");
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Não há tarefas cadastradas");
                    Console.WriteLine("Pressione qualquer tecla para continuar.");
                    Console.ReadKey();
                    MenuInicial();
                }
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao listar tarefas.");
                Console.WriteLine("Pressione qualquer tecla para continuar.");
                Console.ReadKey();
                MenuInicial();
            }
        }
        public void ListarTarefasDetalhadas()
        {
            var tarefas = ObterTarefas();

            try
            {
                Limpar();
                Console.WriteLine($"=== Tarefas ===");
                Console.WriteLine();
                if (tarefas.Count > 0)
                {
                    Console.WriteLine("ID - TAREFA - STATUS - DESCRICAO - DATA CRIACAO");
                    Console.WriteLine();
                    foreach (var item in tarefas)
                    {
                        var status = item.Status == 1 ? "Pendente" : "Concluida";
                        var dataConclusao = item.Status == 2 ? $" ------> Concluída no dia {item.DataVcto}" : "";
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(item.Id);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine($" - {item.Nome} - {status} - {item.Descricao} - {item.DataCriacao}{dataConclusao}");
                    }
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine("Não há tarefas cadastradas");
                    Console.WriteLine("Pressione qualquer tecla para continuar.");
                    Console.ReadKey();
                    MenuInicial();
                }
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao listar tarefas.");
                Console.WriteLine("Pressione qualquer tecla para continuar.");
                Console.ReadKey();
                MenuInicial();
            }
        }
        public void ListarFiltros()
        {
            Limpar();
            Console.WriteLine("Quais tarefas você deseja visualizar?");
            Console.WriteLine("1 - Todas");
            Console.WriteLine("2 - Concluídas");
            Console.WriteLine("3 - Pendentes");
        }
        public void ListarOpcoes()
        {
            Console.WriteLine();
            Console.WriteLine("1 - Listar tarefas");
            Console.WriteLine("2 - Adicionar tarefa");
            Console.WriteLine("3 - Excluir tarefa");
            Console.WriteLine("4 - Editar tarefa");
            Console.WriteLine("5 - Alterar status");
            Console.WriteLine("6 - Lista detalhada");
            Console.WriteLine();
            Console.Write("Digite a opção (número) da ação que deseja realizar: ");
            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    Limpar();
                    try
                    {
                        Console.WriteLine("Quais tarefas você deseja exibir?");
                        Console.WriteLine("1 - Todas");
                        Console.WriteLine("2 - Concluídas");
                        Console.WriteLine("3 - Pendentes");
                        var escolha = int.Parse(Console.ReadLine());
                        ListarTarefas(escolha);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Opção inválida.");
                    }
                    Console.WriteLine("Pressione qualquer tecla para continuar.");
                    Console.ReadKey();
                    MenuInicial();
                    break;
                case "2":
                    Add();
                    break;
                case "3":
                    Delete();
                    break;
                case "4":
                    Edit();
                    break;
                case "5":
                    AlterStatus();
                    break;
                case "6":
                    ListarTarefasDetalhadas();
                    Console.WriteLine("Pressione qualquer tecla para continuar.");
                    Console.ReadKey();
                    MenuInicial();
                    break;
                default:
                    Console.WriteLine("Opção inválida");
                    Console.WriteLine("Pressione qualquer tecla para continuar.");
                    Console.ReadKey();
                    MenuInicial();
                    break;

            }
        }



        private List<Tarefa> FiltrarLista(int filtro)
        {
            //filtro
            //1 - todas
            //2 - concluidas
            //3 - pendentes

            var tarefas = ObterTarefas();

            try
            {
                switch (filtro)
                {
                    case 2:
                        tarefas = tarefas.Where(tarefa => tarefa.Status == 2).ToList();
                        break;
                    case 3:
                        tarefas = tarefas.Where(tarefa => tarefa.Status == 1).ToList();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception e)
            {
                Limpar();
                Console.WriteLine("Falha ao executar filtro.");
                Console.WriteLine("Pressione qualquer tecla para continuar.");
                Console.ReadKey();
                MenuInicial();
            }
            return tarefas;
        }
        private List<Tarefa> ObterTarefas()
        {
            try
            {
                var tarefas = tarefasControl.GetAll();
                return tarefas;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao obter tarefas. " + e.Message);
                Console.WriteLine();
                Console.WriteLine("Pressione qualquer tecla para continuar.");
                Console.ReadKey();
                MenuInicial();
            }

            return new List<Tarefa>();
        }
    }
}
