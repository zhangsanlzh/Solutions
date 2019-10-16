#### C# WPF实现鼠标拖动的代码片

```csharp
///可表示实时拖动
void xxx_PreviewMouseLeftButtonUp(System.Object sender, System.Windows.Input.MouseButtonEventArgs e)
{
    IsMouseLeftBtnDown = false;
    IsMouseLeftBtnUp = true;
}

/// <summary>
/// 是否按下鼠标左键
/// </summary>
private bool IsMouseLeftBtnDown = false;
/// <summary>
/// 是否松开鼠标左键
/// </summary>
private bool IsMouseLeftBtnUp = true;
/// <summary>
/// 鼠标拖动期间 x轴偏移量
/// </summary>
private double xOffset = 0;
/// <summary>
/// 鼠标按下点在X轴的位置
/// </summary>
private double xDown = 0;
/// <summary>
/// 鼠标松开点在X轴的位置
/// </summary>
private double xUp = 0;

void xxx_PreviewMouseLeftButtonDown(System.Object sender, System.Windows.Input.MouseButtonEventArgs e)
{
    IsMouseLeftBtnDown = true;
    IsMouseLeftBtnUp = false;

	xDown = e.GetPosition(this).X;
}

void xxx_PreviewMouseMove(System.Object sender, System.Windows.Input.MouseEventArgs e)
{
    if (IsMouseLeftBtnDown && !IsMouseLeftBtnUp)
    {
        xUp = e.GetPosition(this).X;
        xOffset = xDown - xUp;
        xDown = xMove;//改变当前鼠标按下位置，这样曲线就可以随着鼠标移动方向而准确移动

        txTagName8.Text = xOffset.ToString();
    }
}


///鼠标弹起时发生操作，这样就不是实时的了
void CurvePnl_PreviewMouseLeftButtonUp(System.Object sender, System.Windows.Input.MouseButtonEventArgs e)
{
    IsMouseLeftBtnDown = false;
    IsMouseLeftBtnUp = true;
    
    xUp = e.GetPosition(this).X;
    xOffset = xDown - xUp;
    xDown = xMove;//改变当前鼠标按下位置，这样曲线就可以随着鼠标移动方向而准确移动

    txTagName8.Text = xOffset.ToString();
}

/// <summary>
/// 是否按下鼠标左键
/// </summary>
private bool IsMouseLeftBtnDown = false;
/// <summary>
/// 是否松开鼠标左键
/// </summary>
private bool IsMouseLeftBtnUp = true;
/// <summary>
/// 鼠标拖动期间 x轴偏移量
/// </summary>
private double xOffset = 0;
/// <summary>
/// 鼠标按下点在X轴的位置
/// </summary>
private double xDown = 0;
/// <summary>
/// 鼠标松开点在X轴的位置
/// </summary>
private double xUp = 0;

void CurvePnl_PreviewMouseLeftButtonDown(System.Object sender, System.Windows.Input.MouseButtonEventArgs e)
{
    IsMouseLeftBtnDown = true;
    IsMouseLeftBtnUp = false;

    xDown = e.GetPosition(this).X;
}

void CurvePnl_PreviewMouseMove(System.Object sender, System.Windows.Input.MouseEventArgs e)
{
    if (IsMouseLeftBtnDown && !IsMouseLeftBtnUp)
    {
        //这里没有操作
    }
}

```

以上就是鼠标拖动的两种刷新策略。