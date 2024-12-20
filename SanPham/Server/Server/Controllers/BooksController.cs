using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Services;
using static System.Reflection.Metadata.BlobBuilder;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : Controller
    {
        private readonly BooksService _booksService;
        public BooksController(BooksService booksService) =>
             _booksService = booksService;

        [HttpGet]
        public async Task<List<Book>> Get() =>
      await _booksService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Book>> Get(string id)
        {
            var book = await _booksService.GetAsync(id);

            if (book is null)
            {
                return NotFound();
            }
            
            return book;
        }

        [HttpGet("category/{id:length(1)}")]
        public async Task<ActionResult<List<Book>>> Get(int id)
        {
            var book = await _booksService.GetAsyncCategory(id);

            if (book is null || !book.Any())
                return NotFound();

            return book;
        }

    }
}
