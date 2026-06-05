using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class GenresController : Controller
    {
        private readonly IGenreRepository _repo;

        public GenresController(IGenreRepository repo)
        {
            _repo = repo;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _repo.GetAllAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Add(Genre genre)
        {
            await _repo.AddAsync(genre);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}