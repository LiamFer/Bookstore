using Orion_Books.Models;

namespace Orion_Books.ViewModels
{
    public class LivroDetailViewModel
    {
        public Livro Livro { get; set; }
        public IEnumerable<Livro> LivrosRecomendados { get; set; }
    }
}
