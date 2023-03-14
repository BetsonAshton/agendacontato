using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaContatos.Data.Entities
{
    public class Usuario
    {
        private Guid _idUsuario;
        private string? _nome;
        private string? _email;
        private string? _senha;
        private DateTime _dataCriacao;
        private List<Contato>? _contatos;

        public Guid IdUsuario { get => _idUsuario; set => _idUsuario = value; }
        public string? Nome { get => _nome; set => _nome = value; }
        public string? Email { get => _email; set => _email = value; }
        public string? Senha { get => _senha; set => _senha = value; }
        public DateTime DataCriacao { get => _dataCriacao; set => _dataCriacao = value; }
        public List<Contato>? Contatos { get => _contatos; set => _contatos = value; }

        public Usuario()
        {
            _dataCriacao = DateTime.Now;
        }

    }
}
