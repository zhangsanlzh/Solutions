#### C#Drawing.Color转Media.Color

需如此转换

```csharp
SolidColorBrush sldColor = new SolidColorBrush(System.Windows.Media.Color.FromArgb(dcolor.A,dcolor.R, dcolor.G, dcolor.B));

```

