# DisposeOnSceneUnLoaded

拡張メソッド

名前空間：SilCilSystem.Variables

---

IDisposableをシーンのアンロード時に呼ぶようにする拡張メソッドです。
[イベントオブジェクト][page:GameEvent]の解除処理に利用できます。

## 使用例

シーンの読み込みで破棄されるようなゲームオブジェクトが行うイベントの解除を簡潔に書けるようになります。

```cs
using UnityEngine;
using SilCilSystem.Variables;

public class TestEventListener : MonoBehaviour
{
    [SerializeField] private GameEventListener m_event = default;

    private void Start()
    {
        // シーンと同時に破棄されるスクリプトなら、OnDestroyメソッドでDisposeを書く必要がなくなります.
        m_event.Subscribe(() => Debug.Log("Hello")).DisposeOnSceneUnLoaded();
    }
}
```

## 使用上の注意点

マルチシーンやDontDestroyOnLoadしているゲームオブジェクトに使ってしまうと、
予期せぬタイミングでイベントが解除されるため注意です。

## 実装

RuntimeInitializeOnLoadMethodを用いてコールバックを登録して実現しています。

```cs
private static List<IDisposable> _Disposables = new List<IDisposable>(); 

public static void DisposeOnSceneUnLoaded(this IDisposable disposable)
{
    _Disposables.Add(disposable);
}

[RuntimeInitializeOnLoadMethod]
private static void RegisterCallback()
{
    SceneManager.sceneUnloaded += OnSceneUnloaded;
}

private static void OnSceneUnloaded(Scene arg0)
{
    foreach (var dispose in _Disposables)
    {
        dispose?.Dispose();
    }
    _Disposables.Clear();
}
```

<!--- footer --->

{% include footer.md %}

<!--- 参照 --->

{% include paths.md %}
