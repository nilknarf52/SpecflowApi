using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BDD.Models
{
    public class CustomerModel
    {
        public String NomeCompleto { get; set; }
        public String Endereco { get; set; }
        public DateTime DataNascimento { get; set; }

        public String CPF { get; set; }
        public String Email { get; set; }
    }
}