# Electron渲染进程与主进程交互

某个Electron项目配置如下

```json
{
  "name": "nodejs-console-app1",
  "version": "0.0.0",
  "description": "NodejsConsoleApp1",
  "main": "app.js",
  "author": {
    "name": ""
 }
```

app.js规定的文件是可与主进程直接通信。项目其它任何位置都被视为在渲染进程内。主界面是Html页，其中不可避免地需要与主进程交互。例如，要实现窗体最小化，最大化，关闭窗体等功能，就需要触发Electron规定的一些事件，调用一些只有Electron库中规定的方法，以实现上述功能。可用`ipcMain`和`ipcRender`来在主渲进程间交互。



#### 一、渲染进程让主进程做事

```javascript
// Html页中的js

var ipcRenderer=nodeRequire('electron').ipcRenderer;
document.getElementById('self-min').onclick=function(){
    ipcRenderer.send('min-window')
}
document.getElementById('self-max').onclick=function(){
    var length=document.getElementsByClassName('glyphicon-resize-full').length
    if(length>0){
        ipcRenderer.send('max-window')
        document.getElementById('self-btn-max').className='glyphicon glyphicon-resize-small'
    }else{
        ipcRenderer.send('unmax-window')
        document.getElementById('self-btn-max').className='glyphicon glyphicon-resize-full'
    }        
}
document.getElementById('self-close').onclick=function(){
    ipcRenderer.send('quit-app')
}
```

```js
// app.js中的js

ipcMain.on('min-window', () => {
win.minimize()
})
ipcMain.on('max-window', () => {
win.maximize()
})
ipcMain.on('unmax-window', () => {
win.unmaximize()
})
ipcMain.on('quit-app', () => {
app.quit();
})
```

#### 二、主进程让渲染进程做事

```js
win.on('maximize',function(){
win.webContents.executeJavaScript(
"document.getElementById('self-btn-max').className='glyphicon glyphicon-resize-small'"
)
})
win.on('unmaximize',function(){
win.webContents.executeJavaScript(
"document.getElementById('self-btn-max').className='glyphicon glyphicon-resize-full'"
)
})
```

