#### CSS禁用滚动条但仍可滚动

这样做：

```css
*{
    margin: 0;
    padding: 0;
    /* border: 1px solid red; */
    /* box-sizing: border-box; */
}

html{
    overflow: hidden;
}

body{/*滚动条将显示在body区域*/
    overflow-y: scroll;/*按需要显示*/
    overflow-x: hidden;/*始终不显示x方向的滚动条*/
    width: calc(100vw + 20px);/*设body宽为比视窗宽还要宽20像素，这样就可以把滚动条隐藏在视窗外*/
}
```

