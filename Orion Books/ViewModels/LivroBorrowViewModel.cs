using Orion_Books.Models;

namespace Orion_Books.ViewModels
{
    public class LivroBorrowViewModel
    {
        public Livro Livro { get; set; }
        public Emprestimo emprestimo { get; set; }
    }
}
