#### 完全卸载oracle11g步骤：

1、 开始－＞设置－＞控制面板－＞管理工具－＞服务 停止所有Oracle服务。 

2、 开始－＞程序－＞Oracle - OraHome81－＞Oracle Installation Products－＞ Universal Installer，单击“卸载产品”-“全部展开”，选中除“OraDb11g_home1”外的全部目录，删除。 

5、 运行regedit，选择HKEY_LOCAL_MACHINE\SOFTWARE\ORACLE，按del键删除这个入口。 

6、 运行regedit，选择HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services，滚动这个列表，删除所有Oracle入口(以oracle或OraWeb开头的键)。 

7、 运行refedit，HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Eventlog\Application，删除所有Oracle入口。 

8、 删除HKEY_CLASSES_ROOT目录下所有以Ora、Oracle、Orcl或EnumOra为前缀的键。 

9、 删除HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\MenuOrder\Start Menu\Programs中所有以oracle开头的键。

 10、删除HKEY_LOCAL_MACHINE\SOFTWARE\ODBC\ODBCINST.INI中除Microsoft ODBC for Oracle注册表键以外的所有含有Oracle的键。

11、我的电脑-->属性-->高级-->环境变量,删除环境变量CLASSPATH和PATH中有关Oracle的设定。 12、从桌面上、STARTUP（启动）组、程序菜单中，删除所有有关Oracle的组和图标。 

13、删除所有与Oracle相关的目录(如果删不掉，重启计算机后再删就可以了)包括：    

1.C:\Program file\Oracle目录。     

2.ORACLE_BASE目录(oracle的安装目录)。     			  3.C:\WINDOWS\system32\config\systemprofile\Oracle目录。     

4.C:\Users\Administrator\Oracle或C:\Documents and Settings\Administrator\Oracle目录。     5.C:\WINDOWS下删除以下文件ORACLE.INI、oradim73.INI、oradim80.INI、oraodbc.ini等等。     6.C:\WINDOWS下的WIN.INI文件中若有[ORACLE]的标记段，删除该段。 



14、如有必要，删除所有Oracle相关的ODBC的DSN 

15、到事件查看器中，删除Oracle相关的日志 说明： 如果有个别DLL文件无法删除的情况，则不用理会，重新启动，开始新的安装，安装时，选择一个新的目录，则，安装完毕并重新启动后，老的目录及文件就可以删除掉了。 