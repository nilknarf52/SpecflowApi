using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication.Models;

namespace WebApplication.Controllers.API
{
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        [HttpGet]
        [Route("")]
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var lista = new List<UserModel>()
            {
                new UserModel()
                {
                    Email = "teste@teste.com.br",
                    Id = 1,
                    Nome = "Teste",
                    Password = "123456A#"
                },
                 new UserModel()
                {
                    Email = "teste2@teste.com.br",
                    Id = 2,
                    Nome = "Teste2",
                    Password = "123456A#"
                }
            };

            return Ok(lista);
        }


        [Route("{id}")]
        [HttpGet]
        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            return Ok(new UserModel()
            {
                Email = "teste@teste.com.br",
                Id = 1,
                Nome = "Teste",
                Password = "123456A#"
            });
        }

       
    }
}