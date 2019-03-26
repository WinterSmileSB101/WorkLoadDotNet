using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC_WorkLoad.Model.JIRA
{
    public class JiraTask
    {
        public string Expand { get; set; }

        public string Id { get; set; }

        /// <summary>
        /// Jira 编号，类似于 CDECXXXXXX
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 查询链接
        /// </summary>
        public string Self { get; set; }

        public Fields Fields { get; set; }
    }
}
