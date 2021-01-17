# 変数アセット

abstract

名前空間：SilCilSystem.Variables (ジェネリック版はSilCilSystem.Variables.Generic)

継承：[VariableAsset][page:VariableAsset]

Assembly：SilCilSystem

---

異なるスクリプト間で変数の値を共有できます。
値の変更が可能な`Variable<T>`と値の読み取りのみ可能な`ReadonlyVariable<T>`が存在します。

## クラス一覧

|抽象クラス|役割|
|-|-|
|Variable\<T>|ValueでT型の値のset/get|
|ReadonlyVariable\<T>|ValueでT型の値のget|

Unity2019ではジェネリッククラスをシリアライズすることができないため、
よく使う型については非ジェネリックな抽象クラスを用意しています。
実際のスクリプトでは以下のクラスを使用することになると思います。

### Primitive

|型|クラス|読取専用クラス|
|-|-|-|
|bool|VariableBool|ReadonlyBool|
|string|VariableString|ReadonlyString|
|int|VariableInt|ReadonlyInt|
|float|VariableFloat|ReadonlyFloat|

### UnityEngineのstruct型

|型|クラス|読取専用クラス|
|-|-|-|
|Vector2|VariableVector2|ReadonlyVector2|
|Vector2Int|VariableVector2Int|ReadonlyVector2Int|
|Vector3|VariableVector3|ReadonlyVector3|
|Vector3Int|VariableVector3Int|ReadonlyVector3Int|
|Color|VariableColor|ReadonlyColor|
|Quaternion|VariableQuaternion|ReadonlyQuaternion|

## 使用例

例えば、キーを押すたびに値が増えるカウンタを作ってみます。

```cs
using UnityEngine;
using SilCilSystem.Variables;

public class TestVariableInt : MonoBehaviour
{
    // 変数アセットをシリアライズしてインスペクタで設定可能に.
    [SerializeField] private VariableInt m_count = default;

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            // キーを押すたびに値を1増やす.
            m_count.Value++;
        }
    }
}
```

カウンタの値を`Debug.Log`で表示することを考えます。
値を読み取るだけで値の変更は行わないため、`ReadonlyInt`を使用します。

```cs
using UnityEngine;
using SilCilSystem.Variables;

public class TestReadonlyInt : MonoBehaviour
{
    // 変数アセットをシリアライズしてインスペクタで設定可能に.
    [SerializeField] private ReadonlyInt m_count = default;

    private void Update()
    {
        // 値を表示する. UpdateでDebug.Logすべきではないけど、サンプルなので許して.
        Debug.Log(m_count.Value);
    }
}
```

インスペクタ上で同じ変数を設定すれば、両者の連携が可能になります。
メニューから`int`型の変数アセットを作成して設定します。

![変数アセットをインスペクタ上で設定する][fig:VariableInInspector]

## 使用上の注意点

`Update`で値をチェックして、値が変わったら何か処理をするといったことをしたい場合には
[イベントアセット][page:GameEvent]の使用を検討してください。
値の変更が少ない場合には、イベントアセットで実現するほうが良いと思います。
上記のカウンタの例もイベントアセットで実現できます。
イベントを使用する場合は[こちら][page:OnValueChanged]が参考になります。

## 実装

`Variable<T>`は抽象クラスなので、具体的な実装を記述していません。
インスペクタ上で設定するアセットは`internal`クラスとして実装しています。

最も簡単な変数アセットの実装は変数を1つだけ持つ単純な`ScriptableObejct`です。
例えば、`Variable<bool>`を継承したクラスならこうなります。

```cs
// 使用する際は具体的な型（この場合はBoolValue）を知る必要がないのでinternalで実装.
internal class BoolValue : VariableBool
{
    [SerializeField] private bool m_value = default;
    public override bool Value { get => m_value; set => m_value = value; }
}
```

読み取り専用クラスの具体的な実装は`VariableBool`を参照に持ち、値を返します。

```cs
internal class ReadonlyBoolValue : ReadonlyBool
{
    [SerializeField] private VariableBool m_variable = default;
    public override bool Value => m_variable.Value;
}
```

これら2つを機能させるためには、それぞれのアセットを作るだけでなく、参照関係も設定しなければなりません。
つまり、機能させるには以下の3つのステップが必要です。

1. `BoolValue`アセットを作成する
2. `ReadonlyBoolValue`アセットを作成する
3. `ReadonlyBoolValue`アセットの`m_variable`に1で作成した`BoolValue`アセットを設定する

これを変数作成のたびにやるのは面倒なので、エディタ拡張で対応しています。

実際には[値の変更を通知][page:OnValueChanged]するために[イベントアセット][page:GameEvent]も紐づけており、
1つの変数に対して4つのアセットが生成されるようになっています。

これらのアセットをすべて表示するのは煩わしいので、サブアセットにして`HideFlags`を設定することで非表示にしています。
アセットのインスペクタにある`Show/Hide`を押すことで表示/非表示を切り替えられます。
細かく参照設定を行いたい場合は表示するとよいでしょう。

![サブアセットの表示を切り替える][fig:ShowHideSubAsset]

※変更を反映するにはプロジェクトビューを更新させなければならないようです。この辺り、エディタ拡張でうまくやることができませんでした。

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}

<!--- 画像 --->

[fig:VariableInInspector]: Figures/VariableInInspector.gif
[fig:ShowHideSubAsset]: Figures/ShowHideSubAsset.gif
