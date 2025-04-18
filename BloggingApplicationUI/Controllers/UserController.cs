using BloggingApplicationUI.BloggingHttpClient;
using BloggingApplicationUI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BloggingApplicationUI.Controllers
{
    public class UserController : Controller
    {
        private readonly BloggingAppHttpClient _httpClient;

        public UserController(BloggingAppHttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Register Page
        public IActionResult Register()
        {
            return View();
        }

        // Handle User Registration
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Call the API to register the user
                    var response = await _httpClient.PostDataAsync<object>("auth/register", model);

                    // On success, redirect to Login page
                    TempData["SuccessMessage"] = "Registration successful. Please login.";
                    return RedirectToAction("Login");
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
                    return View(model);
                }
            }

            // If the model is not valid, return to the registration page
            return View(model);
        }

        // Login Page
        public IActionResult Login()
        {
            return View();
        }

        // Handle User Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    // Call the API to login the user
                    var user = await _httpClient.PostDataAsync<object>("auth/login", model);

                    // On success, store the user data (e.g., in session or cookie)
                    // Here you can optionally store the user session in cookies or session
                    TempData["SuccessMessage"] = "Login successful.";
                    return RedirectToAction("Index", "Post"); // Redirect to homepage or desired landing page
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Login failed: {ex.Message}";
                    return View(model);
                }
            }

            return View(model);
        }

        // Logout (clear session or cookie)
        public IActionResult Logout()
        {
            // Here, you could clear the session or authentication cookie if you set one
            TempData["SuccessMessage"] = "You have been logged out successfully.";
            return RedirectToAction("Login");
        }
    }
}
