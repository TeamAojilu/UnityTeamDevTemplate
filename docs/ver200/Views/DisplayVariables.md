# DisplayVariables

class

名前空間：SilCilSystem.Views

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
|PropertyBool|IsBusy|更新処理があったときはtrue|数値のアニメーションが終わったタイミングを知りたいときなど|
|string|Format|変数の値を代入する箇所を指定|変数設定のm_keyを{}で囲む|

### 変数アセットの個別設定

#### 基本設定

|type|name|description|note|
|-|-|-|-|
|string|Key|全体設定のFormatで用いる識別用文字列|重複ないように設定|
|ReadonlyPropertyString|Format|変数の値の表示形式|ToStringの引数|
|VariableAsset|Variable|用いる変数アセット|Readonly|

Formatについては[C#のドキュメント][page:StringFormat]を参考にしてください。

#### アニメーション設定

##### 共通

|type|name|description|note|
|-|-|-|-|
|ReadonlyPropertyFloat|Duration|アニメーションの変化時間|0以下ならアニメーションなし|
|InterpolationCurve|Curve|補間曲線|イージングを用いたアニメーションに使用|
|bool|UseInitial|Start時に特定の値を用いる場合はtrueに設定||
|変数の型|InitialValue|Start時に用いる値|UseInitialがtrueの場合のみ使用|

##### int型

|type|name|description|note|
|-|-|-|-|
|FloatToInt.CastType|CastType|floatからintへの変換方法の指定|floatで補間してからintに変換するのに必要|

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
2. 全体設定のFormatに"score: {score}"を入力します。
3. IntValuesにスコアの変数アセットを設定します。
4. Keyには"score", Formatには"0"を入力します。

![DisplayVariablesで変数の値を表示する][fig:DisplayVariablesBasic]

実行すれば、変数アセットの値が表示されます。

アニメーションもさせてみましょう。
Durationに0.5を入力すれば0.5秒で変数の値に変わってくれます。

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
