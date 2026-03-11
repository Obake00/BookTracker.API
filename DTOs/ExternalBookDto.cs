using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookTracker.API.DTOs
{
    public class ExternalBookDto
    {
        public string Title { get; set; } = null!;
        public List<string> Authors { get; set; } = new();
        public int? PublishYear { get; set; }
        public string? CoverUrl { get; set; }

        // útil para importar depois
        public string? OpenLibraryKey { get; set; }
        public string? CoverId { get; set; }
    }
}