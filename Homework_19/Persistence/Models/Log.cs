using System;
using Application.Commands;
using MediatR;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;

namespace Persistence.Models
{
    public class Log
    {
        public List<string> logFile = new();
        //public ObservableCollection<string> logFile = new();
        private readonly IMediator _mediator;

        public Log(IMediator mediator)
        {
            _mediator = mediator;
            logFile = new();
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
            _mediator.Send(new AddTransaction.Command(clientId, message));
        }
    }
}
