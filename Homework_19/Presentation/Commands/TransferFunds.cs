using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Application.Commands
{
    public class TransferFunds
    {
        public record Command(int clientId, int recipientId, decimal amountTransfer) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly IDataAccess _data;

            public Handler(IDataAccess data)
            {
                _data = data;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await Task.Run(() => _data.TransferFunds(request.clientId, request.recipientId, request.amountTransfer));
                return Unit.Value;
            }
        }
    }
}