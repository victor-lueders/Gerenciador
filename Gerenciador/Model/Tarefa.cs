using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador.Model
{
    public class Tarefa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Status { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataVcto { get; set; }

        public Tarefa()
        {
            Id = 0;
            Status = 1;
        }

        public Tarefa(int id, string nome, string descricao, DateTime dataVcto)
        {
            Id = id;
            Nome = nome;
            Descricao = descricao;
            DataVcto = dataVcto;
            Status = 1;
        }
    }
}
