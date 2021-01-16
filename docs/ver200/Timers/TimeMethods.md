# TimeMethods

static class

名前空間：SilCilSystem.Timers

Assembly：SilCilSystem.Packages

---

時間に関するメソッドが定義されています。
一定時間後に処理を呼んだり、一定の時間間隔で処理を呼んだりできます。

## メソッド

### void CallDelayed

指定した秒数が経過した後に処理を呼びます。

|type|parameter|description|
|-|-|-|
|Action|method|実行する処理|
|float|delay|処理が実行されるまでの時間(単位は秒)|
|UnityEngine.Object|lifeTimeObject|処理が実行される前にnullになった場合、呼び出しが行われません|
|[UpdateMode][page:UpdateMode]|updateMode|時間の計測タイミング|

#### 使用例

例えば、1秒後に処理を呼ぶ場合は以下のように使います。

```cs
using UnityEngine;
using SilCilSystem.Timers;

public class TestCallDelayed : MonoBehaviour
{
    private void Start()
    {
        TimeMethods.CallDelayed(() => Debug.Log("Test"), 1f, gameObject);
    }
}
```

### void CallRepeatedly

指定した秒数が経過するたびに処理を呼びます。

|type|parameter|description|
|-|-|-|
|Action|method|実行する処理|
|float|interval|処理を実行する間隔(単位は秒)|
|Object|lifeTimeObject|処理が実行される前にnullになった場合、呼び出しが行われません|
|[UpdateMode][page:UpdateMode]|updateMode|時間の計測タイミング|
|int|repeatCount|繰り返し回数(負なら制限なし)|
|Func<bool>|enabled|trueを返している間のみ動作します(nullのときは常にtrue扱い)|

#### 使用例

例えば、1秒ごとに処理を呼ぶ場合は以下のように使います。

```cs
using UnityEngine;
using SilCilSystem.Timers;

public class TestCallRepeatedly : MonoBehaviour
{
    private void Start()
    {
        int count = 0;
        TimeMethods.CallRepeatedly(() => Debug.Log(++count), 1f, gameObject);
    }
}
```

## 実装

### void CallDelayed

時間をチェックして、`delay`を超えたら処理を呼んでいます。
簡単に書くとこんな感じに処理しています。

```cs
// UpdateDispatcherから呼ばれる.
public bool Update(float deltaTime)
{
    m_time += deltaTime;
    if (m_time < Delay) return true;
    CallBack?.Invoke();
    return false;
}
```

処理は[UpdateDispatcher][page:UpdateDispatcher]から呼ばれるようになっています。

### void CallRepeatedly

時間をチェックして、`interval`を超えたら処理を呼んでいます。
簡単に書くとこんな感じに処理しています。

```cs
// UpdateDispatcherから呼ばれる.
public bool Update(float deltaTime)
{
    m_time += deltaTime;
    if (m_time < Interval) return true;

    Method?.Invoke();
    m_count++;

    if(RepeatCount >= 0 && m_count >= RepeatCount)
    {
        return false;
    }
    else
    {
        m_time = 0f;
        return true;
    }
}
```

処理は[UpdateDispatcher][page:UpdateDispatcher]から呼ばれるようになっています。
1回の更新処理で処理が2回呼ばれることはありません。

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}
