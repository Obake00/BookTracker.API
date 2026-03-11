using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using BookTracker.API.Entities;
using ReadingTrackerAPI.Services;
using Microsoft.AspNetCore.Mvc;
using BookTracker.API.DTOs;
using BookTracker.API.Services;
using BookTracker.API.Mappers;

using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace BookTracker.API.Controllers;

[ApiController]
[Route("api/books")]
[Authorize]
public class LivroController : ControllerBase
{
    private readonly BooksIntegrationService _service;
    private readonly ILogger<LivroController> _logger;
    private readonly LivroService _livroService;

    public LivroController(
        ILogger<LivroController> logger,
        LivroService livroService,
        BooksIntegrationService service)
    {
        _logger = logger;
        _livroService = livroService;
        _service = service;
    }

    private Guid? GetUsuarioId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? User.FindFirst("UserId")?.Value;

        if (Guid.TryParse(userIdClaim, out var userId))
            return userId;

        return null;
    }

    [HttpGet("search")]
    public async Task<ActionResult<List<ExternalBookDto>>> Search([FromQuery] string query,CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(query))
            return BadRequest(new { message = "Query é obrigatória." });

        var books = await _service.SearchAsync(query, cancellationToken);
        return Ok(books);
    }

    [HttpPost("import")]
    public async Task<ActionResult<List<LivroResponseDto>>> Import(
        [FromBody] List<ImportBookRequest> request,
        CancellationToken cancellationToken)
    {
        var usuarioId = GetUsuarioId();
        if (usuarioId == null)
            return Unauthorized();

        var livros = await _service.ImportAsync(request, usuarioId.Value, cancellationToken);
        var response = livros.Select(LivroMapper.ToDto).ToList();

        return Ok(response);
    }

    [HttpGet]
    public ActionResult<List<LivroResponseDto>> ListarLivros()
    {
        var usuarioId = GetUsuarioId();
        if (usuarioId == null)
            return Unauthorized();

        var livros = _livroService.ListarLivros(usuarioId.Value);
        return Ok(livros.Select(LivroMapper.ToDto).ToList());
    }

    [HttpGet("{id}")]
    public ActionResult<LivroResponseDto> BuscarLivroPorId(Guid id)
    {
        var usuarioId = GetUsuarioId();
        if (usuarioId == null)
            return Unauthorized();

        var livro = _livroService.BuscarLivroPorId(id, usuarioId.Value);

        if (livro == null)
            return NotFound(new { message = "Livro não encontrado" });

        return Ok(LivroMapper.ToDto(livro));
    }

    [HttpPost]
    public ActionResult<LivroResponseDto> CriarLivro([FromBody] CriarLivroRequestDto request)
    {
        var usuarioId = GetUsuarioId();
        if (usuarioId == null)
            return Unauthorized();

        var livro = _livroService.CriarLivro(request, usuarioId.Value);

        return CreatedAtAction(
            nameof(BuscarLivroPorId),
            new { id = livro.Id },
            LivroMapper.ToDto(livro));
    }

    [HttpPut("{id}")]
    public ActionResult<LivroResponseDto> AtualizarLivro(Guid id, [FromBody] AtualizarLivroRequestDto request)
    {
        var usuarioId = GetUsuarioId();
        if (usuarioId == null)
            return Unauthorized();

        var livro = _livroService.AtualizarLivro(id, request, usuarioId.Value);

        if (livro == null)
            return NotFound(new { message = "Livro não encontrado" });

        return Ok(LivroMapper.ToDto(livro));
    }

    [HttpDelete("{id}")]
    public IActionResult DeletarLivro(Guid id)
    {
        var usuarioId = GetUsuarioId();
        if (usuarioId == null)
            return Unauthorized();

        var deletado = _livroService.DeletarLivro(id, usuarioId.Value);

        if (!deletado)
            return NotFound(new { message = "Livro não encontrado" });

        return NoContent();
    }

    [HttpPatch("{id}/status")]
    public ActionResult<LivroResponseDto> AtualizarStatusLivro(
        Guid id,
        [FromBody] AtualizarStatusLivroRequestDto request)
    {
        var usuarioId = GetUsuarioId();
        if (usuarioId == null)
            return Unauthorized();

        var livro = _livroService.AtualizarStatusLivro(id, request.Status, usuarioId.Value);

        if (livro == null)
            return NotFound(new { message = "Livro não encontrado" });

        return Ok(LivroMapper.ToDto(livro));
    }
}