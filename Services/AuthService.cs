using BookTracker.API.Data;
using BookTracker.API.DTOs;
using BookTracker.API.DTOs.Auth;
using BookTracker.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookTracker.API.Services;

public class AuthService
{
    private readonly AppDbContext _context;
    private readonly JwtService _jwtService;

    public AuthService(AppDbContext context, JwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    public async Task<AuthResponseDto?> RegisterAsync(RegisterRequestDto request, CancellationToken cancellationToken = default)
    {
        var email = request.Email.Trim().ToLower();

        var exists = await _context.Usuarios.AnyAsync(u => u.Email == email, cancellationToken);
        if (exists)
            return null;

        var usuario = new Usuario
        {
            Id = Guid.NewGuid(),
            Nome = request.Nome,
            Email = email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Senha),
            Role = "User"
        };

        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync(cancellationToken);

        var token = _jwtService.GenerateToken(usuario);

        return new AuthResponseDto
        {
            Token = token,
            UserId = usuario.Id,
            Email = usuario.Email,
            Role = usuario.Role
        };
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto request, CancellationToken cancellationToken = default)
    {
        var email = request.Email.Trim().ToLower();

        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        if (usuario == null)
            return null;

        var senhaValida = BCrypt.Net.BCrypt.Verify(request.Senha, usuario.PasswordHash);
        if (!senhaValida)
            return null;

        var token = _jwtService.GenerateToken(usuario);

        return new AuthResponseDto
        {
            Token = token,
            UserId = usuario.Id,
            Email = usuario.Email,
            Role = usuario.Role
        };
    }
}