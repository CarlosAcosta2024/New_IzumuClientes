using AutoMapper;
using Microservice_Izumu.Models;
using MicroService_Izumu.Test.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MicroService_Izumu.Test.Services
{
    public class ClienteServices
    {
        private readonly DbContext _db;
        private readonly IMapper _mapper;

        public ClienteServices(DbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<ResponseServices> UpdateCliente(ClienteRequest clienteRequest)
        {
            try
            {
                var cliente = _mapper.Map<Cliente>(clienteRequest);
                _db.Update(cliente);
                await _db.SaveChangesAsync();

                return new ResponseServices { HttpCode = HttpStatusCode.OK };
            }
            catch (Exception ex)
            {
                return new ResponseServices { HttpCode = HttpStatusCode.InternalServerError, Message = ex.Message };
            }
        }
    }
}
