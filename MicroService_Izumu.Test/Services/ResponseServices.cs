using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Izumu.Test.Services
{
    public class ResponseServices
    {
        public HttpStatusCode HttpCode { get; set; }
        public string Message { get; set; }
    }
}
