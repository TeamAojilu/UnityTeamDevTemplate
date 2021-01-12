# イベントオブジェクト

abstract

名前空間：SilCilSystem.Variables

継承：UnityEngine.ScriptableObject

---

異なるスクリプト間でのメソッドの実行ができます。
メソッドの実行が可能なGameEventと実行されるメソッドの登録のみ可能なGameEventListenerが存在します。

## クラス一覧

### 引数なしのメソッドを扱う

|抽象クラス|役割|
|-|-|
|GameEvent|Publishでメソッドの実行|
|GameEventListener|Subscribeでメソッドの登録|

### 引数1つのメソッドを扱う

|抽象クラス|役割|
|-|-|
|GameEvent\<T>|Publishでメソッドの実行|
|GameEventListener\<T>|Subscribeでメソッドの登録|

ただ、UnityではGenericクラスのScriptableObjectをアセットとして生成することができないため、
よく使うであろう型についてはGenericクラスを継承した非Genericな抽象クラスを用意しています。
引数ありのメソッドを使用する場合はこちらを使用することになると思います。

#### Primitive型

|型|クラス|登録専用クラス|
|-|-|-|
|bool|GameEventBool|GameEventBoolListener|
|string|GameEventString|GameEventStringListener|
|int|GameEventInt|GameEventIntListener|
|float|GameEventFloat|GameEventFloatListener|

#### UnityEngineのstruct型

|型|クラス|登録専用クラス|
|-|-|-|
|Vector2|GameEventVector2|GameEventVector2Listener|
|Vector2Int|GameEventVector2Int|GameEventVector2IntListener|
|Vector3|GameEventVector3|GameEventVector3Listener|
|Vector3Int|GameEventVector3Int|GameEventVector3IntListener|
|Color|GameEventColor|GameEventColorListener|
|Quaternion|GameEventQuaternion|GameEventQuaternionListener|

## 使用例

例えば、キーを押すたびにサイコロを振るスクリプトを作ってみます。

```cs
using UnityEngine;
using SilCilSystem.Variables;

public class RollDice : MonoBehaviour
{
    // イベントオブジェクトをシリアライズしてインスペクタで設定可能に.
    [SerializeField] private GameEventInt m_onDiceRolled = default;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            // キーを押すたびに1から6までの乱数値を生成.
            int value = Random.Range(1, 7);
            // メソッドを呼び出す.
            m_onDiceRolled.Publish(value);
        }
    }
}
```

サイコロの値をDebug.Logで表示するにはGameEventIntListenerを使用します。

```cs
using System;
using UnityEngine;
using SilCilSystem.Variables;

public class DebugDice : MonoBehaviour
{
    // イベントオブジェクトをシリアライズしてインスペクタで設定可能に.
    [SerializeField] private GameEventIntListener m_onDiceRolled = default;

    // 登録解除用.
    private IDisposable m_disposable = default;

    private void Start()
    {
        // 実行するメソッドを登録する.
        m_disposable = m_onDiceRolled.Subscribe(x => Debug.Log(x));
    }

    private void OnDestroy()
    {
        // メソッドの登録解除はDisposeを呼ぶ.
        m_disposable?.Dispose();
    }
}
```

インスペクタ上で同じイベントアセットを設定すれば、両者の連携が可能になります。
プロジェクトのメニューからInt型のイベントアセットを作成します。
GameEventIntにはイベントアセットを、GameEventIntListenerにはサブアセットになっているListenerのアセットを設定します。

![イベントアセットをインスペクタ上で設定する][fig:GameEventInInspector]

## 変数の値が変化した場合に処理を呼ぶ

変数の値が変化した場合に処理を呼びたい場合には、
変数アセットのサブアセットになっているListenerが利用できます。
使用方法は[こちら][page:OnValueChanged]。

## 使用上の注意点

Disposeメソッドの呼び出し忘れに気を付けてください。
登録解除されないと参照が残ったままになります。
そうなると、メモリが解放されなくなりメモリリークの原因になります。
OnDisableやOnDestroyなど利用しましょう。

## 実装

イベントオブジェクトはイベントハンドラをメンバに持つ単純なScriptableObejctとして実装されています。
例えば、GameEventを継承した抽象クラスEventNoArgsの具体的な実装は以下です。

```cs
using System;

namespace SilCilSystem.Variables
{
    // 使用する際は具体的な型（この場合はEventNoArgs）を知る必要がないのでinternalで実装.
    internal class EventNoArgs : GameEvent
    {
        private event Action m_event;
        public override void Publish() => m_event?.Invoke();
        public override IDisposable Subscribe(Action action)
        {
            // イベントを登録.
            m_event += action;
            // 解除用のIDisposableクラスを返す.
            return DelegateDispose.Create(() => m_event -= action);
        }
    }
}
```

読み取り専用クラスGameEventListenerの具体的な実装はGameEventを参照に持ち、値を返します。

```cs
using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class EventNoArgsListener : GameEventListener
    {
        [SerializeField] private EventNoArgs m_event = default;
        public override IDisposable Subscribe(Action action) => m_event.Subscribe(action);
    }
}
```

したがって、イベント1つに対して2つのアセットが必要です。
2つのアセットを作るだけでなく、参照関係も設定しなければなりません。
つまり、機能させるには以下の3つのステップが必要です。

1. EventNoArgsアセットを作成する
2. EventNoArgsListenerアセットを作成する
3. EventNoArgsListenerアセットのm_eventに1で作成したEventNoArgsアセットを設定する

これを作成のたびにやるのは面倒なので、Editor拡張で対応しています。
GameEventアセットを選択すると自動でEventNoArgsListenerアセットをサブアセットとして生成し、参照を設定するようになっています。

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}

[fig:GameEventInInspector]: Figures/GameEventInInspector.gif
