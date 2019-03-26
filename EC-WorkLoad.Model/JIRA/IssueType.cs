using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC_WorkLoad.Model.JIRA
{
    public class IssueType
    {
        public string Self { get; set; }

        public string Id { get; set; }

        public string Description { get; set; }

        public string IconUrl { get; set; }

        public string Name { get; set; }

        public bool SubTask { get; set; }

        public int AvatarId { get; set; }
    }
}
