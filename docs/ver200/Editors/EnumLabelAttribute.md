# EnumLabelAttribute

エディタ拡張, Attribute

名前空間：SilCilSystem.Editors

継承：System.Attribute

---

Enumのエディタ上での表示を変更するカスタム属性です。

## 使用例

EnumLabel属性をつけるだけです。

```cs
using UnityEngine;
using SilCilSystem.Editors;

public enum Animal
{
    [EnumLabel("ひと")] Human,
    [EnumLabel("いぬ")] Dog,
    [EnumLabel("ねこ")] Cat,
}

public class Test : MonoBehaviour
{
    [SerializeField] private Animal m_animal = Animal.Human;
}
```

画像を挿入予定

## 注意点

Enumの要素以外に用いた場合の動作は保証できません。
検証していないです。

## 実装

プログラムがコンパイルされるとリフレクションでEnumLabelがついているEnumを検索します。
エディタ上ではIntPopupを使用して表示しています。
例外処理などを省いて書くと以下みたいな感じです。

```cs
public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
{
    var displayNames = EnumLabelAttributeList.GetDisplayNames(fieldInfo.FieldType);
    var options = EnumLabelAttributeList.GetValues(fieldInfo.FieldType);

    EditorGUI.BeginProperty(position, label, property);
    property.intValue = EditorGUI.IntPopup(position, label, property.intValue, displayNames, options);
    EditorGUI.EndProperty();
}
```

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}
