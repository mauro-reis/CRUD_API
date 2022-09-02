using System;

namespace CRUD_API.Models
{
    public class PessoaFisica
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public double ValorRenda { get; set; }
        public string CPF { get; set; } 
    }
}