using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD_API.Models
{
    public class PessoaFisicaAcoes : IPessoaFisicaAcoes
    {
        private List<PessoaFisica> PessoaFisicas = new List<PessoaFisica>();
        private int _nextId = 1;

        public PessoaFisicaAcoes()
        {
            Add(new PessoaFisica { Id = 1, Nome = "Mauro", DataNascimento = Convert.ToDateTime("28/06/1962"), ValorRenda = 16400.99, CPF = "000.000.123-62" });;
            Add(new PessoaFisica { Id = 2, Nome = "Vinicius", DataNascimento = Convert.ToDateTime("25/04/1994"), ValorRenda = 8600.16, CPF = "000.000.123-94" });
            Add(new PessoaFisica { Id = 3, Nome = "Bruno", DataNascimento = Convert.ToDateTime("16/05/1996"), ValorRenda = 10550.16, CPF = "000.000.456-96" });
            Add(new PessoaFisica { Id = 4, Nome = "Larissa", DataNascimento = Convert.ToDateTime("26/07/1998"), ValorRenda = 12405.25, CPF = "000.000.789-98" });
        }

        public bool Add(PessoaFisica item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            item.Id = _nextId++;
            PessoaFisicas.Add(item);

            return true;
        }

        public PessoaFisica Get(int id)
        {
            return PessoaFisicas.Find(p => p.Id == id);
        }

        public IEnumerable<PessoaFisica> GetAll()
        {
            return PessoaFisicas;
        }

        public void Remove(int id)
        {
            PessoaFisicas.RemoveAll(p => p.Id == id);
        }

        public bool Update(PessoaFisica item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }

            int index = PessoaFisicas.FindIndex(p => p.Id == item.Id);

            if (index == -1)
            {
                return false;
            }
            PessoaFisicas.RemoveAt(index);
            PessoaFisicas.Add(item);
            return true;
        }
    }
}