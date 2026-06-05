using LibraryManagementSystem.Models;
using LibraryManagementSystem.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepo;
        private readonly IAuthorRepository _authorRepo;
        private readonly IGenreRepository _genreRepo;

        public BooksController(
            IBookRepository bookRepo,
            IAuthorRepository authorRepo,
            IGenreRepository genreRepo)
        {
            _bookRepo = bookRepo;
            _authorRepo = authorRepo;
            _genreRepo = genreRepo;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Authors = await _authorRepo.GetAllAsync();

            ViewBag.Genres = await _genreRepo.GetAllAsync();

            var books = await _bookRepo.GetBooksWithDetailsAsync();

            return View(books);
        }

        [HttpPost]
        public async Task<IActionResult> Add(Book book)
        {
            try
            {
                await _bookRepo.AddAsync(book);

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookRepo.DeleteAsync(id);

            return Json(new { success = true });
        }
    }
}