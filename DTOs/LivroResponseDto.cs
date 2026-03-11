namespace BookTracker.API.DTOs;

public class LivroResponseDto
{
    public Guid Id { get; set; }

    public string Titulo { get; set; } = null!;

    public string Autor { get; set; } = null!;

    public int? Ano { get; set; }

    public string Status { get; set; } = null!;

    public Guid? UsuarioId { get; set; }

    public string? UsuarioNome { get; set; }
}