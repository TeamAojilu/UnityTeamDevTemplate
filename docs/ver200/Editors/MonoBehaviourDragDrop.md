# スクリプトをドラッグ&ドロップしてゲームオブジェクトを作成する

エディタ拡張

Assembly：SilCilSystem.Attributes

---

MonoBehaviourを継承したスクリプトをHierarchyにドラッグ&ドロップすることで、
スクリプトがアタッチされたゲームオブジェクトの生成が可能です。

**画像を挿入予定**

複数のスクリプトをドラッグ&ドロップする場合には、2パターンから選択できます。

1. ゲームオブジェクトを1つ作成する

**画像を挿入予定**

2. ゲームオブジェクトをそれぞれ作成する

**画像を挿入予定**

## 機能のON/OFF

ドラッグ&ドロップでのゲームオブジェクト生成機能をOFFにするには、
SilCilSystemメニューからDragDrop/MonoBehaviourのチェックを外してください。

**画像を挿入予定**

## カスタマイズ：生成できるゲームオブジェクトの種類を増やす

カスタム属性により生成時に選択できるゲームオブジェクトの種類を増やすことができます。
独自のゲームオブジェクト生成機能を追加するには、
引数無しで返り値がGameObject型のstatic関数に`[MonoBehaviourDragDrop("メニュー名")]`をつけます。
例えば、空のゲームオブジェクトを生成する処理は以下のようになっています。

```cs
// Editor拡張用スクリプトなのでシンボルを使用, あるいは, Editorフォルダ以下に置く.
#if UNITY_EDITOR
using UnityEngine;
using UnityEditor; // Undoに必要.
using SilCilSystem.Editors; // MonoBehaviourDragDropの使用に必要.

public static class CustomGameObjectGenerators
{
    [MonoBehaviourDragDrop("Custom/Empty")]
    private static GameObject CreateEmpty()
    {
        var obj = new GameObject();
        obj.name = "Custom"; // 名前をCustomにしてみる.
        Undo.RegisterCreatedObjectUndo(obj, "Create Empty"); // ctrl+zなどのUndoができるようにする.
        return obj;
    }
}
#endif
```

## 実装

MonoBehaviourDragDropで指定された関数をリフレクションで呼び出し、
取得したゲームオブジェクトにAddComponentしています。

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}
