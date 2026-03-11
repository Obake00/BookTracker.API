namespace BookTracker.API.DTOs; // Registro de Login

public class LoginRequestDto
{
    public string Email { get; set; } = null!;
    public string Senha { get; set; } = null!;
}
