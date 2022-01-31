using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence.Models;

namespace Application.Commands
{
    public class AddTransaction
    {
        public record Command(int clientId, string operation) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly IDataAccess _data;

            public Handler(IDataAccess data)
            {
                _data = data;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await Task.Run(() => _data.AddTransaction(request.clientId, request.operation));
                return Unit.Value;
            }
        }
    }
}