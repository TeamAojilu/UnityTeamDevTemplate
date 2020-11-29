using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace SilCilSystem.Components.Views
{
    /// <summary>文字を表示するためのインターフェース</summary>
    public interface IDisplayText
    {
        void SetText(string value);
    }

    public static class DisplayTextExtensions
    {
        /// <summary>
        /// GameObjectにアタッチされているIDisplayText/Text/TextMeshPro(UGUI, 3D Text)を取得してインターフェースを返す
        /// 【挙動】ない場合はnull
        /// </summary>
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

        internal class DisplayStringTextUGUI : IDisplayText
        {
            private readonly Text m_text;
            public DisplayStringTextUGUI(Text text) => m_text = text;
            public void SetText(string value) => m_text.text = value;
        }

        internal class DisplayStringTextMeshProUGUI : IDisplayText
        {
            private readonly TextMeshProUGUI m_text;
            public DisplayStringTextMeshProUGUI(TextMeshProUGUI text) => m_text = text;
            public void SetText(string value) => m_text.text = value;
        }

        internal class DisplayStringTextMesh : IDisplayText
        {
            private readonly TextMesh m_textMesh;
            public DisplayStringTextMesh(TextMesh textMesh) => m_textMesh = textMesh;
            public void SetText(string value) => m_textMesh.text = value;
        }

        internal class DisplayStringTextMeshPro : IDisplayText
        {
            private readonly TextMeshPro m_textMesh;
            public DisplayStringTextMeshPro(TextMeshPro textMesh) => m_textMesh = textMesh;
            public void SetText(string value) => m_textMesh.text = value;
        }
    }
}