using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace SilCilSystem.Editors.Views
{
    internal static class UICreator
    {
        public static Canvas CreateCanvas()
        {
            var instance = Instantiate<Canvas>(Constants.CanvasTemplateID);
            CreateEventSystem(false);
            return instance;
        }

        public static EventSystem CreateEventSystem()
        {
            if (Object.FindObjectOfType<EventSystem>() != null) return null;
            var instance = Instantiate<EventSystem>(Constants.EventSystemTemplateID, true);
            return instance;
        }
        
        public static Text CreateText()
        {
            return InstantiateElement<Text>(Constants.TextTemplateID);
        }

        public static TextMeshProUGUI CreateTextTMP()
        {
            return InstantiateElement<TextMeshProUGUI>(Constants.TextTMPTemplateID);
        }

        public static Slider CreateSlider()
        {
            return InstantiateElement<Slider>(Constants.SliderTemplateID);
        }

        public static Slider CreateBar()
        {
            return InstantiateElement<Slider>(Constants.BarTemplateID);
        }

        public static Toggle CreateToggle()
        {
            return InstantiateElement<Toggle>(Constants.ToggleTemplateID);
        }

        private static T InstantiateElement<T>(string guid, bool selection = true) where T : Component
        {
            var canvas = GetCanvas();
            var instance = Instantiate<T>(guid, selection);
            instance.transform.SetParent(canvas.transform, false);
            return instance;
        }
        
        private static T Instantiate<T>(string guid, bool selection = true) where T : Component
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var prefab = AssetDatabase.LoadAssetAtPath<T>(path);
            var instance = Object.Instantiate(prefab);
            instance.name = prefab.name;
            if (selection) Selection.activeObject = instance;

            Undo.RegisterCreatedObjectUndo(instance.gameObject, $"Create");
            return instance;
        }

        private static EventSystem CreateEventSystem(bool selection)
        {
            if (Object.FindObjectOfType<EventSystem>() != null) return null;
            return Instantiate<EventSystem>(Constants.EventSystemTemplateID, selection);
        }

        private static Canvas GetCanvas()
        {
            Canvas canvas = null;
            foreach(var gameObj in Selection.gameObjects)
            {
                canvas = gameObj.GetComponent<Canvas>();
                if (canvas != null) return canvas;
            }

            canvas = Object.FindObjectOfType<Canvas>();
            if (canvas != null) return canvas;

            canvas = CreateCanvas();
            return canvas;
        }
    }
}