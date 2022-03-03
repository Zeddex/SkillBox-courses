using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Application.Queries
{
    public class GetTransactionsList
    {
        public record Query : IRequest<List<string>>;

        public class Handler : IRequestHandler<Query, List<string>>
        {
            private readonly IDataAccess _data;

            public Handler(IDataAccess data)
            {
                _data = data;
            }

            public async Task<List<string>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await Task.FromResult(_data.TransactionsList());
            }
        }
    }
}
