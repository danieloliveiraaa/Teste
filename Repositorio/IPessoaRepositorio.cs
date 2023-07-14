using Sistema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Repositorio
{
    public interface IPessoaRepositorio
    {
        List<Pessoa> BuscarTodos();
        Pessoa Adicionar(Pessoa model);
        Pessoa BuscarPorId(int id);
        Pessoa Salvar(Pessoa modelUpdate);
        bool Apagar(int id);
    }
}
