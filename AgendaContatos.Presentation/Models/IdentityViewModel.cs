using Microsoft.Extensions.Primitives;

namespace AgendaContatos.Presentation.Models
{
    public class IdentityViewModel
    {
        public Guid IdUsuario { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public DateTime DataHoraAcesso { get; set; }
    }
}
