# IDisplayText

interface

名前空間：SilCilSystem.Views

---

文字列を表示するためのインターフェースです。
「文字列の表示」と一言で表しても、実際には画面、コンソール、ファイルなど出力先は様々です。
複数の表示先に対応できるようにインターフェースを用意しました。

## メンバ一覧

### メソッド

|member|description|
|-|-|
|void SetText(string value)|文字列valueを表示する|

## 使用例

例えば、UnityのuGUIシステムを利用して画面に文字列を表示する場合は
以下のような実装で実現できます。

```cs
using UnityEngine.UI;

class DisplayStringTextUGUI : IDisplayText
{
    private readonly Text m_text;
    public DisplayStringTextUGUI(Text text) => m_text = text;
    public void SetText(string value) => m_text.text = value;
}
```

SilCilSystemではText, TextMeshProに対応したIDisplayTextの実装を用意しています。
これらはGameObjectの拡張メソッドを通して取得できます。

```cs
using UnityEngine;
using SilCilSystem.Components.Views;

public class TestDisplayText : MonoBehaviour
{
    private void Start()
    {
        IDisplayText text = gameObject.GetTextComponent();
        text.SetText("Hello World!");
    }
}
```

このスクリプトをTextやTextMeshProコンポーネントにアタッチします。

## 実装

GetTextComponentの実装は以下です。
順番にGetComponentして取得しています。

```cs
public static IDisplayText GetTextComponent(this GameObject gameObject)
{
    if (gameObject == null) return null;
    if (gameObject.TryGetComponent(out IDisplayText displayText)) return displayText;
    if (gameObject.TryGetComponent(out Text text)) return new DisplayStringTextUGUI(text);
    if (gameObject.TryGetComponent(out TextMeshProUGUI textMeshProUGUI)) return new DisplayStringTextMeshProUGUI(textMeshProUGUI);
    if (gameObject.TryGetComponent(out TextMesh textMesh)) return new DisplayStringTextMesh(textMesh);
    if (gameObject.TryGetComponent(out TextMeshPro textMeshPro)) return new DisplayStringTextMeshPro(textMeshPro);
    return null;
}
```

<!--- footer --->

{% include ver200/footer.md %}

<!--- 参照 --->

{% include ver200/paths.md %}
