using EC_WorkLoad.Model.AppModel;
using EC_WorkLoad.Model.Argument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC_WorkLoad.Model.Delegates
{
    public class CommonDelegate
    {
        public delegate void PassValuesHandler(object sender, PassValueEventArgs e);

        public delegate void LoginValuesHandler(UserInfo userInfo,WorksEntity worksEntity, PassValueEventArgs e);
    }
}
