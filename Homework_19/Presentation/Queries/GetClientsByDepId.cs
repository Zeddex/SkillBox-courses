using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence.Models;

namespace Application.Queries
{
    public class GetClientsByDepId
    {
        public record Query(int depId) : IRequest<Dictionary<string, decimal>>;

        public class Handler : IRequestHandler<Query, Dictionary<string, decimal>>
        {
            private readonly IDataAccess _data;

            public Handler(IDataAccess data)
            {
                _data = data;
            }

            public async Task<Dictionary<string, decimal>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await Task.FromResult(_data.ShowClients(request.depId));
            }
        }
    }
}