using UnityEngine;
using UnityEditor;
using SilCilSystem.Variables;

#if UNITY_EDITOR
namespace SilCilSystem.Editors
{
    internal class GameEventEditor<TEvent, TListener> : Editor
        where TEvent : ScriptableObject
        where TListener : ScriptableObject, IGameEventSetter<TEvent>
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // 永続化されていないならスキップ.
            if (!EditorUtility.IsPersistent(target)) return;

            // アセットを検索.
            var path = AssetDatabase.GetAssetPath(target);
            var listenerAsset = AssetDatabase.LoadAssetAtPath<TListener>(path);

            // Listenerが存在しないなら作成.
            if (listenerAsset == null)
            {
                listenerAsset = CreateInstance<TListener>();
                listenerAsset.SetGameEvent(target as TEvent);
                listenerAsset.hideFlags = HideFlags.NotEditable;
                AssetDatabase.AddObjectToAsset(listenerAsset, target);
                serializedObject.ApplyModifiedProperties();
                AssetDatabase.SaveAssets();
            }

            // 名前の変更.
            if (listenerAsset.name != $"{target.name}Listener")
            {
                listenerAsset.name = $"{target.name}Listener";
                EditorUtility.SetDirty(listenerAsset);
                EditorUtility.SetDirty(target);
                AssetDatabase.ImportAsset(path);
            }

            base.OnInspectorGUI();
        }
    }
}
#endif