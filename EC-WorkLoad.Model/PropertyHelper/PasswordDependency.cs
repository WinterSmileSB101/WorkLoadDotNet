using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EC_WorkLoad.Model.PropertyHelper
{
    public class PasswordDependency
    {
        static bool isInistialised = false;

        #region 字段定义
        public static string GetHintText(DependencyObject obj)
        {
            return ((string)obj.GetValue(HintTextProperty)) ?? string.Empty;
        }

        public static void SetHintText(DependencyObject obj, string value)
        {
            obj.SetValue(HintTextProperty, value);
        }

        public static readonly DependencyProperty HintTextProperty =
            DependencyProperty.RegisterAttached("HintText",
                typeof(string), typeof(PasswordDependency), new PropertyMetadata(null, HintTextChanged));

        public static bool GetShowHintText(DependencyObject obj) {
            return (bool)obj.GetValue(ShowHintTextProperty);
        }

        public static void SetShowHintText(DependencyObject obj, bool value) {
            obj.SetValue(ShowHintTextProperty, value);
        }

        public static readonly DependencyProperty ShowHintTextProperty =
            DependencyProperty.RegisterAttached("ShowHintText",
            typeof(bool), typeof(PasswordDependency), new PropertyMetadata(false));
        #endregion

        /// <summary>
        /// 值变化事件
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="e"></param>
        static void HintTextChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e) {
            var pwd = obj as PasswordBox;
            CheckShowHintText(pwd);
            if (!isInistialised)
            {
                pwd.PasswordChanged += new RoutedEventHandler(Pwd_PasswordChanged);
                pwd.Unloaded += new RoutedEventHandler(Pwd_Unloaded);
                isInistialised = true;
            }
        }

        static void Pwd_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var pwd = sender as PasswordBox;
            CheckShowHintText(pwd);
        }

        /// <summary>
        /// 卸载事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Pwd_Unloaded(object sender, RoutedEventArgs e)
        {
            var pwd = sender as PasswordBox;
            pwd.PasswordChanged -= new RoutedEventHandler(Pwd_PasswordChanged);
        }

        private static void CheckShowHintText(PasswordBox pwd)
        {
            pwd.SetValue(ShowHintTextProperty, pwd.Password == string.Empty);
        }
    }
}
