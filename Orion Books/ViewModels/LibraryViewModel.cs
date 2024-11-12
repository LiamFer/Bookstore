using Orion_Books.Models;

namespace Orion_Books.ViewModels
{
    public class LibraryViewModel
    {
        public IEnumerable<Emprestimo> history {get;set;}
        public IEnumerable<Emprestimo> library { get; set; }
    }
}
