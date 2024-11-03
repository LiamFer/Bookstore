using Orion_Books.Models;

namespace Orion_Books.Interfaces
{
    public interface IEmprestimoDAO
    {
        Task<IEnumerable<Emprestimo>> GetAll();
        Task<IEnumerable<Emprestimo>> GetCurrentBooks(string userId);
        Task<Emprestimo> GetById(int id);

        bool Add(Emprestimo emprestimo);
        bool Update(Emprestimo emprestimo);
        bool Delete(Emprestimo emprestimo);
        bool Save();
    }
}
