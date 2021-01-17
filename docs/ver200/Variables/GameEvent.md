# イベントアセット

abstract

名前空間：SilCilSystem.Variables (ジェネリック版はSilCilSystem.Variables.Generic)

継承：[VariableAsset][page:VariableAsset]

Assembly：SilCilSystem

---

異なるスクリプト間でのメソッドの実行ができます。
メソッドの実行が可能な`GameEvent`とメソッドの登録のみ可能な`GameEventListener`があります。

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

サイコロの値を`Debug.Log`で表示するには`GameEventIntListener`を使用します。

```cs
using UnityEngine;
using SilCilSystem.Variables;

public class DebugDice : MonoBehaviour
{
    // イベントアセットをシリアライズしてインスペクタで設定可能に.
    [SerializeField] private GameEventIntListener m_onDiceRolled = default;

    private void Start()
    {
        // 実行するメソッドを登録する. 第2引数に指定したGameObjectが破棄されたらイベント解除される.
        m_onDiceRolled.Subscribe(x => Debug.Log(x), gameObject);
    }
}
```

インスペクタ上で同じイベントアセットを設定すれば、両者の連携が可能になります。
メニューから`int`型のイベントアセットを作成して設定します。

![イベントアセットをインスペクタ上で設定する][fig:GameEventInInspector]

## 変数の値が変化した場合に処理を呼ぶ

変数の値が変化した場合に処理を呼びたい場合には、
変数アセットのサブアセットになっている`GameEventListener`が利用できます。
使用方法は[こちら][page:OnValueChanged]が参考になります。

## 使用上の注意点

引数が1つの`Subscribe`メソッドを使用する場合は`IDisposable`が返り値になります。
その`Dispose`メソッドの呼び出しでイベント解除になるため、呼び出し忘れに気を付けてください。
`OnDisable`や`OnDestroy`メソッドなどを利用しましょう。

`IDisposable`に関する機能を用意しています。

- [DisposeメソッドをゲームオブジェクトのDestroy時に呼ぶ][page:DisposeOnDestroy]
- [Disposeメソッドをシーンのアンロード時に呼ぶ][page:DisposeOnSceneUnLoaded]
- [複数のIDisposableを1つにまとめる][page:CompositeDisposable]

## 実装

イベントアセットはイベントハンドラをメンバに持つ単純な`ScriptableObejct`として実装されています。
例えば、`GameEvent`を継承したクラス`EventNoArgs`の具体的な実装は以下です。

```cs
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
```

※登録解除用の`IDisposable`の生成に用いている`DelegateDispose`については[こちら][page:DelegateDispose]。

読み取り専用クラス`GameEventListener`の具体的な実装は`GameEvent`を参照に持ち、値を返します。

```cs
internal class EventNoArgsListener : GameEventListener
{
    [SerializeField] private EventNoArgs m_event = default;
    public override IDisposable Subscribe(Action action) => m_event.Subscribe(action);
}
```

これら2つを機能させるためには、それぞれのアセットを作るだけでなく、参照関係も設定しなければなりません。
つまり、機能させるには以下の3つのステップが必要です。

1. `EventNoArgs`アセットを作成する
2. `EventNoArgsListener`アセットを作成する
3. `EventNoArgsListener`アセットの`m_event`に1で作成した`EventNoArgs`アセットを設定する

これを作成のたびにやるのは面倒なので、エディタ拡張で対応しています。
アセットをすべて表示するのは煩わしいので、サブアセットにして`HideFlags`を設定することで非表示にしています。
アセットのインスペクタにある`Show/Hide`を押すことで表示/非表示を切り替えられます。
細かく参照設定を行いたい場合は表示するとよいでしょう。

![サブアセットの表示を切り替える][fig:ShowHideSubAsset]

※変更を反映するにはプロジェクトビューを更新させなければならないようです。この辺り、エディタ拡張でうまくやることができませんでした。

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}

[fig:GameEventInInspector]: Figures/GameEventInInspector.gif
[fig:ShowHideSubAsset]: Figures/ShowHideSubAsset.gif
