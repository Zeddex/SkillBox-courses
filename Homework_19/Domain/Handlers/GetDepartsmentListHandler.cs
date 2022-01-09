using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Models;
using Domain.Queries;
using MediatR;

namespace Domain.Handlers
{
    public class GetDepartsmentListHandler : IRequestHandler<GetDepartsmentListQuery, ObservableCollection<Department>>
    {
        private readonly IDataAccess _data;

        public GetDepartsmentListHandler(IDataAccess data)
        {
            _data = data;
        }

        public Task<ObservableCollection<Department>> Handle(GetDepartsmentListQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_data.DepartmentsList());
        }
    }
}
