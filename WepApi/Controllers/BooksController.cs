using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WepApi.Models;
using WepApi.Repositories;

namespace WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly RepositoryContext _context;

        public BooksController(RepositoryContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(_context.Books.ToList());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            try
            {
                var book = _context.Books.SingleOrDefault(x => x.Id == id);

                if (book == null)
                    return NotFound();

                return Ok(book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book book)
        {
            try
            {
                _context.Books.Add(book);
                _context.SaveChanges();
                return StatusCode(201, book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook([FromRoute] int id, [FromBody] Book book)
        {
            try
            {
                var entity = _context.Books.SingleOrDefault(x => x.Id == id);

                if (entity == null)
                    return NotFound();

                if (id != book.Id)
                    return BadRequest();


                entity.Title = book.Title;
                entity.Price = book.Price;

                _context.SaveChanges();

                return Ok(book);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        [HttpPatch("{id}")]
        public IActionResult PartiallyUpdateBook([FromRoute] int id, [FromBody] JsonPatchDocument<Book> bookPath)
        {
            try
            {
                var entity = _context.Books.SingleOrDefault(x => x.Id == id);

                if (entity == null)
                    return NotFound();

                bookPath.ApplyTo(entity); 
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult RemoveBook([FromRoute] int id)
        {
            try
            {
                var book = _context.Books.SingleOrDefault(x => x.Id == id);

                if (book == null)
                    return NotFound();


                _context.Books.Remove(book);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
