using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Model
{
    class InvalidNickNameException: Exception
    {
        public override string Message
        {
            get
            {
                return "Erro: Nick já existente";
            }
        }
    }
}
