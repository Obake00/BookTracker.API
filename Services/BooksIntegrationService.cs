namespace BookTracker.API.Services;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookTracker.API.Clients;
using BookTracker.API.Data;
using BookTracker.API.DTOs;
using BookTracker.API.Entities;


public class BooksIntegrationService
{
    private readonly OpenLibraryClient _openLibraryClient;
    private readonly AppDbContext _context;

    public BooksIntegrationService(OpenLibraryClient openLibraryClient, AppDbContext context)
    {
        _openLibraryClient = openLibraryClient;
        _context = context;
    }

    public Task<List<ExternalBookDto>> SearchAsync(string query, CancellationToken cancellationToken = default)
    {
        return _openLibraryClient.SearchAsync(query, cancellationToken);
    }

    public async Task<List<Livro>> ImportAsync(List<ImportBookRequest> requests, Guid usuarioId, CancellationToken cancellationToken = default)
    {
        var livros = requests.Select(r => new Livro
        {
            Id = Guid.NewGuid(),
            Titulo = r.Title,
            Autor = string.Join(", ", r.Authors),
            Ano = r.PublishYear,
            Status = LeituraStatus.Quero_Ler,
            UsuarioId = usuarioId
        }).ToList();

        _context.Livros.AddRange(livros);
        await _context.SaveChangesAsync(cancellationToken);

        return livros;
    }
}