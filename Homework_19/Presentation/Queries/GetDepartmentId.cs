using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;
using Persistence.Models;

namespace Application.Queries
{
    public class GetDepartmentId
    {
        public record Query(string depName) : IRequest<int>;

        public class Handler : IRequestHandler<Query, int>
        {
            private readonly IDataAccess _data;

            public Handler(IDataAccess data)
            {
                _data = data;
            }

            public Task<int> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_data.GetDepartmentId(request.depName));
            }
        }
    }
}
