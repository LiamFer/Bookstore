namespace Orion_Books.Models
{
    public class LivroDetailViewModel
    {
        public Livro Livro { get; set; }
        public IEnumerable<Livro> LivrosRecomendados { get; set; }
    }
}
