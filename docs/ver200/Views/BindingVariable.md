# 変数アセットをコンポーネントにバインドする

class

名前空間：SilCilSystem.Views

継承：UnityEngine.MonoBehaviour

---

[変数アセット][page:Variable]の値をuGUIのUI要素やAnimatorのパラメータに設定することができます。（以下、バインドと表記）
ToggleのON/OFFやSliderのValueをバインドすることでUIとゲームロジックの連携が可能です。

## クラス一覧

### バインドする対象が1つのもの

|対象のコンポーネント|name|description|
|-|-|-|
|Animator|BindingAnimator|AnimatorControllerのパラメータへのバインド、イベントアセットによるSetTriggerも可能|
|CanvasGroup|BindingCanvasGroup|Alphaパラメータはアニメーション可能|
|Slider|BindingSlider|Valueパラメータはアニメーション可能|
|Toggle|BindingToggle||
|InputField/TMP_InputField|BindingInputField||
|Dropdown/TMP_Dropdown|BindingDropdown||

※Text/TextMeshProに関しては[DisplayVariables][page:DisplayVariables]が使用可能です。

※BindingAnimatorに関しては[PublishOnState][page:PublishOnState]と組み合わせることで自由度がさらに広がります。

### バインドする対象を複数設定できるもの

|対象|name|description|
|-|-|-|
|Selectable|BindingSelectables|ButtonやToggleなどのinteractableや色設定が可能|
|Image, Material, Textなどの色|BindingColors|アニメーション可能|

## 設定項目

バインドできる項目はクラスごとに異なるので割愛します。
ここでは多くのクラスで利用されている設定を説明します。

### 更新タイミング

|type|name|description|note|
|-|-|-|-|
|bool|m_setOnStart|trueならStartメソッドで値を設定||
|bool|m_setOnUpdate|trueならUpdateメソッドで値を更新|クラスによっては強制でON|
|ReadonlyPropertyBool|m_setValueWithoutNotify|trueならSetValueWithoutNotifyメソッドを使用して値を設定||

### アニメーション

|type|name|description|note|
|-|-|-|-|
|PropertyBool|m_isBusy|アニメーション中ならtrueになる||
|ReadonlyPropertyFloat|m_duration|アニメーションの長さ|0以下ならアニメーション無し|
|InterpolationCurve|m_curve|アニメーションの補間曲線||

## 使用例

### Sliderに変数アセットをバインドする

変数アセットをSliderのValueにバインドすることで、HPゲージなどに利用できます。

試しに、Z/Xキーで値を増減させるスクリプトを書いてみます。

```cs
using UnityEngine;
using SilCilSystem.Variables;

public class TestSlider : MonoBehaviour
{
    [SerializeField] private VariableFloat m_value = default;
    [SerializeField] private float m_increase = 0.1f;

    private void Start()
    {
        m_value.Value = 0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            m_value.Value += m_increase;
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            m_value.Value -= m_increase;
        }
    }
}
```

float型の変数アセットを作成してm_valueに設定します。

![TestSliderの設定][fig:TestSliderComponent]

Sliderを作成します。
簡単のために、Interactableはfalseにしてプレイヤーがスライダーを動かせないようにします。
BindingSliderをアタッチして、m_valueに先ほど作成したものと同じ変数アセットを設定すれば機能します。

![BindingSliderを設定する][fig:BindingSlider]

アニメーションさせることも可能です。
以下はm_durationが0（アニメーション無し）と0.5のときの比較です。

![アニメーションありとなしの比較][fig:SliderAnimation]

#### 双方向バインドする

SliderのInteractableをtrueにすれば、プレイヤーが画面上で値を変更できるようになります。
プレイヤーに変更された値を変数アセットの値に反映させる（UI -> 変数アセット）には
SliderのOnValueChangedイベントに変数アセットを設定することで可能です。

![Interactableをtrueにした双方向バインディング][fig:InteractableSlider]

## 使用上の注意点

「UI -> 変数アセット」の連携をさせる場合はアニメーションはさせないようにしましょう。

毎フレーム設定するのはパフォーマンス的にネックになる可能性もあります。
とりあえず使用してみて問題になりそうなら、独自に作成するぐらいがいいと思います。

## 実装

StartとUpdateで処理しています。

```cs
void Start()
{
    // 初期化処理...

    if (!m_setOnStart) return;
    SetParameters();
}

void OnValidate()
{
    // 初期化処理...

    SetParameters();
}

void Update()
{
    if (!m_setOnUpdate) return;
    SetParameters();
}
```

SetParametersの実装はクラス毎で異なります。
例えば、BindingSliderの場合、4つの項目を値の変更がある場合に変更しています。

```cs
void SetParameters()
{
    if (m_slider.minValue != m_minValue) m_slider.minValue = m_minValue;
    if (m_slider.maxValue != m_maxValue) m_slider.maxValue = m_maxValue;
    if (m_slider.wholeNumbers != m_wholeNumbers) m_slider.wholeNumbers = m_wholeNumbers;

    // アニメーション処理などは省略...

    if (m_slider.value == value) return;
    if (m_setValueWithoutNotify)
    {
        m_slider.SetValueWithoutNotify(value);
    }
    else
    {
        m_slider.value = value;
    }
}
```

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}

[fig:TestSliderComponent]: Figures/TestSliderComponent.png
[fig:BindingSlider]: Figures/BindingSlider.gif
[fig:SliderAnimation]: Figures/SliderAnimation.gif
[fig:InteractableSlider]: Figures/InteractableSlider.gif
