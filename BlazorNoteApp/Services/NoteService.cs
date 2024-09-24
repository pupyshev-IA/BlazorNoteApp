using BlazorNoteApp.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Net.Mime;
using System.Text;

namespace BlazorNoteApp.Services
{
    public class NoteService : INoteService<Note>
    {
        private readonly Uri _baseUri;

        public NoteService(string baseUri = "http://localhost:5263")
        {
            _baseUri = new Uri(baseUri);
        }

        public async Task<Note> Add(Note note)
        {
            using var http = new HttpClient();

            var relativeUri = new Uri("api/v1/notes/add", UriKind.Relative);
            var uri = new Uri(_baseUri, relativeUri);
            var content = new StringContent(JsonConvert.SerializeObject(note), Encoding.UTF8, MediaTypeNames.Application.Json);

            var response = await http.PostAsync(uri, content);
            var addedNote = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Note>(addedNote);
        }

        public async Task Delete(long id)
        {
            using var http = new HttpClient();

            var relativeUri = new Uri("api/v1/notes/delete", UriKind.Relative);
            var uriBuilder = new UriBuilder(new Uri(_baseUri, relativeUri))
            {
                Query = $"id={id}"
            };

            await http.DeleteAsync(uriBuilder.Uri);
        }

        public Task<Note?> Get(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<Page<Note>> GetPage(ushort pageNumber, int itemsCount)
        {
            using var http = new HttpClient();

            var relativeUri = new Uri("api/v1/notes/list", UriKind.Relative);
            var uriBuilder = new UriBuilder(new Uri(_baseUri, relativeUri))
            {
                Query = $"pageNumber={pageNumber}&itemsCount={itemsCount}"
            };

            var response = await http.GetAsync(uriBuilder.Uri);
            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Page<Note>>(result);
        }

        public async Task<Note?> Update(Note note)
        {
            using var http = new HttpClient();

            var relativeUri = new Uri("api/v1/notes/update", UriKind.Relative);
            var uri = new Uri(_baseUri, relativeUri);

            var response = await http.PutAsJsonAsync(uri, note);
            var result = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<Note>(result);
        }
    }
}
