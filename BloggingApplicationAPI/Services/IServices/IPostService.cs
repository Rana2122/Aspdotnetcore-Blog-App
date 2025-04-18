using BloggingApplicationAPI.Models;

namespace BloggingApplicationAPI.Services.IServices
{
    public interface IPostService
    {
        Task<List<Post>> GetAllPostsAsync();
        Task<Post> GetPostByIdAsync(int id);
        Task<Post> CreatePostAsync(Post post);
        Task<Post> UpdatePostAsync(int id, Post post);
        Task<bool> DeletePostAsync(int id);
    }
}
