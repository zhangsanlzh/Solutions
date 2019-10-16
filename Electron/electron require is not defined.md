# electron require is not defined

原因是在下面代码中对require进行了重命名。

```html
    <script>
      window.nodeRequire = require;
      delete window.require;
      delete window.exports;
      delete window.module;
    </script>

```

将所有需要用到require的地方换成nodeRequire即可。