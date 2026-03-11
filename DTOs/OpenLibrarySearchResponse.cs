using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookTracker.API.DTOs
{
    public class OpenLibrarySearchResponse
    {
        public List<OpenLibraryDoc> Docs { get; set; } = [];
    }
}