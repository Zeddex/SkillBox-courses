﻿using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;

namespace Application.Commands
{
    public class MakeSimpleDeposit
    {
        public record Command(int clientId, decimal amount) : IRequest;

        public class Handler : IRequestHandler<Command>
        {
            private readonly IDataAccess _data;

            public Handler(IDataAccess data)
            {
                _data = data;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                await Task.Run(() => _data.MakeSimpleDeposit(request.clientId, request.amount));
                return Unit.Value;
            }
        }
    }
}