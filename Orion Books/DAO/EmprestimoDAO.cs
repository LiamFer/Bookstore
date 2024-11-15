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


        public async Task<IEnumerable<Emprestimo>> GetCurrentBooks(string userId)
        {
            return await Context.Emprestimo
            .Include(e => e.Livro)
            .Where(e => e.DataEntrega == null && e.Usuario.Id == userId)
            .ToListAsync();
        }

        public async Task<IEnumerable<Emprestimo>> GetHistory(string userId)
        {
            // Retornando uma List de Livros que foram lidos e já devolvidos por determinado Usuário
            return await Context.Emprestimo
            .Include(e => e.Livro)
            .Where(e => e.DataEntrega != null && e.Usuario.Id == userId).OrderByDescending(e => e.DataEntrega)
            .ToListAsync();
        }

        public async Task<IEnumerable<Livro>> BuildBookRecommendation(string userId)
        {
            var mostReadGenres = await Context.Emprestimo
                .Include(e => e.Livro)
                .Where(e => e.Usuario.Id == userId)
                .GroupBy(e => e.Livro.Genero)
                .OrderByDescending(g => g.Count()) 
                .Take(3)                           
                .Select(g => g.Key)                
                .ToListAsync();

            IEnumerable<Livro> Recommendations = await Context.Emprestimo
                .Include(e => e.Livro)
                .Where(e => mostReadGenres.Contains(e.Livro.Genero))
                .Select(e => e.Livro)
                .Distinct()
                .OrderBy(r => Guid.NewGuid())
                .Take(3)
                .ToListAsync();

            // Se ele não conseguir voltar recomendações eu só puxo 3 livros aleatórios e mando pra View
            return Recommendations.Count() > 0 ? Recommendations : await Context.Emprestimo
                .Include(e => e.Livro)
                .Select(e => e.Livro)
                .Distinct()
                .OrderBy(r => Guid.NewGuid())
                .Take(3)
                .ToListAsync();

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
