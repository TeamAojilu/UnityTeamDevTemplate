# Property<T, TVariable> / ReadonlyProperty<T, TVariable>

class

名前空間：SilCilSystem.Variables (ジェネリック版はSilCilSystem.Variables.Generic)

---

Property, ReadonlyPropertyはインスペクタ上で変数と[変数アセット][page:Variable]のどちらを使うかを選択できるようになるクラスです。
これらを用いることで、変数アセットを必要以上に増やすことなくスクリプトの設定項目をエディタ上で調整できるようになります。

## メンバ一覧

|クラス|役割|
|-|-|
|Property\<T, TVariable>|ValueプロパティでT型の値のset/get|
|ReadonlyProperty\<T, TVariable>|ValueプロパティでT型の値のget|

T: 変数の型（int, floatなど）
TVariable: T型の変数アセット（VariableInt, VariableFloatなど）

Unity2019ではジェネリッククラスをシリアライズすることができないため、
よく使う型については非ジェネリックな抽象クラスを用意しています。
実際のスクリプトでは以下のクラスを使用することになると思います。

### Primitive

|型|クラス|読取専用クラス|
|-|-|-|
|bool|PropertyBool|ReadonlyPropertyBool|
|string|PropertyString|ReadonlyPropertyString|
|int|PropertyInt|ReadonlyPropertyInt|
|float|PropertyFloat|ReadonlyPropertyFloat|

### UnityEngineのstruct型

|型|クラス|読取専用クラス|
|-|-|-|
|Vector2|PropertyVector2|ReadonlyPropertyVector2|
|Vector2Int|PropertyVector2Int|ReadonlyPropertyVector2Int|
|Vector3|PropertyVector3|ReadonlyPropertyVector3|
|Vector3Int|PropertyVector3Int|ReadonlyPropertyVector3Int|
|Color|PropertyColor|ReadonlyPropertyColor|
|Quaternion|PropertyQuaternion|ReadonlyPropertyQuaternion|

## 使用例

Unityでは変数をシリアライズすることでエディタ上で変数の値を変更することができます。
例えば、キャラクターの移動速度など実際にゲームをプレイしながら調整したい値はこの機能を用いると便利です。

SilCilSystemを使わない場合にはこうなります。

```cs
using UnityEngine;

public class TestSerializeField : MonoBehaviour
{
    // 移動速度をシリアライズしてインスペクタで設定可能に.
    [SerializeField] private float m_speed = 10f;

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        transform.position += h * Vector3.right * Time.deltaTime * m_speed;
    }
}
```

SilCilSystemの[変数アセット][page:Variable]を使用する場合は以下のようになります。

```cs
using UnityEngine;
using SilCilSystem.Variables;

public class TestSerializeVariable : MonoBehaviour
{
    // 変数を変数アセットに変更.
    [SerializeField] private ReadonlyFloat m_speed = default;

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        transform.position += h * Vector3.right * Time.deltaTime * m_speed;
    }
}
```

変数アセットへの変更により、移動速度を他のスクリプトから制御できるようになりました。
しかし、動作させるためには、変数アセットを作成して設定するという手間が加わりました。
外部との連携をせずに動かしたい場合であっても変数アセットを作成する必要があり、少し面倒です。

そこで、Property, ReadonlyPropertyの出番です。
今回はReadonlyFloatを用いているので、それに対応するReadonlyPropertyFloatを使用します。

```cs
using UnityEngine;
using SilCilSystem.Variables;

public class TestSerializeProperty : MonoBehaviour
{
    // 変数アセットを使う/使わないを選べるようになる.
    [SerializeField] private ReadonlyPropertyFloat m_speed = new ReadonlyPropertyFloat(10f);

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        transform.position += h * Vector3.right * Time.deltaTime * m_speed;
    }
}
```

このようにすることで、変数アセットを使用したい場合にのみ、エディタ上で指定できるようになります。

![エディタ上での見え方][fig:PropertyFloat]

**この画像は最新ではないので変更予定**

## 使用上の注意点

全ての設定項目をこれで置き換えるのが妥当かどうかは考える必要があります。
Property, ReadonlyPropertyはRangeやTextAreaなどのAttributeや配列の複数ファイルドラッグ&ドロップに対応していません。
外部と連携する必要がないと思われるものは通常のシリアライズで実装することも考えましょう。

## 実装

内部で変数アセットと変数の両方の値を保持しているだけのラッパークラスです。

```cs
public class Property<T, TVariable> where TVariable : Variable<T>
{
    // 2つの値を保持.
    [SerializeField] private T m_value = default;
    [SerializeField] private TVariable m_variable = default;

    public Property(T value) => Value = value;

    public T Value
    {
        // 変数アセットがnullのときは変数の値を使う.
        get => m_variable ?? m_value;
        set
        {
            m_value = value;
            m_variable?.SetValue(value);
        }
    }
}
```

これをエディタ拡張で1行で表示するようにしています。

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}

[fig:PropertyFloat]: Figures/PropertyFloat.gif
