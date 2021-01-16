# DelegateDispose

class

名前空間：SilCilSystem.Variables

継承：

- System.IDisposable
- [SilCilSystem.IPooledObject][page:ObjectPool]

Assembly：SilCilSystem

---

`Dispose`メソッドで呼ばれる処理を指定できる`IDisposable`です。

## メンバ一覧

### メソッド

|member|description|
|-|-|
|void Dispose()|登録したメソッドを呼び出す。|
|static IDisposable Create(Action action)|新しいDelegateDisposeを生成する|

## 使用例

[イベントオブジェクト][page:GameEvent]の登録解除に使用しています。

```cs
private event Action m_event;
public IDisposable Subscribe(Action action)
{
    // イベントを登録.
    m_event += action;
    // 解除用のIDisposableクラスを返す.
    return DelegateDispose.Create(() => m_event -= action);
}
```

## 実装

指定されたメソッドを変数として保持して`Dispose`で呼び出しています。

```cs
private Action m_delegate;

public void Dispose()
{
    m_delegate?.Invoke();
    m_delegate = null;
}
```

`m_delegate`への代入は`static`メソッドの`Create`で行っています。

```cs
public static IDisposable Create(Action action)
{
    var instance = m_pool.GetInstance();
    instance.m_delegate = action;
    return instance;
}
```

`new`が極力呼ばれないようにするために[オブジェクトプール][page:ObjectPool]を行っています。

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}
