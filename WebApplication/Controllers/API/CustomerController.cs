using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [RoutePrefix("api/customer")]
    public class CustomerController : ApiController
    {
        [Route("{Id}")]
        [HttpGet]
        public IHttpActionResult Get([FromUri] int id)
        {
            return Ok(new CustomerModel()
            {
                Id = new Random().Next(),
                CPF = "000.000.000-00",
                Email = "rafaelcruz.net81@gmail.com",
                DataNascimento = new DateTime(1981, 03, 13),
                Endereco = "Rua Teste de API",
                NomeCompleto = "Rafael Cruz",
                DataCadastro = DateTime.Now

            });
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(new[] {
             new CustomerModel()
            {
                Id = 0,
                CPF = "000.000.000-00",
                Email = "rafaelcruz.net81@gmail.com",
                DataNascimento = new DateTime(1981, 03, 13),
                Endereco = "Rua Teste de API",
                NomeCompleto = "Rafael Cruz"
            },
             new CustomerModel()
            {
                Id = 1,
                CPF = "111.111.111-11",
                Email = "rafaelcruz.net81@gmail.com",
                DataNascimento = new DateTime(1981, 03, 13),
                Endereco = "Rua Teste de API",
                NomeCompleto = "Rafael Cruz 2"
            },
             new CustomerModel()
            {
                Id = 2,
                CPF = "222.222.222-22",
                Email = "rafaelcruz.net81@gmail.com",
                DataNascimento = new DateTime(1981, 03, 13),
                Endereco = "Rua Teste de API",
                NomeCompleto = "Rafael Cruz 3"
            }




            });
        }


        [HttpPost]
        [Route("")]
        public IHttpActionResult Post(CustomerModel model)
        {
            model.Id = 10;
            return Created(String.Empty, model);

        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}