using Orion_Books.Models;

namespace Orion_Books.Interfaces
{
    public interface ILivroDAO
    {
        Task<IEnumerable<Livro>> GetAll();
        Task<Livro> GetById(int id);
        Task<IEnumerable<Livro>> GetLivroByGenero(string genero);

        bool Add(Livro livro);
        bool Update(Livro livro);
        bool Delete(Livro livro);
        bool Save();
    }
}
