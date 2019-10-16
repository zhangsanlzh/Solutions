## C# 中字符串转换成日期

我们在处理字符串日期格式常用DateTime.Pares() 但是这个形式的转换是相当有限的，有些C#是会不懂你写入的日期格式的如20031231。那么类似 "20100101" 或者其它形式的字符串如何转换成日期型呢?

**一、拼接字符串的形式** 

```csharp
DateTime dt=Convert.ToDateTime("20100101".Substring(0,4)+"-"+"20100101".Substring(4,2)+"-"+"20071107".Substring(6,2));  
```

**二、Convert.ToDateTime(string)**

string格式有要求，必须是yyyy-MM-dd hh:mm:ss 

**三、Convert.ToDateTime(string, IFormatProvider)**

```csharp
DateTime dt;
```

```
DateTimeFormatInfo dtFormat = new System.GlobalizationDateTimeFormatInfo();
```

```csharp
dtFormat.ShortDatePattern = "yyyy/MM/dd";
```

```csharp
dt = Convert.ToDateTime("2011/05/26", dtFormat);
```

 

**四、DateTime.ParseExact()**

```csharp
string dateString = "20110526";
```

```csharp
DateTime dt = DateTime.ParseExact(dateString, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
```

```csharp
//或者
```

```csharp
DateTime dt = DateTime.ParseExact(dateString, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture);
```

 

**五、DateTime.TryParse(string,out datetime)** 

1、更多时候，会采用DateTime.TryParse(string,out datetime)方法，因为此方法有安全机制，当string内容不正确时，可以返回日期的最小值MinValue。并且可以通过返回的bool值判断转化是否成功。而DateTime.ParseExact()需要按特定的格式来转换，对格式的要求比较严，如果string中不是日期内容，而量类似“asdfasd”的字符串，则会出错。

2、用DateTime.TryParse(string,out datetime)转换后，得到的datetime可以用 datetime.ToString("ddd, MMM. dd")来转换为特殊需求的格式，比较灵活方便。