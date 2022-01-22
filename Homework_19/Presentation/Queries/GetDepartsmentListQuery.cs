using Domain.Entities;
using MediatR;
using System.Collections.ObjectModel;

namespace Application.Queries
{
    public record GetDepartsmentListQuery() : IRequest<ObservableCollection<Department>>;
}
