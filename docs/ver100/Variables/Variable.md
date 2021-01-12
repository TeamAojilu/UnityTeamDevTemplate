# 変数アセット

abstract

名前空間：SilCilSystem.Variables

継承：UnityEngine.ScriptableObject

---

異なるスクリプト間で変数の値を共有できます。
値の変更が可能なVariable\<T>と値の読み取りのみ可能なReadonlyVariable\<T>が存在します。

## クラス一覧

|抽象クラス|役割|
|-|-|
|Variable\<T>|ValueプロパティでT型の値のset/get|
|ReadonlyVariable\<T>|ValueプロパティでT型の値のget|

ただ、UnityではGenericクラスのScriptableObjectをアセットとして生成することができないため、
よく使うであろう型についてはGenericクラスを継承した非Genericな抽象クラスを用意しています。
実際のコーディングではこちらを使用することになると思います。

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

カウンタの値をDebug.Logで表示することを考えます。
値を読み取るだけで値の変更は行わないため、ReadonlyIntを使用します。

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
プロジェクトのメニューからInt型の変数アセットを作成します。
VariableIntには変数アセットを、ReadonlyIntにはサブアセットになっているReadonlyのアセットを設定します。

![変数アセットをインスペクタ上で設定する][fig:VariableInInspector]

## 使用上の注意点

Updateで値をチェックして、値が変わったら何か処理をするといったことをしたい場合には
イベントアセットの使用を検討してください。
値の変更が少ない場合には、イベントアセットで実現するほうが良いと思います。
上記のカウンタの例もイベントアセットで実現できます。
イベントを使用する場合は[こちら][page:OnValueChanged]。

## 実装

変数アセットは変数を1つメンバに持つ単純なScriptableObejctとして実装されています。
例えば、Variable\<bool>を継承した抽象クラスVaraibeBoolの具体的な実装は以下です。

```cs
using UnityEngine;

namespace SilCilSystem.Variables
{
    // 使用する際は具体的な型（この場合はBoolValue）を知る必要がないのでinternalで実装.
    internal class BoolValue : VariableBool
    {
        [SerializeField] private bool m_value = default;
        public override bool Value { get => m_value; set => m_value = value; }
    }
}
```

読み取り専用クラスReadonlyBoolの具体的な実装はVariableBoolを参照に持ち、値を返します。

```cs
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class ReadonlyBoolValue : ReadonlyBool
    {
        [SerializeField] private VariableBool m_variable = default;
        public override bool Value => m_variable.Value;
    }
}
```

したがって、変数1つに対して2つのアセットが必要です。
2つのアセットを作るだけでなく、参照関係も設定しなければなりません。
つまり、機能させるには以下の3つのステップが必要です。

1. BoolValueアセットを作成する
2. ReadonlyBoolValueアセットを作成する
3. ReadonlyBoolValueアセットのm_variableに1で作成したBoolValueアセットを設定する

これを変数作成のたびにやるのは面倒なので、Editor拡張で対応しています。
BoolValueアセットを選択すると自動でReadonlyBoolアセットをサブアセットとして生成し、参照を設定するようになっています。

<!--- footer --->

{% include ver100/footer.md %}

<!--- 参照 --->

{% include ver100/paths.md %}

<!--- 画像 --->
[fig:VariableInInspector]: Figures/VariableInInspector.gif
