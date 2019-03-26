using EC_WorkLoad.Model.AppModel;
using EC_WorkLoad.Model.Argument;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Forms;
using static EC_WorkLoad.Model.Delegates.CommonDelegate;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using EC_WorkLoad.Model.Utilis;

namespace EC_WorkLoad
{
    /// <summary>
    /// WorkLoadWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WorkLoadWindow : Window
    {
        private event PassValuesHandler PassValuesEvent;
        private UserInfo UserInfo;
        private WorksEntity WorksEntity;

        private NotifyIcon _notifyIcon = null;

        public WorkLoadWindow(UserInfo userInfo, WorksEntity works, PassValuesHandler e)
        {
            UserInfo = userInfo;
            WorksEntity = works;
            PassValuesEvent = e;
            InitializeComponent();
        }

        public WorkLoadWindow()
        {
            InitializeComponent();
            OpenLoginWindow();
        }

        private void OpenLoginWindow() {
            Hide();// 先隐藏界面
            LoginWindow loginWindow = new LoginWindow(HandleLoginCallBack);
            if (!(loginWindow.ShowDialog() ?? false))
            {
                //登陆失败，
                System.Windows.Forms.MessageBox.Show("登陆失败,请检查信息后重试.");
            }
            else {
                //登陆成功，
                Show();
            }
        }

        /// <summary>
        /// 处理登陆窗口返回的值
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="worksEntity"></param>
        /// <param name="e"></param>
        private void HandleLoginCallBack(UserInfo userInfo, WorksEntity worksEntity, PassValueEventArgs e)
        {
            // 
            System.Windows.Forms.MessageBox.Show(userInfo.Password);
        }

        /// <summary>
        /// 给父窗口传值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseThis(object sender, PassValueEventArgs e)
        {
            PassValuesEvent.Invoke(sender,e);
        }

        /// <summary>
        /// 导航栏左键事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("按下" + e);
        }

        /// <summary>
        /// 头像按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Avator_Click(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;//标识已经处理了事件，阻止冒泡
            var senderType = sender.GetType();
            if (typeof(Ellipse).IsEquivalentTo(senderType))
            {
                var ellButton = sender as Ellipse;
                switch (ellButton?.Name?.ToLower()?.Trim())
                {
                    case "avator":
                        System.Windows.Forms.MessageBox.Show("按下头像");
                        break;
                    default:
                        break;
                }
            }
        }

        private void NavBarBtn_Click(object sender, RoutedEventArgs e)
        {
            var senderType = sender.GetType();
            if (typeof(System.Windows.Controls.Button).IsEquivalentTo(senderType))
            {
                //按钮按下
                var actionButton = sender as System.Windows.Controls.Button;
                switch (actionButton?.Name?.ToLower()?.Trim())
                {
                    case "todowork":
                        break;
                    case "donework":
                        break;
                    case "loghistory":
                        break;
                    case "minsizebtn":
                        InitialTray();
                        break;
                    case "fullscreenbtn":
                        break;
                    case "closebtn":
                        break;
                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// 最小化到系统托盘
        /// </summary>
        private void InitialTray() {
            
            Visibility = Visibility.Hidden;//隐藏主窗体，
            //设置托盘
            _notifyIcon = new NotifyIcon();
            _notifyIcon.BalloonTipText = "WorkLoad is running...";
            _notifyIcon.Text = "WorkLoad";
            _notifyIcon.Visible = true;//托盘按钮可见

            _notifyIcon.Icon = ImageConverterHelper.BitmapConvertToIcon(Properties.Resources.defaultAvator,ImageFormat.Jpeg);
            _notifyIcon.ShowBalloonTip(1000);//气泡显示时间

            _notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(NotifyIcon_MouseClick);
            var exitBtn = new System.Windows.Forms.MenuItem("close");
            exitBtn.Click += new EventHandler(ExitClick);
            _notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(new System.Windows.Forms.MenuItem[] { exitBtn });

            //主窗口的状态变化检测
            StateChanged += Window_StateChanged;
        }

        /// <summary>
        /// 窗口状态变化后触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_StateChanged(object sender, EventArgs e) {
            if (WindowState == WindowState.Minimized) {
                Visibility = Visibility.Hidden;
            }
        }

        private void NotifyIcon_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e) {
            //最小化或者显示窗体
            if (e.Button == MouseButtons.Left)
            {
                if (Visibility == Visibility.Visible)
                {
                    Visibility = Visibility.Hidden;
                    ShowInTaskbar = false;//使 Form 不在任务栏上显示
                }
                else
                {
                    Visibility = Visibility.Visible;
                    ShowInTaskbar = false;// 使 Form 不在任务栏上显示
                    Activate();
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                //上下文菜单
                //退出
                ExitClick(sender, e);
            }
        }

        private void ExitClick(object sender, EventArgs e)
        {
            if (System.Windows.MessageBox.Show("Are you sure to exit?"
                , "application"
                , MessageBoxButton.YesNo
                , MessageBoxImage.Question
                , MessageBoxResult.Yes) == MessageBoxResult.Yes)
            {
                Environment.Exit(0);//退出程序
            }
        }
    }
}
