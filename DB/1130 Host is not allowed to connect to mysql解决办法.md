# 1130 Host is not allowed to connect to mysql解决办法

就是mysql user表中对应用户的host字段不是连接服务器的客户端的ip。简单说就是不允许这个ip连接到mysql服务器。

解决办法：host设为%，表示允许所有ip地址以此身份连接mysql。或者改为这个ip地址。