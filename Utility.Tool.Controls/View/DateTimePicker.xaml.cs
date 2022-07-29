using System;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;

namespace Utility.Tool.Controls.View
{

    [ToolboxBitmap(typeof(DateTimePicker))]
    /// <summary>
    /// DateTimePicker.xaml 的交互逻辑
    /// </summary>    
    public partial class DateTimePicker : ContentControl, System.ComponentModel.INotifyPropertyChanged
    {
        /// <summary>
        /// 以当前时间初始化当前实例。
        /// </summary>
        public DateTimePicker() : this(DateTime.Now)
        {
        }

        /// <summary>
        /// 以指定时间初始化当前实例
        /// </summary>
        /// <param name="dateTime">指定的时间。</param>
        public DateTimePicker(DateTime dateTime)
        {
            this.InitializeComponent();
            this.DataContext = this;
            DateTime = dateTime;
        }

        #region 事件

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
        }


        /// <summary>
        /// 日历图标点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenSelectPopup()
        {
            if (popChioce.IsOpen)
            {
                popChioce.IsOpen = false;
            }
            TDateTimeView dtView = new TDateTimeView(DateTime);// TDateTimeView  构造函数传入日期时间
            dtView.DateTimeOK += (dateTimeStr) => //TDateTimeView 日期时间确定事件
            {
                DateTime = Convert.ToDateTime(dateTimeStr);
                popChioce.IsOpen = false;//TDateTimeView 所在pop  关闭
            };
            popChioce.Child = dtView;
            popChioce.IsOpen = true;
        }
        #endregion

        #region 属性

        /// <summary>
        /// 日期时间
        /// </summary>
        public DateTime DateTime
        {
            get => (DateTime)GetValue(DateTimeProperty);
            set
            {
                SetValue(DateTimeProperty, value);
                RaisePropertyChanged(nameof(DateTime));
            }
        }
        public static readonly DependencyProperty DateTimeProperty = DependencyProperty.Register(nameof(DateTime), typeof(DateTime), typeof(DateTimePicker));

        #endregion


        private void TextBlock_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenSelectPopup();
        }

        private void iconButton1_Click(object sender, RoutedEventArgs e)
        {
            OpenSelectPopup();
        }
    }
}
