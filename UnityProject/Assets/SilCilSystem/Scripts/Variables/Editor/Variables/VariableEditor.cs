using UnityEngine;
using SilCilSystem.Variables;
using UnityEditor;

#if UNITY_EDITOR
namespace SilCilSystem.Editors
{
    internal class VariableEditor<TVariable, TReadonly> : Editor
        where TVariable : ScriptableObject
        where TReadonly : ScriptableObject, IVariableSetter<TVariable>
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // 永続化されていないならスキップ.
            if (!EditorUtility.IsPersistent(target)) return;

            // アセットを検索.
            var path = AssetDatabase.GetAssetPath(target);
            var asset = target as TVariable;
            var readonlyAsset = AssetDatabase.LoadAssetAtPath<TReadonly>(path);

            // 存在しないものを作成していく.

            if (readonlyAsset == null)
            {
                readonlyAsset = CreateInstance<TReadonly>();
                readonlyAsset.SetVariable(asset);
                readonlyAsset.hideFlags = HideFlags.NotEditable;
                AssetDatabase.AddObjectToAsset(readonlyAsset, target);
                serializedObject.ApplyModifiedProperties();
                AssetDatabase.SaveAssets();
            }

            // 名前の変更.

            if (readonlyAsset.name != $"{target.name}Readonly")
            {
                readonlyAsset.name = $"{target.name}Readonly";
                EditorUtility.SetDirty(readonlyAsset);
                EditorUtility.SetDirty(target);
                AssetDatabase.ImportAsset(path);
            }

            base.OnInspectorGUI();
        }
    }
}
#endif