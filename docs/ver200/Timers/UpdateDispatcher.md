# UpdateDispatcher

class

名前空間：SilCilSystem.Timers

継承：[SingletonMonoBehaviour\<T>][page:SingletonMonoBehaviour]

Assembly：SilCilSystem.Packages

---

Update, LateUpdate, FixedUpdateの処理を行うシングルトンです。
これを用いることでMonoBehaviour以外からもUpdateに準ずる処理を実装できます。

また、[Unityの公式ブログ][page:UnityBlog]でも言われているように、Updateの処理は別々に実行するよりも
1つのMonoBehaviourでまとめて実行するほうがパフォーマンスが出ます。
そういった用途にも利用できます。

## 使用例

Registerメソッドで処理を登録します。
以下は引数の一覧です。

|type|name|description|
|-|-|-|
|Func<float, bool>|update|更新処理を行うメソッドを指定します。floatはdeltaTimeが渡されます。falseを返せば処理を終了します。|
|UnityEngine.Object|lifeTimeObject|nullになった場合、登録を解除します。|
|[UpdateMode][page:UpdateMode]|updateMode|更新のタイミングを指定します。規定値はDeltaTimeです。|

例えば、FPSを測定するスクリプトは以下のようになります。

```cs
using UnityEngine;
using SilCilSystem.Timers;

public class TestFPS : MonoBehaviour
{
    private int m_count = 0;
    private float m_time = 0f;

    private void Start()
    {
        // タイムスケールで変わってほしくないので、UpdateMode.UnscaledDeltaTimeを指定.
        UpdateDispatcher.Register(MicroUpdate, gameObject, UpdateMode.UnscaledDeltaTime);
    }

    private bool MicroUpdate(float deltaTime)
    {
        m_count++;
        m_time += deltaTime;

        // 1秒ごとにFPSを計算する.
        if (m_time > 1f)
        {
            Debug.Log($"FPS: {m_count / m_time}");
            m_count = 0;
            m_time = 0f;
        }

        return true;
    }
}
```

## 実装

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}

[page:UnityBlog]: https://blogs.unity3d.com/jp/2015/12/23/1k-update-calls/
