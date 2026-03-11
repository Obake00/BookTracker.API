using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookTracker.API.DTOs
{
    public class OpenLibraryDoc
    {
        public string? Title { get; set; }
        public List<string>? Author_Name { get; set; }
        public int? First_Publish_Year { get; set; }
        public int? Cover_I { get; set; }
        public string? Key { get; set; }
    }
}