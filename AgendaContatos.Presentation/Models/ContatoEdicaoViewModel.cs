using System.ComponentModel.DataAnnotations;

namespace AgendaContatos.Presentation.Models
{
    public class ContatoEdicaoViewModel
    {
        public Guid Id { get; set; }

        [MinLength(8, ErrorMessage = "Por favor, informe no mínimo {1} caracteres.")]
        [MaxLength(150, ErrorMessage = "Por favor, informe no máximo {1} caracteres.")]
        [Required(ErrorMessage = "Por favor, informe o nome do contato.")]
        public string? Nome { get; set; }

        [Required(ErrorMessage = "Por favor, informe o email do contato.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Por favor, informe a data de nascimento do contato.")]
        public DateTime? DataNascimento { get; set; }

        [Required(ErrorMessage = "Por favor, informe o tipo de contato.")]
        public int? Tipo { get; set; }

        [Required(ErrorMessage = "Por favor, informe o telefone do contato.")]
        public string? Telefone { get; set; }
    }
}
