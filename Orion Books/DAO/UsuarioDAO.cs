using Microsoft.EntityFrameworkCore;
using Orion_Books.Data;
using Orion_Books.Interfaces;
using Orion_Books.Models;

namespace Orion_Books.DAO
{
    public class UsuarioDAO : IUsuarioDAO
    {

        private readonly ApplicationDbContext Context;
        public UsuarioDAO(ApplicationDbContext context)
        {
            Context = context;
        }

        public bool Add(Usuario user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(Usuario user)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Usuario>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> GetById(string id)
        {
            return await Context.Users.Include(e => e.Endereco).Where(e => e.Id == id).FirstAsync();
        }

        public async Task<Usuario> GetNoTracking(string id)
        {
            return await Context.Users.Include(e => e.Endereco).Where(e => e.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public bool Save()
        {
            return Context.SaveChanges() > 0 ? true : false;
        }

        public bool Update(Usuario user)
        {
            Context.Users.Update(user);
            return Save();
        }
    }
}
