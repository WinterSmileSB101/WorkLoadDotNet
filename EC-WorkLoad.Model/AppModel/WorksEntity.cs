using EC_WorkLoad.Model.JIRA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC_WorkLoad.Model.AppModel
{
    public class WorksEntity
    {
        public WorksEntity()
        {
        }

        public WorksEntity(List<JiraTask> todayWorks, List<JiraTask> overLoadWorks)
        {
            TodayWorks = todayWorks ?? throw new ArgumentNullException(nameof(todayWorks));
            OverLoadWorks = overLoadWorks ?? throw new ArgumentNullException(nameof(overLoadWorks));
        }

        public List<JiraTask> TodayWorks { get; set; }

        public List<JiraTask> OverLoadWorks { get; set; }
    }
}
