# 在树莓派上运行 tauri 应用



tauri，是 Rust 编写的界面框架。使用 tauri 将前端应用在本地环境执行。

经测，tauri 可运行在树莓派环境中。



### 1、安装 node

![1691387680385](img/1691387680385.png)



并配置环境变量，指向 node项目的 bin目录。

安装 yarn

`npm install -g yarn`



### 2、安装 tauri 演示项目

`yarn create tauri-app`



### 3、编译项目

先 yarn 配置整个项目

`yarn`



再执行编译命令

`yarn tauri dev`



编译过程中，会报各种错误，主要就是库或依赖缺失。

```bash
sudo apt install libglib2.0-dev libgtk3-dev libsoup2.4-dev libjavascriptcoregtk-4.0-dev libwebkit2gtk-4.0-dev
```

装完依赖再执行编译命令

`yarn tauri dev`



这样就能运行起来 tauri demo 啦。

