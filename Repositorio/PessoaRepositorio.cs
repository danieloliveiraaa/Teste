using Sistema.Data;
using Sistema.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Repositorio
{
    public class PessoaRepositorio : IPessoaRepositorio
    {
        private readonly BDContext _context;

        public PessoaRepositorio(BDContext context)
        {
            _context = context;
        }

        public List<Pessoa> BuscarTodos()
        {
            return _context.Pessoa.ToList();
        }

        public Pessoa Adicionar(Pessoa pessoa)
        {
            _context.Pessoa.Add(pessoa);
            _context.SaveChanges();

            return pessoa;
        }

        public Pessoa BuscarPorId(int id)
        {
            return _context.Pessoa.FirstOrDefault(x => x.Id == id);
        }

        public Pessoa Salvar(Pessoa modelUpdate)
        {
            try
            {
                Pessoa pessoa = BuscarPorId(modelUpdate.Id);

                if (pessoa == null)
                    throw new Exception("Erro ao salvar, pessoa não encontrada.");

                pessoa.NomeCompleto = modelUpdate.NomeCompleto;
                pessoa.DataNascimento = modelUpdate.DataNascimento;
                pessoa.RendaValor = modelUpdate.RendaValor;
                pessoa.CPF = modelUpdate.CPF;

                _context.Pessoa.Update(pessoa);
                _context.SaveChanges();

                return pessoa;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool Apagar(int id)
        {
            try
            {
                Pessoa pessoa = BuscarPorId(id);

                if (pessoa == null)
                    throw new Exception("Erro ao deletar, pessoa não encontrada.");
                else
                {
                    _context.Pessoa.Remove(pessoa);
                    _context.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
