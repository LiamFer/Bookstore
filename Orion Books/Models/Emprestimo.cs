using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orion_Books.Models
{
    public class Emprestimo
    {
        [Key]
        public int Id { get; set; }

        public DateTime DataEmprestimo {  get; set; }
        public DateTime? DataEntrega { get; set; }
        public DateTime DataDevolucao { get; set; }

        public bool Status { get; set; }
        public Usuario Usuario { get; set; }
        [ForeignKey("Livro")]
        public int LivroId { get; set; }
        public Livro Livro { get; set; }
    }
}
