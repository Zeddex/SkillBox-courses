using System;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace Homework_22_WPF.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private readonly IMediator _mediator;

        public MainWindowViewModel(IMediator mediator)
        {
            _mediator = mediator;
        }

        #region Commands

        #endregion
    }
}
