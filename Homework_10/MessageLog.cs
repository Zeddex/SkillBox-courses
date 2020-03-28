using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework_10
{
    struct MessageLog
    {
        public string Time { get; set; }
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string Msg { get; set; }

        public MessageLog (string time, string firstname, string msg, long id)
        {
            Time = time;
            Id = id;
            FirstName = firstname;
            Msg = msg;
        }
    }
}
