#### C#`using`语句用例一则

开发中想要能够多次打开相同资源而不报错，这就需要每次用完这个资源后立即释放。于是就是用了`using`语句。这样用：

```csharp
BinaryReader br = new BinaryReader(File.Open(vFileName, FileMode.Open));

string faultPointLocation = br.ReadByte().ToString();//故障点位置
string temp = br.ReadByte().ToString();
string cyCleNum = br.ReadByte().ToString();//循环次数

using (br) {/*用来释放文件对象,这样就可以重复打开相同文件 */}

```

