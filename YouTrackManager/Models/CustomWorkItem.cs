using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTrackManager.Models
{
    public class CustomWorkItem
    {
        public string IssueId { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Duration { get; set; }
    }
}
