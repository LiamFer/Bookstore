using Microsoft.EntityFrameworkCore;
using Orion_Books.Data;
using Orion_Books.Interfaces;
using Orion_Books.Models;

namespace Orion_Books.DAO
{
    public class EmprestimoDAO : IEmprestimoDAO
    {
        private readonly ApplicationDbContext Context;

        public EmprestimoDAO(ApplicationDbContext context)
        {
            Context = context;
        }

        public bool Add(Emprestimo emprestimo)
        {
            Context.Add(emprestimo);
            return Save();
        }

        public bool Delete(Emprestimo emprestimo)
        {
            Context.Remove(emprestimo);
            return Save();
        }

        public async Task<IEnumerable<Emprestimo>> GetAll()
        {
            return await Context.Emprestimo.ToListAsync();
        }

        public async Task<Emprestimo> GetById(int id)
        {
            return await Context.Emprestimo.FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = Context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Emprestimo emprestimo)
        {
            Context.Update(emprestimo);
            return Save();
        }
    }
}
