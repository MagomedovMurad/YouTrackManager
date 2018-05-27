using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YouTrackSharp.Issues;

namespace YouTrackManager.Models
{
    public class CustomWorkItem
    {
        public Issue Issue { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
