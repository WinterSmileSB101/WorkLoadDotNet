using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC_WorkLoad.Model.JIRA
{
    public class WorkLog
    {
        public int StartAt { get; set; }

        public int MaxResult { get; set; }

        public int Total { get; set; }

        public List<LogDetail> WorkLogs { get; set; }
    }

    public class LogDetail
    {
        public string Self { get; set; }

        public User Author { get; set; }

        public User UpdateAuthor { get; set; }

        public string Comment { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public DateTime Started { get; set; }

        public string TimeSpent { get; set; }

        public long TomeSpentSeconds { get; set; }

        public string Id { get; set; }

        public string IssueId { get; set; }
    }
}
