using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Queries;
using MediatR;

namespace Domain.Handlers
{
    public class GetClientsByDepIdHandler : IRequestHandler<GetClientsByDepIdQuery, Dictionary<string, decimal>>
    {
        public Task<Dictionary<string, decimal>> Handle(GetClientsByDepIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
