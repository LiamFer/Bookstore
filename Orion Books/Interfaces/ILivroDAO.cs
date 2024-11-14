using Orion_Books.Models;
using Orion_Books.ViewModels;

namespace Orion_Books.Interfaces
{
    public interface ILivroDAO
    {
        Task<IEnumerable<Livro>> GetAll();
        Task<Livro> GetById(int id);
        Task<IEnumerable<Livro>> GetLivroByGenero(string genero);

        Task<LivroBorrowViewModel> getBookWithLoan(int id);

        bool Add(Livro livro);
        bool Update(Livro livro);
        bool Delete(Livro livro);
        bool Save();
    }
}
