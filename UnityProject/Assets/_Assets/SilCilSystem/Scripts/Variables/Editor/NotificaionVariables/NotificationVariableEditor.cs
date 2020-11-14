using UnityEngine;
using UnityEditor;
using SilCilSystem.Variables;

#if UNITY_EDITOR
namespace SilCilSystem.Editors
{
    public class NotificationVariableEditor<TNotification, TReadonly, TEvent, TAbstractVariable, TAbstractEvent> : Editor
        where TNotification : TAbstractVariable, IGameEventSetter<TAbstractEvent>
        where TReadonly : ScriptableObject, IVariableSetter<TAbstractVariable>
        where TEvent : TAbstractEvent
        where TAbstractVariable : ScriptableObject
        where TAbstractEvent : ScriptableObject
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            // 永続化されていないならスキップ.
            if (!EditorUtility.IsPersistent(target)) return;

            // アセットを検索.
            var path = AssetDatabase.GetAssetPath(target);
            var notificationAsset = target as TNotification;
            var readonlyAsset = AssetDatabase.LoadAssetAtPath<TReadonly>(path);
            var eventAsset = AssetDatabase.LoadAssetAtPath<TEvent>(path);

            // 存在しないものを作成していく.

            if(readonlyAsset == null)
            {
                readonlyAsset = CreateInstance<TReadonly>();
                readonlyAsset.SetVariable(notificationAsset);
                readonlyAsset.hideFlags = HideFlags.NotEditable;
                AssetDatabase.AddObjectToAsset(readonlyAsset, target);
                serializedObject.ApplyModifiedProperties();
                AssetDatabase.SaveAssets();
            }

            if (eventAsset == null)
            {
                eventAsset = CreateInstance<TEvent>();
                eventAsset.hideFlags = HideFlags.NotEditable;
                notificationAsset.SetGameEvent(eventAsset);
                AssetDatabase.AddObjectToAsset(eventAsset, target);
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

            if (eventAsset.name != $"On{target.name}Changed")
            {
                eventAsset.name = $"On{target.name}Changed";
                EditorUtility.SetDirty(eventAsset);
                EditorUtility.SetDirty(target);
                AssetDatabase.ImportAsset(path);
            }

            base.OnInspectorGUI();
        }
    }

    [CustomEditor(typeof(NotificationInt))]
    internal class NotificationIntEditor : NotificationVariableEditor<NotificationInt, ReadonlyIntValue, EventInt, VariableInt, GameEventInt> { }

    [CustomEditor(typeof(NotificationBool))]
    internal class NotificationBoolEditor : NotificationVariableEditor<NotificationBool, ReadonlyBoolValue, EventBool, VariableBool, GameEventBool> { }

    [CustomEditor(typeof(NotificationString))]
    internal class NotificationStringEditor : NotificationVariableEditor<NotificationString, ReadonlyStringValue, EventString, VariableString, GameEventString> { }

    [CustomEditor(typeof(NotificationFloat))]
    internal class NotificationFloatEditor : NotificationVariableEditor<NotificationFloat, ReadonlyFloatValue, EventFloat, VariableFloat, GameEventFloat> { }
}
#endif