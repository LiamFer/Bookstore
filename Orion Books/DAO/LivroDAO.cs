using Microsoft.EntityFrameworkCore;
using Orion_Books.Data;
using Orion_Books.Interfaces;
using Orion_Books.Models;
using Orion_Books.ViewModels;

namespace Orion_Books.DAO
{
    public class LivroDAO : ILivroDAO
    {
        private readonly ApplicationDbContext Context;
        public LivroDAO(ApplicationDbContext context)
        {
            Context = context;
        }

        // Método pra Inserir um novo Livro no Banco de Dados
        public bool Add(Livro livro)
        {
            Context.Add(livro);
            return Save();
        }

        public bool Delete(Livro livro)
        {
            Context.Remove(livro);
            return Save();
        }

        public async Task<IEnumerable<Livro>> GetAll()
        {
            return await Context.Livros.ToListAsync();
        }

        public async Task<Livro> GetById(int id)
        {
            return await Context.Livros.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<IEnumerable<Livro>> GetLivroByGenero(string genero)
        {
            return await Context.Livros.Where(l => l.Genero == genero).ToListAsync();
        }

        public async Task<LivroBorrowViewModel> getBookWithLoan(int id)
        {
            LivroBorrowViewModel VM = new LivroBorrowViewModel
            {
                Livro = await Context.Livros.FirstOrDefaultAsync(i => i.Id == id),
                emprestimo = await Context.Emprestimo.FirstOrDefaultAsync(e => e.LivroId == id && e.DataEntrega == null)
            };
            return VM;

            // Eu poderia voltar assim, mas ai na hora de editar eles no banco não sei se daria certo, ai volto o VM q criei
            //return await Context.Emprestimo.Include(e => e.Livro).FirstOrDefaultAsync(e => e.Livro.Id == id);
        }

        public bool Save()
        {
            var saved = Context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Livro livro)
        {
            Context.Update(livro);
            return Save();
        }
    }
}
