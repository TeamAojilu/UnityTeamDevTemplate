# 変数の値が変化した場合に処理を呼ぶ

変数の値が変化した場合に処理を呼びたい場合には、変数アセットのサブアセットになっているListenerが利用できます。
これは[変数アセット][page:Variable]と[イベントアセット][page:GameEvent]を組み合わせた機能です。

## 使用例

例えば、キーを押すたびに値が増えるカウンタを作ってみます。
これは[変数アセットのページ][page:Variable]に記載されているTestVariableIntをそのまま利用できます。

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

カウンタの値が変更された場合にDebug.Logで表示することを考えます。
イベントとして処理するため、GameEventIntListenerを使用します。
これも[イベントアセットのページ][page:GameEvent]に記載されているDebugDiceをそのまま利用できます。
（スクリプト名が適切ではありませんが、サンプルなので気にしないでください。）

```cs
using System;
using UnityEngine;
using SilCilSystem.Variables;

public class DebugDice : MonoBehaviour
{
    // イベントアセットをシリアライズしてインスペクタで設定可能に.
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

インスペクタ上で同じ変数を設定すれば、両者の連携が可能になります。
プロジェクトのメニューからInt型の変数アセットを作成します。
VariableIntには変数アセットを、GameEventIntListenerにはサブアセットになっているListenerアセットを設定します。

![インスペクタ上で変数とイベントの設定を行う][fig:OnValueChangedInInspector]

※Listenerアセットは1度GameEventアセットを選択して更新しないと生成されないようになっています。

## 実装

正確には値が代入された場合にイベントが呼ばれるようになっています。
例えば、boolの値の代入を検出してイベントを呼ぶ実装は以下です。
イベントアセットを参照として持ち、setter内でPublishを読んでいるだけです。

```cs
using UnityEngine;

namespace SilCilSystem.Variables
{
    // 使用する際は具体的な型（この場合はNotificationBool）を知る必要がないのでinternalで実装.
    internal class NotificationBool : BoolValue // 変数アセットのBoolValueを継承.
    {
        [SerializeField] private GameEventBool m_onValueChanged = default;

        public override bool Value
        {
            get => base.Value;
            set
            {
                base.Value = value;
                // setterが呼ばれる度にイベントを呼ぶ.
                m_onValueChanged?.Publish(value);
            }
        }
    }
}
```

値が変更された場合に処理を呼ぶ機能の実現には以下の2つのステップが必要になります。

1. NotificationBoolアセットを生成する。
2. bool型のイベントアセットを生成する。
3. NotificationBoolアセットのm_onValueChangedに2で生成したアセットを設定する。

これを作成のたびにやるのは面倒なので、Editor拡張で対応しています。
変数アセットを作成すると自動でイベント用のアセットが生成されます。

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}

[fig:OnValueChangedInInspector]: Figures/OnValueChangedInInspector.gif
