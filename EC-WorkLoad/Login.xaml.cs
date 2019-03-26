using Newegg.API.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Configuration;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using EC_WorkLoad.Model;
using EC_WorkLoad.Model.JIRA;
using EC_WorkLoad.Model.Argument;
using static EC_WorkLoad.Model.Delegates.CommonDelegate;
using EC_WorkLoad.Model.AppModel;

namespace EC_WorkLoad
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        private event LoginValuesHandler _loginValuesEvent;

        public LoginWindow(LoginValuesHandler e)
        {
            _loginValuesEvent = e;
            InitializeComponent();
        }

        /// <summary>
        /// 登陆（以调用 JIRA 等信息）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            //var client = BuildJiraApiClientByPath(ConfigurationManager.AppSettings[AppSettingStatic.JIRA_SEARCH_PATH]);
            var userName = UserName.Text ?? string.Empty;
            var password = Password.Password ?? string.Empty;

            if (string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("UserName can't be empty.");
                return;
            }

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Password can't be empty.");
                return;
            }

            var userInfo = new UserInfo(userName, password, GetEncodedCredentials(userName, password));
            var todayWorks = GetWorkByUser(userName, password, true, userInfo.UN_PWD_Encoding);
            var overloadWorks = GetWorkByUser(userName, password, false, userInfo.UN_PWD_Encoding);
            var worksEntity = new WorksEntity(todayWorks, overloadWorks);

            DialogResult = true;//指定成功
            CloseSelf(userInfo, worksEntity,null);//关闭自己
        }

        private List<JiraTask> GetWorkByUser(string userName,string password,bool isTodayWork) {

            #region 参数 check
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("UserName");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("Password");
            }
            userName = userName.ToLower();
            password = password.ToLower();
            if (!RegexStatic.ShortNameRe.IsMatch(userName) && !RegexStatic.EmailRe.IsMatch(userName))
            {
                throw new ArgumentException("UserName is not valid.(we need such like az8g or alvin.x.zhang@newegg.com)", "UserName");
            }
            #endregion

            try
            {
                var client = BuildJiraApiClientByPath(ConfigurationManager.AppSettings[AppSettingStatic.JIRA_SEARCH_PATH]);
                var searchQueryFormat = "?jql=assignee='{0}' and duedate {1} ORDER BY duedate desc&fields={2}";
                var fieldsList = new List<string>() {
                "created",
                "updated",
                "summary",
                "duedate",
                "worklog",
                "parent",
                "creator",
                "fixVersions",
                "lastViewed",
                "resolution",
                "priority",
                "labels",
                "assignee",
                "status",
                "components",
                "subtasks",
                "reporter",
                "progress",
                "issuetype",
                "project",
                "comment",
                "description"
            };

                var entireSearchUrl = string.Format(searchQueryFormat, userName,
                    isTodayWork? ">= new()":$"< {DateTime.Now.ToString("YYYY-MM-DD")}", 
                    string.Join(",", fieldsList) ?? "-1");//短名或者是邮箱，fields 无限制则为 -1

                var res = client.Get<JIRAResponse>(entireSearchUrl, null);
                
                return res.Issues;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<JiraTask> GetWorkByUser(string userName, string password, bool isTodayWork,string encodedCredentials)
        {

            #region 参数 check
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentNullException("UserName");
            }
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException("Password");
            }
            userName = userName.ToLower();
            password = password.ToLower();
            if (!RegexStatic.ShortNameRe.IsMatch(userName) && !RegexStatic.EmailRe.IsMatch(userName))
            {
                throw new ArgumentException("UserName is not valid.(we need such like az8g or alvin.x.zhang@newegg.com)", "UserName");
            }
            #endregion

            if (string.IsNullOrEmpty(encodedCredentials))
            {
                encodedCredentials = GetEncodedCredentials(UserName.Text, Password.Password);
            }

            try
            {
                var client = BuildJiraApiClientByPath(ConfigurationManager.AppSettings[AppSettingStatic.JIRA_SEARCH_PATH], encodedCredentials);
                var searchQueryFormat = "?jql=assignee='{0}' and duedate {1} ORDER BY duedate desc&fields={2}";
                var fieldsList = new List<string>() {
                "created",
                "updated",
                "summary",
                "duedate",
                "worklog",
                "parent",
                "creator",
                "fixVersions",
                "lastViewed",
                "resolution",
                "priority",
                "labels",
                "assignee",
                "status",
                "components",
                "subtasks",
                "reporter",
                "progress",
                "issuetype",
                "project",
                "comment",
                "description"
            };

                var entireSearchUrl = string.Format(searchQueryFormat, userName,
                    isTodayWork ? ">= now()" : $"< '{DateTime.Now.ToString("yyyy-MM-dd")}'",
                    string.Join(",", fieldsList) ?? "-1");//短名或者是邮箱，fields 无限制则为 -1

                var res = client.Get<JIRAResponse>(entireSearchUrl, null);

                return res.Issues;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private RestAPIClient BuildJiraApiClientByPath(string path) {
            var encodedCredentials = GetEncodedCredentials(UserName.Text, Password.Password);
            var url = new StringBuilder()
                .Append(ConfigurationManager.AppSettings[AppSettingStatic.JIRA_HOST])
                .Append(path).ToString();
            var client = new RestAPIClient(url);
            client.AddCustomHeader("Authorization", string.Format("Basic {0}", encodedCredentials));
            client.ContentType = "application/json";
            return client;
        }

        private RestAPIClient BuildJiraApiClientByPath(string path,string encodedCredentials)
        {
            if (string.IsNullOrEmpty(encodedCredentials))
            {
                encodedCredentials = GetEncodedCredentials(UserName.Text, Password.Password);
            }
            var url = new StringBuilder()
                .Append(ConfigurationManager.AppSettings[AppSettingStatic.JIRA_HOST])
                .Append(path).ToString();
            var client = new RestAPIClient(url);
            client.AddCustomHeader("Authorization", string.Format("Basic {0}", encodedCredentials));
            client.ContentType = "application/json";
            return client;
        }

        private string GetEncodedCredentials(string userName,string password) {
            string mergedCredentials = string.Format("{0}:{1}", userName?.Trim(), password?.Trim());
            byte[] byteCredentials = Encoding.UTF8.GetBytes(mergedCredentials);
            return Convert.ToBase64String(byteCredentials);
        }

        private void CloseSelf(UserInfo userInfo,WorksEntity worksEntity, PassValueEventArgs e)
        {
            if (_loginValuesEvent != null)
            {
                _loginValuesEvent.Invoke(userInfo, worksEntity, e);
            }
            Close();
        }

    }
}
