using BlazorNoteApp.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace BlazorNoteApp.Services
{
    public class NoteService : INoteService<Note>
    {
        private readonly Uri _baseUri;

        public NoteService(string baseUri = "http://localhost:5263")
        {
            _baseUri = new Uri(baseUri);
        }

        public Task<Note> Add(Note note)
        {
            throw new NotImplementedException();
        }

        public async Task Delete(long id)
        {
            var relativeUri = new Uri("api/v1/notes/delete", UriKind.Relative);
            var uriBuilder = new UriBuilder(new Uri(_baseUri, relativeUri))
            {
                Query = $"id={id}"
            };

            using var http = new HttpClient();
            await http.DeleteAsync(uriBuilder.Uri);
        }

        public Task<Note?> Get(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<Page<Note>> GetPage(ushort pageNumber, int itemsCount)
        {
            var relativeUri = new Uri("api/v1/notes/list", UriKind.Relative);
            var uriBuilder = new UriBuilder(new Uri(_baseUri, relativeUri))
            {
                Query = $"pageNumber={pageNumber}&itemsCount={itemsCount}"
            };

            using var http = new HttpClient();
            var response = await http.GetAsync(uriBuilder.Uri);
            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Page<Note>>(result);
        }

        public async Task<Note?> Update(Note note)
        {
            var relativeUri = new Uri("api/v1/notes/update", UriKind.Relative);
            var uri = new Uri(_baseUri, relativeUri);

            using var http = new HttpClient();
            var response = await http.PutAsJsonAsync(uri, note);
            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Note>(result);
        }
    }
}
