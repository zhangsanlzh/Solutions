#### JS切换图片

如此写：

```js
//js文件

//窗口加载
this.onload=function(){
    
    // 获取图片列表
    var imageList=document.getElementsByClassName('imageList')[0].getElementsByTagName('li');

    // 设置图片列表项的可见性
    function changeItemVisiable(index) {
        for (var i = 0; i < imageList.length; i++) {
            imageList[i].style.display = 'none';
        }
        imageList[index].style.display = 'list-item';
    }
    changeItemVisiable(0);//默认使第一项可见 

    //设置间隔为2s
    var timeInterval=setInterval(changeItem,2000);

    var count=0;//计数变量
    //改变图片列表项的可见性
    function changeItem(){
        count++;
        count=count%5;
        changeItemVisiable(count);
    }    
}
```

对应HTML文件

```html

<ul class="imageList" style="list-style: none;">
    <li>
        <img src="images/2018-04-18_093957.png" alt="image1" width="348px" height="200px">
    </li>
    <li>
        <img src="images/2018-04-18_094016.png" alt="image2" width="348px" height="200px">
    </li>
    <li>
        <img src="images/2018-04-18_094025.png" alt="image3" width="348px" height="200px">
    </li>
    <li>
    	<img src="images/2018-04-18_094036.png" alt="image4" width="348px" height="200px">
    </li>
    <li>
    	<img src="images/2018-04-18_094123.png" alt="image5" width="348px" height="200px"> 
    </li>
</ul>

```

