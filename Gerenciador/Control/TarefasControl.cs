using Gerenciador.Interfaces;
using Gerenciador.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador.Control
{
    public class TarefasControl : TarefaControlInterface<Tarefa>
    {

        private List<Tarefa> _tarefas = new List<Tarefa>();
        public void Add(Tarefa item)
        {
            try
            {
                item.Id = _tarefas.Count + 1;
                _tarefas.Add(item);
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao inserir tarefa. " + e.Message);
                throw;
            }
        }

        public void Update(Tarefa item)
        {
            if (TarefaExiste(item.Id))
            {
                try
                {
                    var index = _tarefas.FindIndex(tarefa => tarefa.Id == item.Id);
                    _tarefas[index] = item;
                    Console.WriteLine("Tarefa atualizada!");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro ao alterar status.");
                }
            }
        }

        public void Delete(int id)
        {
            if (TarefaExiste(id))
            {
                try
                {
                    var index = _tarefas.FindIndex(tarefa => tarefa.Id == id);
                    _tarefas.RemoveAt(index);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Erro ao excluir tarefa.");
                }
            }
        }

        public Tarefa Get(int id)
        {
            try
            {
                var tarefa = _tarefas.Find(item => item.Id == id);
                return tarefa;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao localizar tarefa informada.");
                Console.WriteLine("Pressione qualquer tecla para continuar.");
                Console.ReadKey();
            }
            return new Tarefa();
        }

        public List<Tarefa> GetAll()
        {
            try
            {
                return _tarefas;
            }
            catch (Exception e)
            {
                Console.WriteLine("Erro ao retornar tarefas. " + e.Message);
                Console.WriteLine();
                Console.WriteLine("Pressione qualquer tecla para continuar.");
                Console.ReadKey();
            }
            return new List<Tarefa>();
        }

        public bool TarefaExiste(int id)
        {
            if (Get(id).Id == 0)
            {
                Console.WriteLine("Tarefa não localizada. Insira um Id válido.");
                return false;
            }
            return true;
        }
    }
}
