using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class DataWrapper<T>
    {
        public string DataType { get { return typeof(T).Name; } }
        public T Data { get; set; }
    }
}
