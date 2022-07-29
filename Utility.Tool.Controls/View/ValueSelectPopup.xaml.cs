using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Utility.Tool.Controls.View
{
    /// <summary>
    /// TMinSexView.xaml 的交互逻辑
    /// </summary>
    public partial class ValueSelectPopup : System.Windows.Controls.Primitives.Popup
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="defaultValue">默认值。</param>
        /// <param name="title">窗口标题</param>
        /// <param name="min">用于选择的最小值</param>
        /// <param name="max">用于选择的最大值</param>
        /// <param name="numofline">即每行多少个数据</param>
        public ValueSelectPopup(int min, int max, double step, int numofline, string title = "选择")
        {
            InitializeComponent();
            //加载值
            MinValue = min;
            MaxValue = max;
            Step = step;
            NumOfLine = numofline;
            _initilizated = true;
            UpdateDataGrid(true);
            //更新标题
            Title = title;
        }
        private readonly bool _initilizated = false;

        private void UpdateDataGrid(bool throwIfErr)
        {
            if (!_initilizated)
            {
                return;
            }

            dgMinSex.ItemsSource = null;

            double max = MaxValue, min = MinValue, step = Step;
            int numofline = NumOfLine;
            if (numofline < 1)
            {
                throw new ArgumentException("每行个数不能小于1。", nameof(NumOfLine));
            }
            if (min >= max)
            {
                if (throwIfErr)
                {
                    throw new ArgumentException("最小值不能大于等于最大值。", nameof(MinValue));
                }
                else
                {
                    return;
                }
            }
            if (step <= 0)
            {
                if (throwIfErr)
                {
                    throw new ArgumentException("步进值不能小于等于0。", nameof(Step));
                }
                else
                {
                    return;
                }
            }
            double diff = max - min;
            if (step > diff)
            {
                if (throwIfErr)
                {
                    throw new ArgumentException("步进值不能大于最大值和最小值之差。", nameof(Step));
                }
                else
                {
                    return;
                }
            }

            //加载数据到显示列表
            DataTable dt = new DataTable();
            //计算行数
            int rowCount = (int)Math.Ceiling(diff / step / numofline);
            for (int i = 0; i < numofline; i++)
            {
                dt.Columns.Add();
            }
            double currValue = min;
            for (int i = 0; i < rowCount; i++)
            {
                //新增一行
                dt.Rows.Add();
                for (int j = 0; j < numofline; j++)
                {
                    //写入第i行j列的数据
                    dt.Rows[i][j] = currValue;
                    //步进一次值
                    currValue += step;
                    //确认是否已经完成添加
                    if (currValue > max)
                    {
                        break;
                    }
                }
            }
            dgMinSex.ItemsSource = dt.DefaultView;
        }
        private static void UpdateCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ValueSelectPopup).UpdateDataGrid(false);
        }
        /// <summary>
        /// 每一行可选择项的数量。
        /// </summary>
        public int NumOfLine
        {
            get { return (int)GetValue(NumOfLineProperty); }
            set { SetValue(NumOfLineProperty, value); }
        }
        public static readonly DependencyProperty NumOfLineProperty = DependencyProperty.Register("NumOfLine", typeof(int), typeof(ValueSelectPopup), new PropertyMetadata(1, UpdateCallBack));


        /// <summary>
        /// 值的步进大小。
        /// </summary>
        public double Step
        {
            get { return (double)GetValue(StepProperty); }
            set { SetValue(StepProperty, value); }
        }
        public static readonly DependencyProperty StepProperty = DependencyProperty.Register("Step", typeof(double), typeof(ValueSelectPopup), new PropertyMetadata(0d, UpdateCallBack));



        /// <summary>
        /// 可选择的最大值。
        /// </summary>
        public double MaxValue
        {
            get { return (double)GetValue(MaxValueProperty); }
            set { SetValue(MaxValueProperty, value); }
        }

        public static readonly DependencyProperty MaxValueProperty = DependencyProperty.Register("MaxValue", typeof(double), typeof(ValueSelectPopup), new PropertyMetadata(1d, UpdateCallBack));


        /// <summary>
        /// 可选择的最小值。
        /// </summary>
        public double MinValue
        {
            get { return (double)GetValue(MinValueProperty); }
            set { SetValue(MinValueProperty, value); }
        }

        public static readonly DependencyProperty MinValueProperty = DependencyProperty.Register("MinValue", typeof(double), typeof(ValueSelectPopup), new PropertyMetadata(0d, UpdateCallBack));


        /// <summary>
        /// 窗口标题
        /// </summary>
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(ValueSelectPopup), new PropertyMetadata("选择", (d, ee) =>
        {
            (d as ValueSelectPopup).textBlockTitle.Text = (string)ee.NewValue;
        }));



        /// <summary>
        /// 当前选择的值，未选择时为<see cref="int.MinValue"/>
        /// </summary>
        public int Value
        {
            get { return (int)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(int), typeof(ValueSelectPopup), new PropertyMetadata(int.MinValue));



        /// <summary>
        /// dgMinSex控件 单元格点击（选择）事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DgMinSec_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataGridCellInfo cell = dgMinSex.CurrentCell;
            if (cell.Column == null)
            {
                return;
            }

            DataRowView min = cell.Item as DataRowView;

            if (int.TryParse((string)min[cell.Column.DisplayIndex], out var value))
            {
                OnMinClickContent(value);
            }
        }

        /// <summary>
        /// 窗体关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void iBtnCloseView_Click(object sender, RoutedEventArgs e)
        {
            this.IsOpen = false;
        }


        /// <summary>
        /// 分钟数据点击（确定）后 的传递事件
        /// </summary>
        public Action<int> SelectedValueEvent;

        /// <summary>
        /// 分钟数据点击（确定）后 传递的时间内容
        /// </summary>
        /// <param name="minStr"></param>
        protected void OnMinClickContent(int minStr)
        {
            Value = minStr;
            SelectedValueEvent?.Invoke(minStr);
        }
    }
}
