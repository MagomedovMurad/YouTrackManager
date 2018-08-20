using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouTrackSharp;

namespace YouTrackManager.Utils
{
    public class LogonEventArgs: EventArgs
    {
        public LogonEventArgs(UsernamePasswordConnection connection)
        {
            Connection = connection;
        }
        public UsernamePasswordConnection Connection { get; set; }
    }
}
