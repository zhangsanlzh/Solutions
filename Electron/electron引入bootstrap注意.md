# electron引入bootstrap注意

#### 引入方法

##### 方法1：

可通过`npm install bootstrap`引入，`bootstrap@3`或`bootstrap@4`下载对应版本的bootstrap。

##### 方法2：

到bootstrap官网下载对应压缩包，至于`node_modules`下。

引入时直接在html页面中引入

```html
<head>
    <meta charset="utf-8" />
    <script>
        window.nodeRequire = require;
        delete window.require;
        delete window.exports;
        delete window.module;
    </script>
    <script type="text/javascript" src="node_modules/jquery/dist/jquery.min.js"></script>   
    <script src="node_modules/bootstrap/dist/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="node_modules/bootstrap/dist/css/bootstrap.css">
    <title></title>
</head>
```

bootstrap依赖`jquery`。若不在引入`bootstrap.js`前先引入`jquery`，将导致许多效果失效。如`dropdown`不起作用等。