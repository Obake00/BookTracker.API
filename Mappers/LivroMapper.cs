using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookTracker.API.Entities;
using BookTracker.API.DTOs;

namespace BookTracker.API.Mappers
{
    public class LivroMapper
    {
        public static LivroResponseDto ToDto(Livro livro)
        {
            return new LivroResponseDto
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                Autor = livro.Autor,
                Ano = livro.Ano,
                Status = livro.Status.ToString(),
                UsuarioId = livro.UsuarioId,
                UsuarioNome = livro.Usuario?.Nome
            };
        }
    }
}