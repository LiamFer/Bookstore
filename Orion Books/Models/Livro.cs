using System.ComponentModel.DataAnnotations;

namespace Orion_Books.Models
{
    public class Livro
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Autor {  get; set; }
        public string Genero { get; set; }
        public int AnoPublicado { get; set; }
        public string ISBN { get; set; }
        public string CapaURL { get; set; }
        public bool Disponivel { get; set; }
    }
}
