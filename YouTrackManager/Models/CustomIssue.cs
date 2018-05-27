using Humanizer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTrackManager.Models
{
    public class CustomIssue
    {
        private const string uri = "http://ticket.infolan.org/issue/"; 

        public string Id { get; set; }

        public string Summary { get; set; }

        public string Name { get { return $"{Id} {Summary}"; } }

        public string Url { get { return uri + Id; } }

        public TimeSpan Duration { get; set; }

        public string Time
        {
            get
            {
                return Duration.Humanize(3).Replace(",","") + " на:";
            }
        }

    }
}
