# DisposeOnDestroy

拡張メソッド

名前空間：SilCilSystem.Variables

Assembly：SilCilSystem

---

ゲームオブジェクトが破棄されるタイミングでIDisposable.Disposeを呼ぶようにする拡張メソッドです。
[イベントアセット][page:GameEvent]の解除処理に利用できます。

## 使用例

ゲームオブジェクトが行うイベントの解除を簡潔に書けるようになります。

```cs
using UnityEngine;
using SilCilSystem.Variables;

public class TestDisposeOnDestroy : MonoBehaviour
{
    [SerializeField] private GameEventListener m_event = default;
    [SerializeField] private GameEventIntListener m_eventInt = default;

    private void Start()
    {
        // OnDestroyメソッドでDisposeを書く必要がなくなります.
        m_event?.Subscribe(() => Debug.Log("Hello")).DisposeOnDestroy(gameObject);

        // Subscribeの第2引数にGameObjectを指定することもできます.
        m_eventInt?.Subscribe(i => Debug.Log(i), gameObject);
    }
}
```

## 実装

Disoposeを呼ぶためのコンポーネントを指定されたゲームオブジェクトにAddComponentすることで実現しています。
イベント1つ1つに対してそれぞれコンポーネントを追加するのはパフォーマンス的にもよろしくないので、
[CompositeDisposable][page:CompositeDisposable]を使用して複数のIDisposableを1つにまとめています。
アタッチする用のコンポーネントは以下のようになっています。

```cs
internal class DisposeOnDestroyCaller : MonoBehaviour
{
    private CompositeDisposable m_disposable = new CompositeDisposable();

    private void OnDestroy()
    {
        m_disposable?.Dispose();
    }

    internal void Set(IDisposable disposable)
    {
        m_disposable.Add(disposable);
    }
}
```

これを拡張メソッドで生成/取得しています。

```cs
public static void DisposeOnDestroy(this IDisposable disposable, GameObject gameObject)
{
    if (gameObject == null) return;

    if(gameObject.TryGetComponent(out DisposeOnDestroyCaller caller))
    {
        caller.Set(disposable);
    }
    else
    {
        gameObject.AddComponent<DisposeOnDestroyCaller>().Set(disposable);
    }
}
```

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}
