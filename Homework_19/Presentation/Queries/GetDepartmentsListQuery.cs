using Domain.Entities;
using MediatR;
using System.Collections.ObjectModel;

namespace Application.Queries
{
    public record GetDepartmentsListQuery() : IRequest<ObservableCollection<Department>>;
}
