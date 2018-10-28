#### C#ping命令

```c#
string host = "192.168.10.10";

Ping p1 = new Ping();
PingReply reply = p1.Send(host); //发送主机名或Ip地址

StringBuilder sbuilder;
if (reply.Status == IPStatus.Success)
{
    sbuilder = new StringBuilder();
    sbuilder.AppendLine(string.Format("Address: {0} ", reply.Address.ToString()));
    sbuilder.AppendLine(string.Format("RoundTrip time: {0} ", reply.RoundtripTime));
    sbuilder.AppendLine(string.Format("Time to live: {0} ", reply.Options.Ttl));
    sbuilder.AppendLine(string.Format("Don't fragment: {0} ", reply.Options.DontFragment));
    sbuilder.AppendLine(string.Format("Buffer size: {0} ", reply.Buffer.Length));
    MessageBox.Show(sbuilder.ToString());
}

else if (reply.Status == IPStatus.TimedOut)
{
	MessageBox.Show("超时");
}
else
{
	MessageBox.Show("失败");
}

```

