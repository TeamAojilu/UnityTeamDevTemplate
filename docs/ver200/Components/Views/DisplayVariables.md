# DisplayVariables

class

名前空間：SilCilSystem.Components.Views

継承：UnityEngine.MonoBehaviour

---

変数アセットの値を表示するためのコンポーネントです。
実際の表示処理は[IDisplayTextインターフェース][page:IDisplayText]に委譲し、
このコンポーネントは表示形式の指定や表示する値のアニメーション設定を行います。

スコアやタイムの表示はこれ1つで十分だと言えるようなものを目指しています。

## 設定項目

### 全体設定

|type|name|description|note|
|-|-|-|-|
|PropertyBool|m_isBusy|更新処理があったときはtrue|数値のアニメーションが終わったタイミングを知りたいときなど|
|string|m_format|変数の値を代入する箇所を指定|変数設定のm_keyを{}で囲む|

### 変数アセットの個別設定

#### 基本設定

|type|name|description|note|
|-|-|-|-|
|string|m_key|全体設定のm_formatで用いる識別用文字列|重複ないように設定|
|ReadonlyPropertyString|m_format|変数の値の表示形式|ToStringの引数|
|ScriptableObject|m_variable|用いる変数アセット|ReadonlyでもVariableでも可、読取のみ使用|

m_formatについては[C#のドキュメント][page:StringFormat]を参考にしてください。

#### アニメーション設定

##### 共通

|type|name|description|note|
|-|-|-|-|
|ReadonlyPropertyFloat|m_duration|アニメーションの変化時間|0以下ならアニメーションなし|
|InterpolationCurve|m_curve|補間曲線|イージングを用いたアニメーションに使用|
|bool|m_useInitial|Start時に特定の値を用いる場合はtrueに設定||
|変数の型|m_initialValue|Start時に用いる値|m_useInitialがtrueの場合のみ使用|

##### int型

|type|name|description|note|
|-|-|-|-|
|FloatToInt.CastType|m_castType|floatからintへの変換方法の指定|floatで補間してからintに変換するのに必要|

## 使用例

例としてスコアの表示をしてみます。
ここでは、キーを押すとスコアが100上がることにします。

```cs
using UnityEngine;
using SilCilSystem.Variables;

public class TestScore : MonoBehaviour
{
    [SerializeField] private VariableInt m_score = default;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            // キーを押すたびに値を100増やす.
            m_score.Value += 100;
        }
    }
}
```

変数アセットを作成して設定します。

![変数アセットを設定する][fig:TestScore]

次に、スコアをDisplayVariablesを用いて表示してみましょう。

1. Textコンポーネント（TextMeshProでも可）と同じゲームオブジェクトにDisplayVariablesをアタッチして設定します。
2. 全体設定のm_formatに"score: {score}"を入力します。
3. m_intValuesにスコアの変数アセットを設定します。
4. m_keyには"score", m_formatには"0"を入力します。

![DisplayVariablesで変数の値を表示する][fig:DisplayVariablesBasic]

実行すれば、変数アセットの値が表示されます。

アニメーションもさせてみましょう。
m_durationに0.5を入力すれば0.5秒で変数の値に変わってくれます。

![アニメーションの設定をする][fig:AnimationSettings]

![スコアのアニメーション][fig:AnimationScore]

## 実装

基本的にはループを回して置換しているだけです。

```cs
private void SetText()
{
    string text = m_format;
    foreach (var variable in m_variables)
    {
        // ループを回して置換.
        text = text.Replace($"{{{variable.Key}}}", variable.Update()); // variable.UpdateでToStringを呼んでいる.
    }
    m_text.SetText(text);
}
```

テキストの表示については、[IDisplayTextインターフェース][page:IDisplayText]を取得して使用しています。

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}

[page:StringFormat]: https://docs.microsoft.com/ja-jp/dotnet/standard/base-types/custom-numeric-format-strings

[fig:TestScore]: Figures/TestScore.png
[fig:DisplayVariablesBasic]: Figures/DiaplayVariablesBasic.gif
[fig:AnimationSettings]: Figures/AnimationSettings.png
[fig:AnimationScore]: Figures/AnimationScore.gif
