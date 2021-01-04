using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

namespace SilCilSystem.Editors
{
    internal static class CreateBuiltInUI
    {
        private const string RootPath = "GameObject/UI/";
        private const string TextPath = RootPath + "Text";

        public static bool TryCreateText(out Text text)
        {
            text = null;
            if (!EditorApplication.ExecuteMenuItem(TextPath)) return false;

            text = Selection.activeGameObject?.GetComponent<Text>();
            return text != null;
        }
    }
}