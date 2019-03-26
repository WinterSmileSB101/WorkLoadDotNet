using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EC_WorkLoad.Model
{
    public class RegexStatic
    {
        public static readonly Regex ShortNameRe = new Regex(@"^[a-z]{2}[0-9]{1}[a-z]{1}$");
        public static readonly Regex EmailRe = new Regex(@"^[a-z]{1,}.{1}[a-z]{1,}.{1}[a-z]{1,}@[a-z]{1,}.{1}[a-z]{1,}$");
    }
}
