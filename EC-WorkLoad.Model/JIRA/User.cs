using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC_WorkLoad.Model.JIRA
{
    public class User
    {
        public string Self { get; set; }

        public string Name { get; set; }

        public string Key { get; set; }

        public string EmailAddress { get; set; }

        public string DisplayName { get; set; }

        public bool Active { get; set; }

        public string TimeZone { get; set; }
    }
}
