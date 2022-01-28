using MediatR;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Application.Queries;
using Domain.Entities;
using Persistence.Models;

namespace Application.Handlers
{
    public class GetDepartmentsListHandler : IRequestHandler<GetDepartmentsListQuery, ObservableCollection<Department>>
    {
        private readonly IDataAccess _data;

        public GetDepartmentsListHandler(IDataAccess data)
        {
            _data = data;
        }

        public Task<ObservableCollection<Department>> Handle(GetDepartmentsListQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_data.DepartmentsList());
        }
    }
}
