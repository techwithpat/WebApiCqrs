using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApiCqrs.Data;

namespace WebApiCqrs.Features.Books
{
    public class DeleteBook
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.Id).GreaterThan(0);
            }
        }

        public class CommandHandler : IRequestHandler<Command, Unit>
        {
            private readonly BookContext _db;

            public CommandHandler(BookContext db) => _db = db;

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var book = await _db.Books.FindAsync(request.Id);
                if (book == null) return Unit.Value;

                _db.Books.Remove(book);
                await _db.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
