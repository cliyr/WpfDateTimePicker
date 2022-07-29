using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Utility.Tool.Controls.View
{

    [System.ComponentModel.DesignTimeVisible(false)]//在工具箱中 隐藏该窗体 20170804 姜彦
    internal partial class IconButton : UserControl
    {
        public IconButton()
        {
            InitializeComponent();
        }

        #region 图标
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(nameof(Icon), typeof(string), typeof(IconButton), new PropertyMetadata(string.Empty, OnIconChanged));
        public string Icon
        {
            set { SetValue(IconProperty, value); }
            get { return (string)GetValue(IconProperty); }
        }
        private static void OnIconChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            IconButton btn = obj as IconButton;
            if (btn == null)
            {
                return;
            }
            btn.icon.Source = new BitmapImage(new Uri((string)args.NewValue, UriKind.Relative));
        }


        public static readonly DependencyProperty Base64IconProperty = DependencyProperty.Register(nameof(Base64Icon), typeof(string), typeof(IconButton), new PropertyMetadata(string.Empty, OnBase64IconChanged));
        public string Base64Icon
        {
            set { SetValue(Base64IconProperty, value); }
            get { return (string)GetValue(Base64IconProperty); }
        }
        private static void OnBase64IconChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            string value = (string)args.NewValue;
            string imagebase64 = value.Substring(value.IndexOf(",") + 1);
            byte[] streamBase = Convert.FromBase64String(imagebase64);
            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = new System.IO.MemoryStream(streamBase);
            bi.EndInit();

            IconButton btn = obj as IconButton;
            if (btn == null)
            {
                return;
            }
            btn.icon.Source = bi;
        }
        #endregion  

        #region 点击事件
        public static readonly RoutedEvent ClickEvent = EventManager.RegisterRoutedEvent("Click", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(IconButton));
        public event RoutedEventHandler Click
        {
            add { base.AddHandler(ClickEvent, value); }
            remove { base.RemoveHandler(ClickEvent, value); }
        }
        #endregion

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RoutedEventArgs newEvent = new RoutedEventArgs(IconButton.ClickEvent, this);
            this.RaiseEvent(newEvent);
        }
    }
}
