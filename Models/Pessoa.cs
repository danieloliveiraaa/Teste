using System;
using System.ComponentModel.DataAnnotations;

namespace Sistema.Models
{
    public class Pessoa
    {        
        public int Id { get; set;}
        [Required(ErrorMessage ="Digite o nome!")]
        public string NomeCompleto { get; set;}
        [Required]        
        public DateTime DataNascimento { get; set;}
        [Required(ErrorMessage = "Digite a renda!")]
        public string RendaValor { get; set;}
        [Required(ErrorMessage = "Digite o CPF!")]
        public string CPF { get; set;}
    }
}
