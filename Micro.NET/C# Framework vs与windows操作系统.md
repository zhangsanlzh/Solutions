#### C#，Framework与visual studio比较

本文参考CSDN博文`Visual Studio各版本区别`,`https://blog.csdn.net/MYsce/article/details/70879943?locationNum=16&fps=1`，与`C#本质论 第4版 C#5.0`写成。感谢博主转载，感谢作者原创。

到2018年，C#已经到了6.0，framework为4.7，Visual Studio为2018，windows为10。

##### 一、vs2005，C#2.0，framework2.0

vs2005对应`C#2.0`，`framework 2.0`。05版的`visual studio`支持`winform`开发，`ASP.NET`网站开发，支持`C#`，`C++`，`Visual Basic`，`Visual J#` 等开发语言，但不支持换行转义；如sql语句

```c#
string sqlStr=@"select t.diagnosis_code
  				from pat_diagnosis t
 				where t.diagnosis_code = '97.26'";
```

这么写在vs2005中就是非法（执行报错）的，而在更高版本的vs中是合法（不报错）的。

2.0的`framework`不支持`linq`查询，不支持`WPF`开发。

##### 二、vs2008，C#3.0，framework3.5

`framework3.0`让开发者可以开发WPF应用程序，真正与IDE工具集成是在vs2008版本中。08版本的vs是功能相对全面完整的IDE工具，不仅支持WPF开发，还支持`LINQ`查询，`AJAX`交互。`framework3.5`兼容`framework2.0`、`framework3.0`。vs2008取消支持`J#`。

##### 三、vs2010，C#4.0，framework4.0

10版本的vs是非常经典的一个版本。最大的特点是支持`三屏一云`开发，即`PC屏，平板屏，手机屏，Windows Azure`。引入了新语言`F#`。最佳搭配系统是`win7`。这个版本的`c#`添加了对动态类型的支持，对多线程编程API进行了大幅改进。

##### 四、vs2012，c#5.0，framework4.5

vs2012中可以开发windows8应用。这个版本的c#支持调用异步方法，不需要显示注册委托回调。4.5的framework支持对WinRT(Windows Runtime)的互操作性。

##### 五、后续版本的vs，framework

vs2012后又陆续推出了vs2013，vs2015，vs2017，vs2018。framework也逐渐升级：framework4.5.1，framework4.5.2，framework4.5.3，framework4.6，framework4.6.1，framework4.6.2。而visual studio功能则更为全面:支持Git，支持Unity，支持跨平台移动开发 ，支持web 和云开发 ；交互性也更为良好。