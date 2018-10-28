#### C#`dataGridView` `Remove()`不会清除`dataGridView`行

想要清除`dataGridView`？循环移除行是不行的：

```c#
foreach (DataGridViewRow row in dataGridView.Rows)
{
	dataGridView.Rows.Remove(row);
}
或
for (int i = 0; i < dataGridView.Rows.Count; i++)
{
    dataGridView.Rows.RemoveAt(i);
}
```

已有项的`dataGridView`不会删除这些项，执行到最后项仍存在，即使`dataGridView`的Row Count数的确在减小。这算是Bug吧?

完全清除需要这么做：

```csharp
dataGridView.Rows.Clear();
```

