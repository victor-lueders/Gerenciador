using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gerenciador.Interfaces
{
    public interface TarefaControlInterface<T>
    {
        public void Add(T item);
        public void Delete(int id);
        public T Get(int id);
        public List<T> GetAll();
        public void Update(T item);
    }
}
