# Activator

abstract

名前空間：SilCilSystem.Components.Activators

継承：MonoBehaviour

---

イベントでゲームオブジェクトのActiveやコンポーネントのenabledを切り替えるためのクラスです。

## クラス一覧

|name|description|
|-|-|
|GameObjectActivator|GameObjectのActiveを切り替え|
|BehaviourActivator|コンポーネントのenabledを切り替え|

## 設定項目

|type|name|description|
|-|-|-|
|GameEventBoolListener||m_setActive|ON/OFFを行うイベント|
|GameEventListener|m_setActiveTrue|ONにするイベント|
|GameEventListener|m_setActiveFalse|OFFにするイベント|

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

GameObjectActivatorを使用してCubeオブジェクトのSetActiveを切り替えます。
先ほどの変数アセットに関連するGameEventBoolListenerを設定します。
TargetsにON/OFFを切り替えたいGameObjectを指定します。

![GameObjectActivatorを設定する][fig:GameObjectActivator]

## 実装

m_setActive, m_setActiveTrue, m_setActiveFalseのいずれも同一のメソッドを呼んでいます。
OnEnableでイベント登録、OnDisableで登録解除しています。

```cs
void OnEnable()
{
    var disposable = new CompositeDisposable();
    disposable.Add(m_setActive?.Subscribe(SetActives));
    disposable.Add(m_setActiveTrue?.Subscribe(() => SetActives(true)));
    disposable.Add(m_setActiveFalse?.Subscribe(() => SetActives(false)));
    m_disposable = disposable;
}

void OnDisable()
{
    m_disposable?.Dispose();
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

{% include ver100/footer.md %}

<!--- 参照 --->

{% include ver100/paths.md %}

[fig:GameObjectActivator]: Figures/GameObjectActivator.gif
