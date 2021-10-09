using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiCqrs.Models;

namespace WebApiCqrs.Features.Books
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BooksController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<IEnumerable<Book>> GetBooks() => await _mediator.Send(new GetBooks.Query());

        [HttpGet("{id}")]
        public async Task<Book> GetBook(int id) => await _mediator.Send(new GetBookById.Query { Id = id });

        [HttpPost]
        public async Task<ActionResult> CreateBook([FromBody] AddNewBook.Command command)
        {
            var createdBookId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetBook), new { id = createdBookId }, null);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBook(int id)
        {
            await _mediator.Send(new DeleteBook.Command { Id = id });
            return NoContent();
        }
    }
}
