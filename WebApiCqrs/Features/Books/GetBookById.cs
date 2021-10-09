using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApiCqrs.Data;
using WebApiCqrs.Models;

namespace WebApiCqrs.Features.Books
{
    public class GetBookById
    {
        public class Query : IRequest<Book>
        {
            public int Id { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, Book>
        {
            private readonly BookContext _db;

            public QueryHandler(BookContext db) => _db = db;

            public async Task<Book> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _db.Books.FindAsync(request.Id);
            }
        }
    }
}
