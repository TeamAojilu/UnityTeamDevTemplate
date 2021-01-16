# Timer

class

名前空間：SilCilSystem.Timers

継承：UnityEngine.MonoBehaviour

Assembly：SilCilSystem.Packages

---

時間を測定するコンポーネントです。
[変数アセット][page:Variable]の値をUpdateメソッド内で変更します。
2倍速で計算したり、-1倍速にしてカウントダウンさせたり、一定間隔ごとに処理を呼んだりできます。

## 設定項目

### 基本

|type|name|description|note|
|-|-|-|-|
|ReadonlyPropertyBool|Enable|trueなら時間測定を実行||
|VariableFloat|Time|時間を表す変数アセット||
|ReadonlyPropertyFloat|InitialTime|開始次のm_timeの値|Startメソッドで代入|
|ReadonlyPropertyFloat|TimeScale|時間スケール|1なら等倍速、2なら2倍速|

### 値の範囲

|type|name|description|note|
|-|-|-|-|
|ReadonlyPropertyFloat|Min|m_timeの最小値||
|ReadonlyPropertyFloat|Max|m_timeの最大値|制限をつけたくない場合は十分に大きな値を入れる|
|ReadonlyPropertyBool|Repeating|繰り返しありの場合true|trueならm_maxを超えるとm_minに、m_minを下回るとm_maxになる|

### イベント

|type|name|description|note|
|-|-|-|-|
|UnityEvent|OnMinValue|時間がMinに到達した時に呼ばれる|カウントダウンのコールバックなどに使用|
|UnityEvent|OnMaxValue|時間がMaxに到達した時に呼ばれる||

※Repeatingがtrueになっている場合、OnMinValue, OnMaxValueは同時に呼ばれます。

## 使用例

### カウントダウンタイマー

制限時間30秒のカウントダウンタイマーを作成する場合は、以下のように設定します。

|設定|値|
|-|-|
|InitialTime|30|
|TimeScale|-1|
|Min|0|
|Max|30|

OnMinValueに0秒になった時の処理を登録すれば、コールバックが可能です。
例えば、以下のようなスクリプトを作成して登録します。

```cs
using UnityEngine;

public class TestTimer : MonoBehaviour
{
    // UnityEventに登録するためにpublicにする.
    public void TimeOver()
    {
        Debug.Log("TimeOver");
    }
}
```

![カウントダウンタイマーをつくる][fig:CountDownTimer]

### 5秒ごとに処理をする

Repeatingをtrueにして5秒ごとに処理を読んでみましょう。
以下のように設定します。

|設定|値|
|-|-|
|InitialTime|0|
|TimeScale|1|
|Min|0|
|Max|5|
|Repeating|true|

OnMinValueに5秒ごとに呼ぶ処理を登録します。

![5秒ごとに処理を行うタイマーを設定する][fig:RepeatingTimer]

## 実装

Updateで処理して変数アセットの値を加算しています。

```cs
private void Update()
{
    if (m_time == null) return;
    if (!m_enable) return;
    SetTime(m_time + Time.deltaTime * m_timeScale);
}
```

更新処理はRepeatingがtrueの場合はMathf.Repeatを、
falseの場合はMathf.Clampを使用しています。
変更があった場合のみ、変数アセットに値を代入しています。

```cs
private void SetTime(float t)
{
    if (m_repeating)
    {
        t = Mathf.Repeat(t - m_min, m_max - m_min) + m_min;
        if (t == m_time) return;
        m_time.Value = t;
        // ...
        // イベント判定処理は省略...
    else
    {
        t = Mathf.Clamp(t, m_min, m_max);
        if (t == m_time) return;
        m_time.Value = t;
        // ...
        // イベント判定処理は省略...
    }
}
```

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}

[fig:CountDownTimer]: Figures/CountDownTimer.gif
[fig:RepeatingTimer]: Figures/RepeatingTimer.png
