using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC_WorkLoad.Model.JIRA
{
    public class JIRAResponse
    {
        public string Expand { get; set; }

        public int StartAt { get; set; }

        public int MaxResult { get; set; }

        public int Total { get; set; }

        public List<JiraTask> Issues { get; set; }
    }
}
