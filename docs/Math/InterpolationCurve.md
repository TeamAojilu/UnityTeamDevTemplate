# InterpolationCurve

class

名前空間：SilCilSystem.Math

---

補間曲線として使用できるイージング関数を取得するためのクラスです。
シリアライズしてインスペクタ上で設定できるようにしています。

## メンバ一覧

### メソッド

|return|Signature|description|note|
|-|-|-|-|
|float|Evaluate(float t)|補間曲線を用いて値を計算|基本的には返り値はt=0で0, t=1で1|

※「基本的には」とついているのは例外があるからです。例えば、Backとついているものは1より大きい値を返すことがあります。

## 使用例

例えば、transform.positionを徐々に変化させて移動させるなどのアニメーションに利用できます。

```cs
using UnityEngine;
using System.Collections;
using SilCilSystem.Math;

public class MoveTest : MonoBehaviour
{
    [SerializeField] private Vector3 m_start = new Vector3(0f, 0f, 0f);
    [SerializeField] private Vector3 m_end = new Vector3(1f, 0f, 0f);
    [SerializeField] private InterpolationCurve m_curve = default;

    private IEnumerator Start()
    {
        transform.position = m_start;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime;
            transform.position = Vector3.LerpUnclamped(m_start, m_end, m_curve.Evaluate(t));
            yield return null;
        }

        transform.position = m_end;
    }
}
```

m_curveTypeを変えることでメリハリのあるアニメーションを設定できます。

![m_curveTypeの変更によるアニメーションの変化][fig:InterpolationCurveMove]

また、曲線をAnimationCurveで設定することもできます。
その場合はインスペクタのチェックボックスをONにします。

![AnimationCurveを使用して動きをカスタマイズする][fig:InterpolationCurveCustom]

## 実装

最初の呼び出しで関数を生成してキャッシュしています。

```cs
bool m_useCustomCurve;
CurveTypeDefinition.CurveType m_curveType;
AnimationCurve m_customCurve;

// キャッシュ用.
Func<float, float> m_curveFunc;

float Evaluate(float t)
{
    m_curveFunc = m_curveFunc ?? ((m_useCustomCurve) ? m_customCurve.Evaluate : m_curveType.GetCurve());
    return m_curveFunc(t);
}
```

既定の関数はCurveTypeDefinition.CurveType列挙型の拡張メソッドとして実装されています。
拡張メソッドではswitch構文で対応する関数を返しています。

```cs
public static Func<float, float> GetCurve(this CurveType type)
{
    switch (type)
    {
        default:
        // 1次
        case CurveType.Linear: return x => x;
        // 2次
        case CurveType.EaseInQuad: return x => x * x;
        // ...
    }
}
```

<!--- footer --->

{% include footer.md %}

<!--- 参照 --->

{% include paths.md %}

[fig:InterpolationCurveMove]: Figures/InterpolationCurveMove.gif
[fig:InterpolationCurveCustom]: Figures/InterpolationCurveCustom.gif
