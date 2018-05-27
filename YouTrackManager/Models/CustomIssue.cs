using System;
using System.Collections.Generic;
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

        public string Url { get { return uri + Id; } }

        public double Duration { get; set; }

        public void Test()
        {

        }
    }
}
