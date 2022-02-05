using System.Collections.Generic;
using System.Collections.ObjectModel;
using Application.Commands;
using MediatR;

namespace Persistence.Models
{
    public class Log
    {
        public List<string> logFile = new();
        //private readonly IDataAccess _provider = new BankProvider();
        private readonly IMediator _mediator;

        public Log(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Add message to log list
        /// </summary>
        /// <param name="msg"></param>
        public void AddToLog(string msg)
        {
            logFile.Add(msg);
        }

        public void AddToDbLog(int clientId, string message)
        {
            //_provider.AddTransaction(clientId, message);
            _mediator.Send(new AddTransaction.Command(clientId, message));
        }
    }
}
