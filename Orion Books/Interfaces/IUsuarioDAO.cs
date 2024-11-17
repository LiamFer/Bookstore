using Orion_Books.Models;

namespace Orion_Books.Interfaces
{
    public interface IUsuarioDAO
    {
        Task<IEnumerable<Usuario>> GetAll();
        Task<Usuario> GetById(string id);
        Task<Usuario> GetNoTracking(string id);


        bool Add(Usuario user);
        bool Update(Usuario user);
        bool Delete(Usuario user);
        bool Save();
    }
}
