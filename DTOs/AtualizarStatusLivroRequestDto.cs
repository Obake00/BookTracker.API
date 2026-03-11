using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookTracker.API.Entities;

namespace BookTracker.API.DTOs
{
    public class AtualizarStatusLivroRequestDto
    {
        public LeituraStatus Status { get; set; }
    }
}