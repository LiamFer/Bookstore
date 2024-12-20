﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Orion_Books.Models;

namespace Orion_Books.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
            
        }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Emprestimo> Emprestimo { get; set; }


    }
}
