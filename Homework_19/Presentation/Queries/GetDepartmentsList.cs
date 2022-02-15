using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;

namespace Application.Queries
{
    public class GetDepartmentsList
    {
        public record Query : IRequest<List<Department>>;

        public class Handler : IRequestHandler<Query, List<Department>>
        {
            private readonly IDataAccess _data;

            public Handler(IDataAccess data)
            {
                _data = data;
            }

            public async Task<List<Department>> Handle(Query request, CancellationToken cancellationToken)
            {
                return await Task.FromResult(_data.DepartmentsList());
            }
        }
    }
}
