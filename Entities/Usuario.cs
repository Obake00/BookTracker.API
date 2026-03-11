namespace BookTracker.API.Entities; // Tabela Usuarios no Db

public class Usuario
{
    public Guid Id { get; set; }

    public string Nome { get; set; }

    public string Email { get; set; }

    public string PasswordHash { get; set; }

    public string Role { get; set; }
}