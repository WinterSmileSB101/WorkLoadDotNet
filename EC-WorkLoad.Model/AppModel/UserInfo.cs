using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EC_WorkLoad.Model.AppModel
{
    public class UserInfo
    {
        public UserInfo(string userName, string password, string uN_PWD_Encoding)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Password = password ?? throw new ArgumentNullException(nameof(password));
            UN_PWD_Encoding = uN_PWD_Encoding ?? throw new ArgumentNullException(nameof(uN_PWD_Encoding));
        }

        public UserInfo(string userName, string password)
        {
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Password = password ?? throw new ArgumentNullException(nameof(password));
        }

        public UserInfo()
        {
        }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string UN_PWD_Encoding { get; set; }
    }
}
