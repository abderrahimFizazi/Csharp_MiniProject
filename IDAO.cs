using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prepa
{
    internal interface IDAO<T>
    {
        public void Create(T obj);
        public List<T> Select();
        public int Update(T obj);
        public int Delete(T obj);
        public T Find(T obj);
    }
}
