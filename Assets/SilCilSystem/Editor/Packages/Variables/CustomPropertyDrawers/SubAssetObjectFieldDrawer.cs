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
    internal class SubAssetObjectFieldDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.BeginChangeCheck();

            var obj = property.objectReferenceValue as VariableAsset;
            obj = EditorGUI.ObjectField(position, label, obj, typeof(VariableAsset), false) as VariableAsset;
            
            if (EditorGUI.EndChangeCheck())
            {
                if (obj == null)
                {
                    property.objectReferenceValue = null;
                    return;
                }

                foreach (var asset in obj.GetAllVariables())
                {
                    property.objectReferenceValue = asset;
                    if (property.objectReferenceValue != null) break;
                }
            }
            EditorGUI.EndProperty();
        }
    }
}
