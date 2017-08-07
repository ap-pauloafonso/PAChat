using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMessages
{
    public class SerializeWrapper<T>
    {
        public string DataType { get { return typeof(T).AssemblyQualifiedName; } }
        public T Data { get; set; }
    }
}
