using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;
using Persistence.Models;

namespace Application.Queries
{
    public class GetDepartmentsList
    {
        //public record Query : IRequest<List<Department>>;
        public class Query : IRequest<List<Department>>
        { }

        public class Handler : IRequestHandler<Query, List<Department>>
        {
            private readonly IDataAccess _data;

            public Handler(IDataAccess data)
            {
                _data = data;
            }

            public Task<List<Department>> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(_data.DepartmentsList().ToList());
            }
        }
    }
}
