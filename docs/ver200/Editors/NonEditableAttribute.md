# NonEditableAttribute

エディタ拡張, Attribute

名前空間：SilCilSystem.Editors

継承：UnityEngine.PropertyAttribute

---

インスペクタ上で編集不可にするカスタム属性です。
シリアライズはしたいが、Inspectorで編集したくない場合、通常はHideInInspector属性を利用しますが、
これだと値が表示されないため、確認が不便です。

## 使用例

NonEditable属性をフィールドにつけるだけです。

```cs
using UnityEngine;
using SilCilSystem.Editors;

public class Test : MonoBehaviour
{
    [SerializeField, NonEditable] private int m_value = 0;
}
```

画像を挿入予定

## 注意点

あくまでもエディタ拡張によって編集不可にしてあるだけなので、Debugモードに切り替えると値の編集は可能になります。
これは弱点というよりも仕様です。
元々、[変数アセット][page:Variable]の参照設定など、めったにいじることのない項目用に作成しました。
開発中の検証などで、参照設定をいじりたくなることが多々あったので、Debugモードで設定できる仕組みが欲しかったというのが実情です。

## 実装

```cs
[CustomPropertyDrawer(typeof(NonEditableAttribute))]
internal class NonEditableDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label);
        GUI.enabled = true;
    }
}
```

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}
