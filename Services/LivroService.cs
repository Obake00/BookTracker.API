using BookTracker.API.Data;
using BookTracker.API.DTOs;
using BookTracker.API.Entities;


namespace ReadingTrackerAPI.Services
{
    public class LivroService
    {
        private readonly AppDbContext _context;

public LivroService(AppDbContext context)
        {
            _context = context;
        }

        public List<Livro> ListarLivros(Guid usuarioId)
        {
            return _context.Livros
                .Where(l => l.UsuarioId == usuarioId)
                .ToList();
        }

        public Livro? BuscarLivroPorId(Guid id, Guid usuarioId)
        {
            return _context.Livros
                .FirstOrDefault(l => l.Id == id && l.UsuarioId == usuarioId);
        }

        public Livro CriarLivro(CriarLivroRequestDto request, Guid usuarioId)
        {
            var livro = new Livro
            {
                Id = Guid.NewGuid(),
                Titulo = request.Titulo,
                Autor = request.Autor,
                Ano = request.Ano,
                Status = request.Status,
                UsuarioId = usuarioId
            };

            _context.Livros.Add(livro);
            _context.SaveChanges();

            return livro;
        }

        public Livro? AtualizarLivro(Guid id, AtualizarLivroRequestDto request, Guid usuarioId)
        {
            var livro = _context.Livros
                .FirstOrDefault(l => l.Id == id && l.UsuarioId == usuarioId);

            if (livro == null)
                return null;

            livro.Titulo = request.Titulo;
            livro.Autor = request.Autor;
            livro.Ano = request.Ano;
            livro.Status = request.Status;

            _context.SaveChanges();

            return livro;
        }

        public bool DeletarLivro(Guid id, Guid usuarioId)
        {
            var livro = _context.Livros
                .FirstOrDefault(l => l.Id == id && l.UsuarioId == usuarioId);

            if (livro == null)
                return false;

            _context.Livros.Remove(livro);
            _context.SaveChanges();

            return true;
        }

        public Livro? AtualizarStatusLivro(Guid id, LeituraStatus status, Guid usuarioId)
        {
            var livro = _context.Livros
                .FirstOrDefault(l => l.Id == id && l.UsuarioId == usuarioId);

            if (livro == null)
                return null;

            livro.Status = status;

            _context.SaveChanges();

            return livro;
        }
    }
}