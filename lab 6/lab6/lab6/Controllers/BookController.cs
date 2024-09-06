//BookController.cs
using lab6.CommonService.DatabaseService;
using lab6.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lab6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly IDatabaseService _databaseService;

        public BookController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Book>))] //make api looks clean
        public IReadOnlyList<Book> GetBooks()
        {
            var books = _databaseService.GetBooks();
            return books;

        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Book))] //makes api looks cleaner
        [ProducesResponseType(400)]
        public IActionResult GetBook(int id)
        {
            var book = _databaseService.GetBookById(id);
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(book);
        }

        [HttpPost("addBook")]
        public string AddBook(Book book)
        {
            return _databaseService.AddBook(book);
        }
    }
}
