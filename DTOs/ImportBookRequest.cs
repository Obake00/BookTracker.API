using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ImportBookRequest
{
    public string Title { get; set; } = null!;
    public List<string> Authors { get; set; } = new();
    public int? PublishYear { get; set; }
    public string? CoverUrl { get; set; }
    public string? OpenLibraryKey { get; set; }
}