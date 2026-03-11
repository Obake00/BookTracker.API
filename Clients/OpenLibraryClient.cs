using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookTracker.API.DTOs;


namespace BookTracker.API.Clients
{
    public class OpenLibraryClient
    {
        private readonly HttpClient _httpClient;

        public OpenLibraryClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ExternalBookDto>> SearchAsync(string query, CancellationToken cancellationToken = default)
        {
            var url = $"search.json?q={Uri.EscapeDataString(query)}";

            var response = await _httpClient.GetFromJsonAsync<OpenLibrarySearchResponse>(url, cancellationToken);

            if (response?.Docs == null)
                return new List<ExternalBookDto>();

            return response.Docs
                .Where(d => !string.IsNullOrWhiteSpace(d.Title))
                .Take(20)
                .Select(d => new ExternalBookDto
                {
                    Title = d.Title!,
                    Authors = d.Author_Name ?? new List<string>(),
                    PublishYear = d.First_Publish_Year,
                    CoverId = d.Cover_I?.ToString(),
                    CoverUrl = d.Cover_I.HasValue
                        ? $"https://covers.openlibrary.org/b/id/{d.Cover_I}-M.jpg"
                        : null,
                    OpenLibraryKey = d.Key
                })
                .ToList();
        }
    }
}