using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_API.Models
{
    public interface IPessoaFisicaAcoes
    {
        IEnumerable<PessoaFisica> GetAll();
        PessoaFisica Get(int id);
        bool Add(PessoaFisica pessoaFisica);
        void Remove(int id);
        bool Update(PessoaFisica item);
    }
}
