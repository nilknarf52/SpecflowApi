using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication.Models
{
    public class CustomerModel
    {

        public int Id { get; set; }
        public String NomeCompleto { get; set; }
        public String Endereco { get; set; }
        public DateTime DataNascimento { get; set; }

        public String CPF { get; set; }
        public String Email { get; set; }

        public DateTime DataCadastro { get; set; }
    }
}