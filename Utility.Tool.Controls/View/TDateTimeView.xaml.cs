using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Controls.Primitives;
using System.ComponentModel;

namespace Utility.Tool.Controls.View
{
    //在工具箱中隐藏该窗体
    [System.ComponentModel.DesignTimeVisible(false)]
    /// <summary>
    /// TDateTime.xaml 的交互逻辑
    /// </summary>
    internal partial class TDateTimeView : UserControl, System.ComponentModel.INotifyPropertyChanged
    {
        readonly ValueSelectPopup _hourPopup;
        readonly ValueSelectPopup _minPopup;
        readonly ValueSelectPopup _secPopup;
        public TDateTimeView(DateTime dateTime)
        {
            InitializeComponent();
            DateTime = dateTime;
            this.DataContext = this;

            _hourPopup = new ValueSelectPopup(0, 23, 1d, 6, "小时")
            {
                PlacementTarget = girdChioce,
                PopupAnimation = PopupAnimation.Fade,
                AllowsTransparency = true,
                Placement = PlacementMode.Top,
                StaysOpen = false
            };
            _hourPopup.SelectedValueEvent += (val) => { Hour = val; _hourPopup.IsOpen = false; };

            _minPopup = new ValueSelectPopup(0, 59, 1d, 10, "分钟")
            {
                PlacementTarget = girdChioce,
                PopupAnimation = PopupAnimation.Fade,
                AllowsTransparency = true,
                Placement = PlacementMode.Top,
                StaysOpen = false
            };
            _minPopup.SelectedValueEvent += (val) => { Minute = val; _minPopup.IsOpen = false; };

            _secPopup = new ValueSelectPopup(0, 59, 1d, 10, "秒钟")
            {
                PlacementTarget = girdChioce,
                PopupAnimation = PopupAnimation.Fade,
                AllowsTransparency = true,
                Placement = PlacementMode.Top,
                StaysOpen = false
            };
            _secPopup.SelectedValueEvent += (val) => { Second = val; _secPopup.IsOpen = false; };
        }



        DateTime _dateTime;
        public DateTime DateTime
        {
            get => _dateTime;
            set
            {
                _dateTime = value;
                Hour = value.Hour;
                Minute = value.Minute;
                Second = value.Second;

                calDate.SelectedDate = value;
                calDate.DisplayDate = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #region 事件

        /// <summary>
        /// 关闭按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iBtnCloseView_Click(object sender, RoutedEventArgs e)
        {
            OnDateTimeContent(this.DateTime);
        }

        /// <summary>
        /// 确定按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            DateTime dtCal = calDate.SelectedDate ?? DateTime.Now.Date;
            OnDateTimeContent(new DateTime(dtCal.Year, dtCal.Month, dtCal.Day, Hour, Minute, Second));
        }


        private int _hour = 0;
        public int Hour
        {
            get => _hour;
            set
            {
                _hour = value;
                RaisePropertyChanged(nameof(Hour));
            }
        }

        private int _minute = 0;
        public int Minute
        {
            get => _minute;
            set
            {
                _minute = value;
                RaisePropertyChanged(nameof(Minute));
            }
        }

        private int _second = 0;
        public int Second
        {
            get => _second;
            set
            {
                _second = value;
                RaisePropertyChanged(nameof(Second));
            }
        }



        /// <summary>
        /// 当前按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNow_Click(object sender, RoutedEventArgs e)
        {
            popChioce.IsOpen = false;//THourView 或 TMinSexView 所在pop 的关闭动作
            Button btn = sender as Button;

            if (btn.Content.ToString() == "零点")
            {
                Hour = 0;
                Minute = 0;
                Second = 0;
                btn.Content = "当前";
                btn.Background = System.Windows.Media.Brushes.LightBlue;
            }
            else
            {
                DateTime dt = DateTime.Now;
                Hour = dt.Hour;
                Minute = dt.Minute;
                Second = dt.Second;
                calDate.SelectedDate = calDate.DisplayDate = dt;
                btn.Content = "零点";
                btn.Background = System.Windows.Media.Brushes.LightGreen;
            }
        }
        private void CloseAllPopup()
        {
            if (_hourPopup.IsOpen)
            {
                _hourPopup.IsOpen = false;
            }
            if (_minPopup.IsOpen)
            {
                _minPopup.IsOpen = false;
            }
            if (_secPopup.IsOpen)
            {
                _secPopup.IsOpen = false;
            }
        }

        /// <summary>
        /// 小时点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnhh_Click(object sender, RoutedEventArgs e)
        {
            CloseAllPopup();
            _hourPopup.IsOpen = true;
        }

        /// <summary>
        /// 分钟点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnmm_Click(object sender, RoutedEventArgs e)
        {
            CloseAllPopup();
            _minPopup.IsOpen = true;
        }

       
        /// <summary>
        /// 秒钟点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnss_Click(object sender, RoutedEventArgs e)
        {
            CloseAllPopup();
            _secPopup.IsOpen = true;
        }

        /// <summary>
        /// 解除calendar点击后 影响其他按钮首次点击无效的问题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void calDate_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Mouse.Captured is CalendarItem)
            {
                _ = Mouse.Capture(null);
            }
        }
        #endregion

        #region Action交互

        /// <summary>
        /// 时间确定后的传递事件
        /// </summary>
        public Action<DateTime> DateTimeOK;


        /// <summary>
        /// 时间确定后传递的时间内容
        /// </summary>
        /// <param name="dateTimeStr"></param>
        protected void OnDateTimeContent(DateTime dt)
        {
            DateTimeOK?.Invoke(dt);
        }

        #endregion


    }
}
