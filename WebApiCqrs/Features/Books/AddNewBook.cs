using FluentValidation;
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
    public class AddNewBook
    {
        public class Command : IRequest<int>
        {
            public string Title { get; set; }
            public string Author { get; set; }
            public string Description { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(c => c.Title).NotEmpty().MaximumLength(150);
                RuleFor(c => c.Author).NotEmpty().MaximumLength(100);
            }
        }

        public class CommandHandler : IRequestHandler<Command, int>
        {
            private readonly BookContext _db;

            public CommandHandler(BookContext db) => _db = db;

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                var entity = new Book
                {
                    Title = request.Title,
                    Author = request.Author,
                    Description = request.Description
                };

                await _db.Books.AddAsync(entity, cancellationToken);
                await _db.SaveChangesAsync(cancellationToken);

                return entity.Id;
            }
        }
    }
}
