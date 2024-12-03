# Just a Bookstore 📚

[![C#](https://img.shields.io/badge/C%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)](https://learn.microsoft.com/dotnet/csharp/)
[![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-%23007ACC.svg?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/apps/aspnet)
[![Entity Framework](https://img.shields.io/badge/Entity%20Framework-%233178C6.svg?style=for-the-badge&logo=microsoft&logoColor=white)](https://learn.microsoft.com/ef/)
[![Bootstrap](https://img.shields.io/badge/Bootstrap-%23563D7C.svg?style=for-the-badge&logo=bootstrap&logoColor=white)](https://getbootstrap.com/)
[![HTML5](https://img.shields.io/badge/HTML5-%23E34F26.svg?style=for-the-badge&logo=html5&logoColor=white)](https://developer.mozilla.org/docs/Web/HTML)
[![CSS3](https://img.shields.io/badge/CSS3-%231572B6.svg?style=for-the-badge&logo=css3&logoColor=white)](https://developer.mozilla.org/docs/Web/CSS)
[![JavaScript](https://img.shields.io/badge/JavaScript-%23F7DF1E.svg?style=for-the-badge&logo=javascript&logoColor=black)](https://developer.mozilla.org/docs/Web/JavaScript)
[![SQL Server](https://img.shields.io/badge/SQL_Server-%23CC2927.svg?style=for-the-badge&logo=microsoft-sql-server&logoColor=white)](https://www.microsoft.com/sql-server)

Bookstore é um sistema de biblioteca desenvolvido utilizando **ASP.NET Core MVC**, **Entity Framework** e uma camada DAO personalizada para o gerenciamento de empréstimos de livros. O projeto adota o padrão **Model-View-Controller (MVC)** para manter a organização e separação de responsabilidades dentro do Sistema.

---

## 🎥 Overview
![Demonstração do site](https://github.com/LiamFer/Bookstore/blob/main/Orion%20Books/wwwroot/Misc/Project.gif?raw=true)


---

## 🚀 Funcionalidades

- 📖 Pesquisa e exibição de livros disponíveis.
- 📋 Gerenciamento de empréstimos de livros com datas de devolução.
- 👤 Sistema de autenticação para associar empréstimos aos usuários logados.
- 🖼 Interface moderna com Bootstrap para melhorar a experiência do usuário.
- 📚 Recomendação de livros com base nas leituras feitas pelo usuário.

---

## 🛠️ Tecnologias Utilizadas

- **ASP.NET Core MVC**: Estrutura principal para o desenvolvimento do projeto.
- **Entity Framework**: ORM para comunicação com o banco de dados.
- **MySQL**: Banco de dados utilizado para armazenar informações.
- **Bootstrap**: Para estilização e responsividade do site.
- **HTML, CSS e JavaScript**: Tecnologias essenciais para criação da interface do usuário.

---

## 📂 Estrutura do Projeto

O projeto segue o padrão **MVC**, separando as responsabilidades em três camadas:

### **Model (Modelo)**
- Representa os dados da aplicação.
- Contém as classes principais, como `Livro`, `Emprestimo` e outras relacionadas ao banco de dados.
- Utiliza o **Entity Framework** para mapear essas classes para tabelas no banco de dados.

Exemplo de classe modelo:
```csharp
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
    public string Sinopse { get; set; }
    public bool Disponivel { get; set; }
}
```

### **View (Visão)**
- Responsável por renderizar as páginas para o usuário.
- Utiliza arquivos **Razor (.cshtml)**, permitindo uma integração fluida entre HTML e C#.
- As páginas foram estilizadas com **Bootstrap**, garantindo responsividade.

Exemplo de uma view:
```razor
@model LivroBorrowViewModel
<div class="container">
    <h1>@Model.Livro.Titulo</h1>
    <p><strong>Autor:</strong> @Model.Livro.Autor</p>
    <p><strong>Gênero:</strong> @Model.Livro.Genero</p>
    <p><strong>Sinopse:</strong> @Model.Livro.Sinopse</p>
</div>
```

### **Controller (Controlador)**
- Processa as requisições do usuário e interage com os modelos para enviar dados às views.
- Contém a lógica de negócio e as regras para manipular as informações.

Exemplo de método controlador:
```csharp
public IActionResult Borrow(int id) {
    var livro = _context.Livros.FirstOrDefault(l => l.Id == id);
    if (livro == null) return NotFound();
    return View(new LivroBorrowViewModel { Livro = livro });
}
```

### **DAO (Data Access Object)**
- Camada criada para isolar a lógica de acesso ao banco de dados.
- Simplifica as operações CRUD, garantindo um código mais limpo nos controladores.

Exemplo de classe DAO:
```csharp
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
}
```

---


## 💡 Como executar o projeto

1. Clone o repositório:
   ```bash
   git clone https://github.com/LiamFer/Bookstore.git
   ```
2. Instale o **.NET SDK** necessário (ex.: .NET 6).
3. Configure a string de conexão com seu banco de dados SQL Server no `appsettings.json`.
4. Execute as migrações do Entity Framework:
   ```bash
   dotnet ef database update
   ```
5. Inicie o projeto:
   ```bash
   dotnet run
   ```
6. Acesse o site em: `http://localhost:5000`.

---

Feito com 💻 por [William](https://github.com/LiamFer).
