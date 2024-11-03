using Orion_Books.Models;

namespace Orion_Books.Interfaces
{
    public interface IUsuarioDAO
    {
        Task<IEnumerable<Usuario>> GetAll();
        Task<Usuario> GetById(int id);

        bool Add(Usuario emprestimo);
        bool Update(Emprestimo emprestimo);
        bool Delete(Emprestimo emprestimo);
        bool Save();
    }
}
