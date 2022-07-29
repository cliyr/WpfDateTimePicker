# WpfDateTimePicker
修改自：https://github.com/jiangyan219/DateTimePicker

原文链接地址：https://www.cnblogs.com/jiangyan219/articles/7284931.html


下图是控件的运行情况，图片来自原作者，新版控件仅在此基础上做了细节更改，如有侵权请联系删除：

![751382-20170804133555553-1341989978](https://user-images.githubusercontent.com/5352553/181678865-7f131191-9f1b-4edc-bfa0-f19577e8ea00.gif)


本库在原作基础上进行修改，对原有代码进行了优化以减少代码行数和冗余类、增加可读性，并新增如下功能：


一、ValueSelectPopup


![image](https://user-images.githubusercontent.com/5352553/181679434-5ded05f5-5555-45ef-9f46-d1f0571e8bdf.png)


1、该控件主要作用是可以使用鼠标点击值并返回

2、使用了Popup进行了包装方便调用

3、并可以设置可选的最小值、最大值、步进值、每行数据量，可以方便的进行选择和调试。

4、支持纯XAML， 不需要写C#代码

5、缺点就是值选择界面未做大的改动，仍然使用DataGrid实现，效率相对较低，优点就是可以明显减少代码量且可读性较高


二、IconButton

1、支持设置base64图片字符串为图像源的按钮控件，方便代码快速移植

2、支持纯XAML编写

3、截图和控件演示中的"x"按钮等即基于此编写

