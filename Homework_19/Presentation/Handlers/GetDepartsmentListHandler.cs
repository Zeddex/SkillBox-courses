using System.Collections.Generic;
using MediatR;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Queries;
using Domain.Entities;
using Persistence.Models;

namespace Application.Handlers
{
    public class GetDepartmentsListHandler : IRequestHandler<GetDepartmentsListQuery, List<Department>>
    {
        private readonly IDataAccess _data;

        public GetDepartmentsListHandler(IDataAccess data)
        {
            _data = data;
        }

        public async Task<List<Department>> Handle(GetDepartmentsListQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_data.DepartmentsList().ToList());
        }
    }
}
