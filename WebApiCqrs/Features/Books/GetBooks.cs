using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApiCqrs.Data;
using WebApiCqrs.Models;

namespace WebApiCqrs.Features.Books
{
    public class GetBooks
    {
        public class Query : IRequest<IEnumerable<Book>> { }

        public class QueryHandler : IRequestHandler<Query, IEnumerable<Book>>
        {
            private readonly BookContext _db;

            public QueryHandler(BookContext db) => _db = db;

            public async Task<IEnumerable<Book>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await _db.Books.ToListAsync(cancellationToken);
            }
        }
    }
}
