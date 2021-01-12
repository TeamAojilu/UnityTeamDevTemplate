# DisposeOnDestroy

拡張メソッド

名前空間：SilCilSystem.Variables

---

ゲームオブジェクトが破棄されるタイミングでIDisposable.Disposeを呼ぶようにする拡張メソッドです。
[イベントオブジェクト][page:GameEvent]の解除処理に利用できます。

## 使用例

ゲームオブジェクトが行うイベントの解除を簡潔に書けるようになります。

```cs
using UnityEngine;
using SilCilSystem.Variables;

public class TestEventListener : MonoBehaviour
{
    [SerializeField] private GameEventListener m_event = default;

    private void Start()
    {
        // OnDestroyメソッドでDisposeを書く必要がなくなります.
        m_event.Subscribe(() => Debug.Log("Hello")).DisposeOnDestroy(gameObject);
    }
}
```

## 実装

Disoposeを呼ぶためのコンポーネントを指定されたゲームオブジェクトにAddComponentすることで実現しています。
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

{% include ver100/footer.md %}

<!--- 参照 --->

{% include ver100/paths.md %}
