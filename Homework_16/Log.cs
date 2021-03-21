using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Homework_16
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
