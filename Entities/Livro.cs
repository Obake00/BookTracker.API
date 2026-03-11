namespace BookTracker.API.Entities;

public class Livro
{
    public Guid Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Autor { get; set; } = null!;

    public int? Ano { get; set; }

    public LeituraStatus Status { get; set; }

    public Guid UsuarioId { get; set; }

    public Usuario Usuario { get; set; } = null!;
}