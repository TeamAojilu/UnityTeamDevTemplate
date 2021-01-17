# EnumLabelAttribute

エディタ拡張, Attribute

名前空間：SilCilSystem.Editors

継承：System.Attribute

Assembly：SilCilSystem.Attributes

---

`Enum`のエディタ上での表示を変更するカスタム属性です。

## 使用例

表示を変えたい`Enum`の要素に`EnumLabel`属性をつけます。

```cs
using UnityEngine;
using SilCilSystem.Editors;

public class TestEnumLabel : MonoBehaviour
{
    public enum Animal
    {
        Human, // カスタマイズしない場合は無しでいい.
        [EnumLabel("いぬ")] Dog,
        [EnumLabel("ねこ")] Cat,
    }

    [SerializeField] private Animal m_animal = Animal.Human;
}
```

![Enumの表示名を変更する][fig:EnumLabel]

## 注意点

`Enum`の要素以外に用いた場合の動作は保証できません。
検証していないです。

## 実装

プログラムがコンパイルされるとリフレクションで`EnumLabel`がついている`Enum`を検索します。
エディタ上では`IntPopup`を使用して表示しています。
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

<!--- 画像 --->

[fig:EnumLabel]: Figures/EnumLabel.gif
