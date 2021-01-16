# Activator

abstract

名前空間：SilCilSystem.Activators

継承：MonoBehaviour

Assembly：SilCilSystem.Packages

---

bool型の[変数アセット][page:Variable]でゲームオブジェクトのActiveやコンポーネントのenabledを切り替えるためのクラスです。

## クラス一覧

|name|description|
|-|-|
|GameObjectActivator|GameObjectのActiveを切り替え|
|BehaviourActivator|コンポーネントのenabledを切り替え|

## 設定項目

|type|name|description|
|-|-|-|
|bool|SetOnStart|trueならStartメソッドで値を反映|
|ReadonlyPropertyBool|SetOnUpdate|trueならUpdateメソッドで値を反映|
|ReadonlyPropertyBool|IsActive|機能のON/OFFを指定|
|bool|Reverse|trueならIsActiveのtrue/falseと逆の値を設定|

## 使用例

例えば、クリックするたびにCubeをON/OFFする処理をしてみます。
まず、bool型の変数アセットを作成して以下のスクリプトに設定します。

```cs
using UnityEngine;
using SilCilSystem.Variables;

public class TestActivator : MonoBehaviour
{
    [SerializeField] private VariableBool m_value = default;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            m_value.Value = !m_value;
        }
    }
}
```

GameObjectActivatorを作成して先ほどの変数アセットを設定します。
TargetsにON/OFFを切り替えたいGameObjectを指定します。

![GameObjectActivatorを設定する][fig:GameObjectActivator]

## 実装

[UpdateDispatcher][page:UpdateDispatcher]を使用して処理を呼んでいます。

```cs
bool MicroUpdate(float deltaTime)
{
    if (!enabled) return true;
    if (m_setOnUpdate) SetActives((m_reverse == false) ? m_isActive : !m_isActive);
    return true;
}
```

SetActivesメソッドはクラス別に実装しています。
GameObjectActivatorの場合は以下のようになります。

```cs
void SetActives(bool value) 
{
    foreach(GameObject target in m_targets)
    {
        if (target == null) continue;
        target.SetActive(value);
    }
}
```

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}

[fig:GameObjectActivator]: Figures/GameObjectActivator.gif
