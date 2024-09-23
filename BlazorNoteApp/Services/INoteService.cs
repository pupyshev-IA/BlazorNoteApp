using BlazorNoteApp.Models;

namespace BlazorNoteApp.Services
{
    public interface INoteService<T>
    {
        Task<T> Add(T note);
        Task<T?> Get(long id);
        Task<Page<T>> GetPage(ushort pageNumber, int itemsCount);
        Task<T?> Update(T note);
        Task Delete(long id);
    }
}
