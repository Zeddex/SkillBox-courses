using MediatR;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Application.Queries;
using Domain.Entities;
using MediatR;

namespace Application.Handlers
{
    //public class GetDepartsmentListHandler : IRequestHandler<GetDepartsmentListQuery, ObservableCollection<Department>>
    //{
    //    private readonly IDataAccess _data;

    //    public GetDepartsmentListHandler(IDataAccess data)
    //    {
    //        _data = data;
    //    }

    //    public Task<ObservableCollection<Department>> Handle(GetDepartsmentListQuery request, CancellationToken cancellationToken)
    //    {
    //        return Task.FromResult(_data.DepartmentsList());
    //    }
    //}
}
