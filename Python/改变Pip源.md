# 改变Pip源

任意一个位置（保证系统能找到这个路径下的ini文件，环境变量）建文件`pip.ini`。添加如下配置：

```ini
[global]
index-url = https://pypi.com/simple/
[install]
trusted-host=https://pypi.com
```

配置好后`pip install xxx`时将会从`https://pypi.com`下载所需包和依赖。当然也可配置成其它源。