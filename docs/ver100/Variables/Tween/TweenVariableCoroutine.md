# Tweenコルーチン

拡張メソッド

名前空間：SilCilSystem.Varaiables

---

Tweenコルーチンは変数の値をある値から別の値へと時間変化させる拡張メソッドです。
1から10まで3秒間で値を増やしたり、赤から青に1秒で変化させたりできます。

## 対応している型

### Primitive型

- float
- int

### struct

- Vector2
- Vector2Int
- Vector3
- Vector3Int
- Quaternion
- Color

## 使用例

例えば、1から10まで3秒間で値を変化させる場合は以下のように書きます。

```cs
using UnityEngine;
using SilCilSystem.Variables;

public class TweenFloat : MonoBehaviour
{
    [SerializeField] private VariableFloat m_value = default;

    private void Start()
    {
        // コルーチンなので、StartCoroutineで呼ぶ.
        StartCoroutine(m_value.Tween(1f, 10f, 3f));
    }
}
```

コルーチンメソッドを作成し、yield returnすることで、変化が終わるまで待機することもできます。

```cs
using UnityEngine;
using System.Collections;
using SilCilSystem.Variables;
using SilCilSystem.Math; // イージング関数の指定で既に用意されているものを使用する場合は必要.

public class TweenFloat : MonoBehaviour
{
    [SerializeField] private VariableFloat m_value = default;

    private void Start()
    {
        StartCoroutine(MyCoroutine());
    }

    private IEnumerator MyCoroutine()
    {
        // 1から10まで3秒で変化させる.
        yield return m_value.Tween(1f, 10f, 3f);

        // 現在の値から100まで2秒で変化させる.
        yield return m_value.Tween(100f, 2f);

        // イージング関数を指定することも可能.
        yield return m_value.Tween(1f, 10f, 3f, CurveTypeDefinition.CurveType.EaseInOutQuad);

        // イージング関数はラムダ式でもOK.
        yield return m_value.Tween(1f, 10f, 3f, x => Mathf.Sqrt(x));
    }
}
```

## 使用上の注意点

個人的な意見ですが、この機能を多用することはないと思います。
Tween系が必要になるのはアニメーション機能といった表示/エフェクト関連であり、変数アセットが扱いやすい領域からは外れます。
ゲームのロジック部分を変数アセットで行い、演出関連は従来通りのコーディングでやるほうが変数アセットの量も減って管理しやすいのでは？と思います。
もちろん、使っていけないというわけではないです。
「こんな活用方法もあるよ」と教えていただけると嬉しいです。

## 実装

全てのTweenコルーチンは以下のGenericメソッドを呼び出しています。

```cs
internal static IEnumerator Tween<T>(this Variable<T> variable, T start, T end, float time, Func<T, T, float, T> lerp, Func<float, float> curve)
{
    float timer = 0f;
    while (timer < time)
    {
        timer += Time.deltaTime;
        variable.Value = lerp(start, end, curve(Mathf.Clamp01(timer / time)));
        yield return null;
    }
    variable.Value = lerp(start, end, curve(1f));
}
```

<!--- footer --->

{% include ver100/footer.md %}

<!--- 参照 --->

{% include ver100/paths.md %}
