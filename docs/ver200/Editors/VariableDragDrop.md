# 変数アセットをドラッグ&ドロップしてゲームオブジェクトを作成する

エディタ拡張

Assembly：SilCilSystem.Attributes

---

[変数アセット][page:Variable]をヒエラルキーにドラッグ&ドロップすることで、
関連するゲームオブジェクトの生成が可能です。

例えば、`int`型の変数アセットをドラッグ&ドロップして変数の値を表示する`Text`を作成できます。

**画像を挿入予定**

UIだけでなく[Timer][page:Timer]などのコンポーネントも用意しています。

## 機能のON/OFF

ドラッグ&ドロップでのゲームオブジェクト生成機能をOFFにするには、
`SilCilSystem`メニューから`DragDrop/Variable`のチェックを外してください。

## カスタマイズ：生成できるゲームオブジェクトの種類を増やす

生成時に選択できるゲームオブジェクトの種類を増やすには`VaraibleDragDropAction`クラスを継承して
属性`[VariableDragDrop("メニュー名")]`をつけてください。
例えば、ゲームオブジェクトのアクティブを変数アセットにより切り替えることができる[GameObjectActivator][page:Activator]の生成は以下のようになっています。

```cs
// Editor拡張用スクリプトなのでシンボルを使用, あるいは, Editorフォルダ以下に置く.
#if UNITY_EDITOR
using System.Ling;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Activators;
using UnityEditor; // Undoに必要.
using SilCilSystem.Editors; // VariableDragDropの使用に必要.

[VariableDragDrop("Activators/GameObjects")]
internal class GameObjectActivatorGenerator : VariableDragDropAction
{
    // 引数にはドラッグされた変数アセットが入る(サブアセットを含む)ので、利用可能ならtrueを返す.
    public override bool IsAccepted(VariableAsset[] assetIncludingChildren)
    {
        // bool型があれば生成可能なのでtrueを返す.
        return assetIncludingChildren.Any(x => x is ReadonlyBool);
    }

    public override void OnDropExited(VariableAsset dropAsset)
    {
        // ReadonlyBoolを取得する.
        var variable = dropAsset?.GetSubVariable<ReadonlyBool>();
        if (variable == null) return;

        // 空のゲームオブジェクトを作ってAddComponentして設定.
        var obj = new GameObject();
        obj.name = $"GameObjectActivator {dropAsset.name}";
        var activator = obj.AddComponent<GameObjectActivator>();
        activator.m_isActive.Variable = variable;

        // Undoに設定&選択状態にする.
        Undo.RegisterCreatedObjectUndo(obj, $"Create {obj.name}");
        Selection.activeObject = obj;
    }
}
#endif
```

## 実装

`VariableDragDrop`属性で指定されたクラスをリフレクションで生成して処理を呼んでいます。
そのため、引数のないコンストラクタで動作するようにしてください。

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}
