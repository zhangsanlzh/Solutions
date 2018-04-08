#### CSS按钮背景渐变

要实现当鼠标放到按钮上时，背景色和字体色渐渐变成另一种颜色，离开时又变成另一种颜色。只需用transition属性就行了。

```css
.button{
    margin: 20px auto;
    line-height: 50px;
    width: 150px;
    height: 50px;
    display: block;
    text-decoration: none;
    font-weight: 100;
    font-size: 20px;
    border-radius: 5px;
    color: #B4B4B5;
    background: #333337;
    text-align: center;
    transition: all 900ms;
}

.button:hover{  
    background:#434346;
    color: #F0F0F0;
}
```

