# CompositeDisposable

class

名前空間：SilCilSystem.Variables

継承：System.IDisposable

---

複数のIDisposableを1つにまとめるためのクラスです。

## メンバ一覧

### メソッド

|member|description|
|-|-|
|void Add(IDisposable item)|IDisposableを登録します。|
|void Remove(IDisposable item)|IDisoposableの登録を解除します。|
|void Dispose()|登録したIDisposableのDisposeメソッドを呼び出します。|

## 使用例

複数のイベントを登録する場合、登録する分だけ返り値のIDisposableを保持する必要があって面倒です。
例えば、イベントが2つある場合は以下のようになります。

```cs
using UnityEngine;
using SilCilSystem.Variables;
using System;

public class Test : MonoBehaviour
{
    [SerializeField] private GameEventIntListener m_event1 = default; // int型のイベント.
    [SerializeField] private GameEventListener m_event2 = default; // 引数なしのイベント.

    // 登録する分だけ、IDisposableが必要.
    private IDisposable m_disposable1 = default;
    private IDisposable m_disposable2 = default;

    private void Start()
    {
        m_disposable1 = m_event1.Subscribe(OnEvent1);
        m_disposable2 = m_event2.Subscribe(OnEvent2);
    }

    private void OnDestroy()
    {
        // 登録した分だけDisposeを呼ばないといけない.
        m_disposable1?.Dispose();
        m_disposable2?.Dispose();
    }

    private void OnEvent1(int value)
    {
        // m_event1の処理.
    }

    private void OnEvent2()
    {
        // m_event2の処理.
    }
}
```

CompositeDisposableを使えば、複数のIDisposableを1つのIDisposableとして扱うことが可能です。

```cs
using UnityEngine;
using SilCilSystem.Variables;
using System;

public class Test : MonoBehaviour
{
    [SerializeField] private GameEventIntListener m_event1 = default;
    [SerializeField] private GameEventListener m_event2 = default;

    // IDisposableは1つで済む.
    private IDisposable m_disposable = default;

    private void Start()
    {
        var disposable = new CompositeDisposable();
        disposable.Add(m_event1.Subscribe(OnEvent1));
        disposable.Add(m_event2.Subscribe(OnEvent2));
        m_disposable = disposable;
    }

    private void OnDestroy()
    {
        // Disposeが1回で済む.
        m_disposable?.Dispose();
    }

    private void OnEvent1(int value)
    {
        // m_event1の処理.
    }

    private void OnEvent2()
    {
        // m_event2の処理.
    }
}
```

## 実装

典型的なCompositeパターンです。

```cs
public class CompositeDisposable : IDisposable
{
    // 内部ではListで保持.
    private readonly List<IDisposable> m_disposables = new List<IDisposable>();

    public void Add(IDisposable item) => m_disposables.Add(item);
    public void Remove(IDisposable item) => m_disposables.Remove(item);

    public void Dispose()
    {
        // 全てのDisposeを呼ぶだけ.
        foreach(var disposable in m_disposables)
        {
            disposable?.Dispose();
        }
        m_disposables.Clear();
    }
}
```
