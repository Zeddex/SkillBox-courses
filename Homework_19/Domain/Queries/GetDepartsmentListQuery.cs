using Domain.Entities;
using MediatR;
using System.Collections.ObjectModel;

namespace Domain.Queries
{
    public record GetDepartsmentListQuery() : IRequest<ObservableCollection<Department>>;
}
