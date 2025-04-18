using Microsoft.AspNetCore.Mvc;
using BloggingApplicationUI.BloggingHttpClient;
using BloggingApplicationUI.Models;
using System;
using System.Threading.Tasks;

namespace BloggingApplicationUI.Controllers
{
    public class PostController : Controller
    {
        private readonly BloggingAppHttpClient _httpClient;

        public PostController(BloggingAppHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            try
            {

                var posts = await _httpClient.GetDataAsync<List<Post>>("posts/Pagination");
                return View(posts);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while fetching the posts: {ex.Message}";
                return View("Error");
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var post = await _httpClient.GetDataAsync<Post>($"posts/{id}");
                return View(post);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while fetching the post details: {ex.Message}";
                return View("Error");
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post post)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var createdPost = await _httpClient.PostDataAsync<Post>("posts", post);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"An error occurred while creating the post: {ex.Message}";
                    return View(post);
                }
            }
            return View(post);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var post = await _httpClient.GetDataAsync<Post>($"posts/{id}");
                return View(post);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while fetching the post for editing: {ex.Message}";
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Post post)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var updatedPost = await _httpClient.PutDataAsync<Post>($"posts/{id}", post);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"An error occurred while updating the post: {ex.Message}";
                    return View(post);
                }
            }
            return View(post);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var post = await _httpClient.GetDataAsync<Post>($"posts/{id}");
                return View(post);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while fetching the post for deletion: {ex.Message}";
                return View("Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var result = await _httpClient.DeleteDataAsync($"posts/{id}");
                if (result)
                    return RedirectToAction(nameof(Index));
                return NotFound();
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while deleting the post: {ex.Message}";
                return View("Error");
            }
        }
    }
}
