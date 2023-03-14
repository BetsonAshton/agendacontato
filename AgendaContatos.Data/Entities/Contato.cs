using AgendaContatos.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgendaContatos.Data.Entities
{
    public class Contato
    {
        private Guid _idContato;
        private string? _nome;
        private Guid? _idUsuario;
        private Usuario? _usuario;
        private string? _email;
        private DateTime? _dataNascimento;
        private TipoContato? _tipo;
        private string? _telefone;
        private decimal? _quantidade;

        public Guid IdContato { get => _idContato; set => _idContato = value; }
        public string? Nome { get => _nome; set => _nome = value; }
        public Guid? IdUsuario { get => _idUsuario; set => _idUsuario = value; }
        public Usuario? Usuario { get => _usuario; set => _usuario = value; }
        public string? Email { get => _email; set => _email = value; }
        public DateTime? DataNascimento { get => _dataNascimento; set => _dataNascimento = value; }
        public TipoContato? Tipo { get => _tipo; set => _tipo = value; }
        public string? Telefone { get => _telefone; set => _telefone = value; }
        public decimal? Quantidade { get => _quantidade; set => _quantidade = value; }
    }
}
