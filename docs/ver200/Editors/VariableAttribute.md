# 変数アセットをカスタマイズする

エディタ拡張

Assembly：SilCilSystem.Attributes

---

[VariableAssetクラス][page:VariableAsset]を継承したアセットは[変数アセット][page:Variable]の子供として生成することができます。
作成したスクリプトは既存の変数アセットのインスペクタにドラッグ&ドロップすることでサブアセットとして生成できます。
この仕組みを利用することで、変数アセットの変換などが可能になります。

## int型の変数アセットをfloat型に変換する

いくつかの変換についてはカスタマイズするコードを書かなくても既に用意しています。
例えば、`int`型の変数アセットを`float`型として扱いたい場合には、
インスペクタの`Add SubAsset`メニューから`Converter/Float (from int)`を選択しましょう。

![int型の変数アセットをfloat型に変換する][fig:VariableAttribute_ToFloat]

## カスタマイズ：独自の変数アセットを書く

例えば、独自の`Enum`型に対応する変数アセットを作成する場合、最も単純には以下のようになります。

```cs
using UnityEngine;
using SilCilSystem.Variables.Generic;

public enum Element
{
    Type1,
    Type2,
    Type3,
}

public class VariableElement : Variable<Element>
{
    [SerializeField] private Element m_value = Element.Type1;

    public override Element Value
    {
        get => m_value;
        set => m_value = value;
    }
}
```

これを生成する処理として`CreateAssetMenu`属性などを使えばいいわけです。

独立したアセットを作りたい場合はそれで構わないんですが、
ここでは、`int`型の変数を変換して独自の`Element`型にしたい場合を考えます。
単純に考えるなら、`int`型の変数を参照に持って`Value`で変換するだけです。
コードを修正すると、以下のようになります。

```cs
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Generic;

public enum Element
{
    Type1,
    Type2,
    Type3,
}

public class VariableElement : Variable<Element>
{
    // int型の変数を参照に持つ.
    [SerializeField] private VariableInt m_value = default;

    public override Element Value
    {
        get
        {
            try
            {
                return (Element)m_value.Value;
            }
            catch (System.InvalidCastException)
            {
                // キャストに失敗したらType1を返すようにする.
                return Element.Type1;
            }
        }
        set
        {
            m_value.Value = (int)value;
        }
    }
}
```

あとはアセットを作成して、`int`型の変数アセットをエディタ上で設定してあげればいいわけですが、
ちょっとした変換のたびに管理しなければならないアセットの数が増えてしまうのは面倒です。
そのため、変数アセットのインスペクタにスクリプトをドラッグ&ドロップしてサブアセットとして生成できるようになっています。
`int`型の変数アセットにドラッグ&ドロップして、参照を設定すれば完了です。

![カスタマイズした変数アセットをドラッグ&ドロップする][fig:VariableAttribute_DragDrop]

でもどうせなら、参照の設定を自動でやってほしい場合があります。
また、頻繁に使うものはドラッグ&ドロップだけでなくメニューからも選びたくなるでしょう。
そんな時は`Variable`属性と`OnAttached`属性を利用します。
例えば、こんな感じのコードになります。

```cs
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Generic;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

public enum Element
{
    Type1,
    Type2,
    Type3,
}

// サブアセットになった時の名前とメニュー名、対象とする型を指定.
[Variable("ToElement", "Custom/ToElement", typeof(VariableInt))]
public class VariableElement : Variable<Element>
{
    // int型の変数を参照に持つ.
    [SerializeField] private VariableInt m_value = default;

    public override Element Value
    {
        get
        {
            try
            {
                return (Element)m_value.Value;
            }
            catch (System.InvalidCastException)
            {
                // キャストに失敗したらType1を返すようにする.
                return Element.Type1;
            }
        }
        set
        {
            m_value.Value = (int)value;
        }
    }

    // エディタ拡張でのみ使用するため、シンボルで囲む.
#if UNITY_EDITOR
    [OnAttached]
    private void OnAttached(VariableAsset parent)
    {
        // 参照を設定する. GetSubVariableメソッドはエディタ上でしか機能しないので注意.
        m_value = parent.GetSubVariable<VariableInt>();
    }
#endif
}
```

これで、ドラッグ&ドロップだけでなく、メニューからも追加できるようになります。

![カスタマイズした変数アセットをメニューから追加する][fig:VariableAttribute_DragDrop_CustomMenu]

## 注意点

`OnAttached`属性をつけて機能するには以下の2つの条件が必要です。

1. `Variable`属性がついていること
2. 戻り値が`void`, 引数が[VariableAsset][page:VariableAsset]を1つとるメソッドであること

メニューに表示したくない＝ドラッグ&ドロップで機能すればいいというだけであれば、
以下のようにタイプを`null`に指定すればOKです。

```cs
[Variable("ToElement", null)]
```

また、自動で参照設定するようにした場合、インスペクタ上で参照をいじれてしまうのは不便かもしれません。
その場合は、[NonEditable属性][page:NonEditableAttribute]を利用するとよいでしょう。

## 実装

属性で指定されたクラスとメソッドをリフレクションしています。

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}

<!--- 画像 --->

[fig:VariableAttribute_ToFloat]: Figures/VariableAttribute_ToFloat.gif
[fig:VariableAttribute_DragDrop]: Figures/VariableAttribute_DragDrop.gif
[fig:VariableAttribute_DragDrop_CustomMenu]: Figures/VariableAttribute_DragDrop_CustomMenu.gif
