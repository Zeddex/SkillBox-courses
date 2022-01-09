using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;

namespace Domain.Queries
{
    public record GetDepartsmentListQuery() : IRequest<ObservableCollection<Department>>;
}
