#### C# 如何得到XML文件中指定的节点属性值

xml文档如下：

```xml
<?xml version="1.0" encoding="utf-8" ?>
<Root>
  <FuncList type="Circle">
    <Item>中点画圆算法</Item>
    <Item>中点椭圆算法</Item>
  </FuncList>
  <FuncList type="Square">
    <Item>正方形生成算法</Item>
    <Item>长方形生成算法</Item>
  </FuncList>
  <FuncList type="StraitLine">
    <Item>直线生成算法</Item>
  </FuncList>
  <FuncList type="Curve">
    <Item>曲线生成算法</Item>
  </FuncList>
  <FuncList type="Character">
    <Item>英文字符生成算法</Item>
    <Item>汉字生成算法</Item>
    <Item>特殊字符生成算法</Item>
  </FuncList>
  <FuncList type="Transform">
    <Item>平移算法</Item>
    <Item>旋转算法</Item>
  </FuncList>
  <FuncList type="Setting">
    <Item>线宽设置</Item>
    <Item>线型设置</Item>
    <Item>线颜色设置</Item>
  </FuncList>
</Root>
```

这样获取

```c#
var x=n.Attributes["type"].Value;
```

