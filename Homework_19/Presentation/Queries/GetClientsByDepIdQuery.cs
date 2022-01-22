using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Application.Queries
{
    public record GetClientsByDepIdQuery() : IRequest<Dictionary<string, decimal>>;
}
