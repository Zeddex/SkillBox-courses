using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Homework_15
{
    public class Log
    {
        public ObservableCollection<string> logFile = new ObservableCollection<string>();

        /// <summary>
        /// Add message to log list
        /// </summary>
        /// <param name="msg"></param>
        public void AddToLog(string msg)
        {
            logFile.Add(msg);
        }
    }
}
