# イベントアセット

abstract

名前空間：SilCilSystem.Variables

継承：[VariableAsset][page:VariableAsset]

---

異なるスクリプト間でのメソッドの実行ができます。
メソッドの実行が可能なGameEventとメソッドの登録のみ可能なGameEventListenerがあります。

## クラス一覧

### 引数なしのメソッドを扱う

|抽象クラス|役割|
|-|-|
|GameEvent|Publishでメソッドの実行|
|GameEventListener|Subscribeでメソッドの登録|

### 引数1つのメソッドを扱う

|抽象クラス|役割|備考|
|-|-|
|GameEvent\<T>|Publishでメソッドの実行|GameEventを継承|
|GameEventListener\<T>|Subscribeでメソッドの登録|GameEventListenerを継承|

Unity2019ではジェネリッククラスをシリアライズすることができないため、
よく使う型については非ジェネリックな抽象クラスを用意しています。
実際のスクリプトでは以下のクラスを使用することになると思います。

#### Primitive

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
    // イベントアセットをシリアライズしてインスペクタで設定可能に.
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
    // イベントアセットをシリアライズしてインスペクタで設定可能に.
    [SerializeField] private GameEventIntListener m_onDiceRolled = default;

    private void Start()
    {
        // 実行するメソッドを登録する. 第2引数に指定したGameObjectが破棄されたらイベント解除される.
        m_disposable = m_onDiceRolled.Subscribe(x => Debug.Log(x), gameObject);
    }
}
```

インスペクタ上で同じイベントアセットを設定すれば、両者の連携が可能になります。
メニューからInt型のイベントアセットを作成して設定します。

![イベントアセットをインスペクタ上で設定する][fig:GameEventInInspector]
**画像は最新版ではないので変更予定**

## 変数の値が変化した場合に処理を呼ぶ

変数の値が変化した場合に処理を呼びたい場合には、
変数アセットのサブアセットになっているListenerが利用できます。
使用方法は[こちら][page:OnValueChanged]が参考になります。

## 使用上の注意点

引数が1つのSubscribeメソッドを使用する場合はIDisposableが返り値になります。
そのDisposeメソッドの呼び出しでイベント解除になるため、呼び出し忘れに気を付けてください。
OnDisableやOnDestroyなど利用しましょう。

IDisposeに関する機能を用意しています。

- [DisposeメソッドをゲームオブジェクトのDestroy時に呼ぶ][page:DisposeOnDestroy]
- [Disposeメソッドをシーンのアンロード時に呼ぶ][page:DisposeOnSceneUnLoaded]
- [複数のIDisposableを1つにまとめる][page:CompositeDisposable]

## 実装

イベントアセットはイベントハンドラをメンバに持つ単純なScriptableObejctとして実装されています。
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

これら2つを機能させるためには、それぞれのアセットを作るだけでなく、参照関係も設定しなければなりません。
つまり、機能させるには以下の3つのステップが必要です。

1. EventNoArgsアセットを作成する
2. EventNoArgsListenerアセットを作成する
3. EventNoArgsListenerアセットのm_eventに1で作成したEventNoArgsアセットを設定する

これを作成のたびにやるのは面倒なので、Editor拡張で対応しています。
GameEventアセットを選択すると自動でEventNoArgsListenerアセットをサブアセットとして生成し、参照を設定するようになっています。

これらのアセットをすべて表示するのは煩わしいので、サブアセットにしてHideFlagsを設定することで非表示にしています。
アセットのインスペクタにあるShow/Hideを押すことで表示/非表示を切り替えられます。

ここに画像を挿入予定

※変更を反映するにはプロジェクトビューを更新させなければならないようです。この辺り、エディタ拡張でうまくやることができませんでした。

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}

[fig:GameEventInInspector]: Figures/GameEventInInspector.gif
