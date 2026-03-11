using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookTracker.API.Entities;

namespace BookTracker.API.DTOs
{
    public class CriarLivroRequestDto
    {
        public string Titulo { get; set; } = null!;
        public string Autor { get; set; } = null!;
        public int? Ano { get; set; }
        public LeituraStatus Status { get; set; }
    }
}