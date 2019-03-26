using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EC_WorkLoad.Model.JIRA
{
    public class Fields
    {
        /// <summary>
        /// 标题
        /// </summary>
        public string Summary { get; set; }

        public DateTime Created { get; set; }

        public User Creator { get; set; }

        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        public DateTime Updated { get; set; }

        public WorkLog WorkLog { get; set; }

        public JiraStatus Status { get; set; }

        public IssueType IssueType { get; set; }

        public User Assignee { get; set; }

        public JiraTask Parent { get; set; }

        public List<Component> Components { get; set; }

        public List<JiraTask> SubTasks { get; set; }

        public JiraProject Project { get; set; }

        public DateTime LastViewed { get; set; }
    }
}
