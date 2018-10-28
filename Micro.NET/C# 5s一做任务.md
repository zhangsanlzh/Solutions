#### C# 5s一做任务

```c#
System.Timers.Timer _timer5s = new System.Timers.Timer();   //5s
private bool PrinterServerStatus;

_timer5s.Interval = 5000;

#region 每5s 触发的事件
_timer5s.Elapsed += (oo, ee) => {
string host = "192.168.10.10";

Ping p1 = new Ping();
PingReply reply = p1.Send(host); //发送主机名或Ip地址

if (reply.Status == IPStatus.Success)
PrinterServerStatus = true;
else if (reply.Status == IPStatus.TimedOut)
PrinterServerStatus = false;
};
#endregion

_timer5s.Start();

```

