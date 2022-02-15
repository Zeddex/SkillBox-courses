using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Application.Queries
{
    public class GetClientLoan
    {
        public record Query(int clientId) : IRequest<decimal>;

        public class Handler : IRequestHandler<Query, decimal>
        {
            private readonly IDataAccess _data;

            public Handler(IDataAccess data)
            {
                _data = data;
            }

            public async Task<decimal> Handle(Query request, CancellationToken cancellationToken)
            {
                return await Task.FromResult(_data.GetClientLoan(request.clientId));
            }
        }
    }
}