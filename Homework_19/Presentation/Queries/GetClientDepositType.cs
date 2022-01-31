using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence.Models;

namespace Application.Queries
{
    public class GetClientDepositType
    {
        public record Query(int clientId) : IRequest<string>;

        public class Handler : IRequestHandler<Query, string>
        {
            private readonly IDataAccess _data;

            public Handler(IDataAccess data)
            {
                _data = data;
            }

            public async Task<string> Handle(Query request, CancellationToken cancellationToken)
            {
                return await Task.FromResult(_data.GetClientDepositType(request.clientId));
            }
        }
    }
}