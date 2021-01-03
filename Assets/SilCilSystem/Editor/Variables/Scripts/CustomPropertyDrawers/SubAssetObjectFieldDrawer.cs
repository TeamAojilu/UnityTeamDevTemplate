using UnityEngine;
using UnityEditor;
using SilCilSystem.Variables.Base;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Generic;

namespace SilCilSystem.Editors
{
    [CustomPropertyDrawer(typeof(Variable<>), true)]
    [CustomPropertyDrawer(typeof(ReadonlyVariable<>), true)]
    [CustomPropertyDrawer(typeof(GameEvent), true)]
    [CustomPropertyDrawer(typeof(GameEvent<>), true)]
    [CustomPropertyDrawer(typeof(GameEventListener), true)]
    [CustomPropertyDrawer(typeof(GameEventListener<>), true)]
    public class SubAssetObjectFieldDrawer : PropertyDrawer
    {
        private VariableAsset m_object = default;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            m_object = property.objectReferenceValue as VariableAsset;
            m_object = EditorGUI.ObjectField(position, label, m_object, typeof(VariableAsset), false) as VariableAsset;

            if (m_object == null)
            {
                property.objectReferenceValue = null;
                return;
            }
            
            string path = AssetDatabase.GetAssetPath(m_object);
            m_object = null;
            foreach (var asset in AssetDatabase.LoadAllAssetsAtPath(path))
            {
                property.objectReferenceValue = asset;
                if (property.objectReferenceValue == null) continue;
                break;
            }
        }
    }
}
