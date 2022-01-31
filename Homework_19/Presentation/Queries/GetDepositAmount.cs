using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;
using Persistence.Models;

namespace Application.Queries
{
    public class GetDepositAmount
    {
        public record Query(int clientId, string depTypeInfo, int depRate) : IRequest<List<decimal>>;

        public class Handler : IRequestHandler<Query, List<decimal>>
        {
            private readonly IDataAccess _data;

            public Handler(IDataAccess data)
            {
                _data = data;
            }

            public async Task<List<decimal>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await Task.FromResult(_data.DepositInfo(request.clientId, request.depTypeInfo, request.depRate));
            }
        }
    }
}