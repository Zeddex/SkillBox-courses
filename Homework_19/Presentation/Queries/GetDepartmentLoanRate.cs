using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;
using Persistence.Models;

namespace Application.Queries
{
    public class GetDepartmentLoanRate
    {
        public record Query(int departmentId) : IRequest<int>;

        public class Handler : IRequestHandler<Query, int>
        {
            private readonly IDataAccess _data;

            public Handler(IDataAccess data)
            {
                _data = data;
            }

            public async Task<int> Handle(Query request, CancellationToken cancellationToken)
            {
                return await Task.FromResult(_data.GetDepartmentLoanRate(request.departmentId));
            }
        }
    }
}
