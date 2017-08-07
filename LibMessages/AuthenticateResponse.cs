using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibMessages
{

    public class AuthenticateResponse
    {
        public AuthenticateResponse(ResponseEnum Response, List<String> ClientList)
        {
            this.ClientList = ClientList;
            this.Response = Response;
        }
        public ResponseEnum  Response { get; set; }
        public List<string> ClientList { get; set; }
    }
}
