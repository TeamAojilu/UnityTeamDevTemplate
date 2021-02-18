using SilCilSystem.Variables.Base;
using SilCilSystem.Variables.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace SilCilSystem.Editors
{
    [CustomEditor(typeof(VariableAsset), true), CanEditMultipleObjects]
    internal class VariableAssetEditor : Editor
    {
        private Dictionary<Editor, bool> m_activeEditors = new Dictionary<Editor, bool>();
        private VariableInspectorOrders m_orders = default;

        private bool IsMain()
        {
            if (target == null) return false;
            if (!(target is VariableAsset)) return false;
            if (!EditorUtility.IsPersistent(target)) return false;
            if (AssetDatabase.IsSubAsset(target)) return false;
            if (!AssetDatabase.IsMainAsset(target)) return false;
            return true;
        }

        private void OnEnable()
        {
            if (IsMain())
            {
                InitActiveEditors();
                Undo.undoRedoPerformed += InitActiveEditors;
            }
        }

        private void OnDisable()
        {
            ClearActiveEditors();
            Undo.undoRedoPerformed -= InitActiveEditors;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

<<<<<<< Updated upstream
            DrawHiddenSelializedProperty();
=======
            DrawHiddenSerializedProperty();
>>>>>>> Stashed changes

            if (!IsMain()) return;

            DrawHideFlagsMenu();

            if (targets.Length == 1)
            {
                List<Editor> editors = new List<Editor>(m_activeEditors.Keys);
                DrawSubAssetsInspector(editors);
                DrawLine();

                // 削除された要素がある場合は初期化.
                if (editors.Any(e => e.target == null))
                {
                    InitActiveEditors();
                    Repaint();
                }
            }
            else
            {
                DrawLine();
            }

            DrawDragDropArea();
        }

<<<<<<< Updated upstream
        private void DrawHiddenSelializedProperty()
=======
        private void DrawHiddenSerializedProperty()
>>>>>>> Stashed changes
        {
            EditorGUILayout.Space();

            serializedObject.Update();

            var restoreOnSceneChanged = serializedObject.FindProperty(nameof(Variable<int>.m_restoreOnSceneChanged)); // intじゃなくてもいい. nameofが使いたいだけ.
            if (restoreOnSceneChanged != null) EditorGUILayout.PropertyField(restoreOnSceneChanged);

            EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
            EditorGUILayout.LabelField("Editor", EditorStyles.boldLabel);

            var restore = serializedObject.FindProperty(nameof(Variable<int>.m_restoreValue));
            if (restore != null) EditorGUILayout.PropertyField(restore);

            var description = serializedObject.FindProperty(nameof(VariableAsset.m_description));
            if (description != null) EditorGUILayout.PropertyField(description);

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawDragDropArea()
        {
            //ドロップできる領域を確保
            var dragDropRect = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.ExpandHeight(true), GUILayout.MinHeight(200));
            GUI.Label(dragDropRect, "");

            // 追加ボタンを表示.
            DrawAddSubAssetMenu(dragDropRect);

            // イベント処理.
            if (dragDropRect.Contains(Event.current.mousePosition) == false) return;
            if (DragAndDrop.objectReferences == null) return;
            
            var attaches = DragAndDrop.objectReferences
                .Where(x => x.GetType() == typeof(MonoScript))
                .OfType<MonoScript>()
                .Select(m => m.GetClass())
                .ToArray();

            if (attaches.Length == 0) return;

            DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

            if (Event.current.type != EventType.DragPerform) return;

            DragAndDrop.AcceptDrag();
            Event.current.Use();
            foreach (var parent in targets)
            {
                if (parent is VariableAsset variable)
                {
                    variable.AddSubVariables(true, attaches);
                }
            }

            InitActiveEditors();
            Repaint();
        }

        private void DrawAddSubAssetMenu(Rect dragDropRect)
        {
            var buttonRect = new Rect(dragDropRect.x + 30f, dragDropRect.y + 30f, dragDropRect.width - 60f, 20f);
            if (!GUI.Button(buttonRect, "Add SubAsset")) return;

            VariableAttributeList.GetEnableTypes(target as VariableAsset, out List<string> paths, out List<Type> types);
            foreach(var t in targets.Skip(1))
            {
                VariableAttributeList.GetEnableTypes(t as VariableAsset, out List<string> ps, out List<Type> ts);
                for(int i = 0; i < paths.Count; i++)
                {
                    if (ts.Contains(types[i])) continue;
                    types.RemoveAt(i);
                    paths.RemoveAt(i);
                    i--;
                }
            }

            EditorMenuUtil.DisplayMenu(buttonRect, i => 
            { 
                foreach(var t in targets)
                {
                    (t as VariableAsset).AddSubVariables(true, types[i]);
                }
                InitActiveEditors();
                Repaint();
            }, paths.ToArray());
        }

        private void DrawSubAssetsInspector(List<Editor> editors)
        {
            foreach (var editor in editors)
            {
                if (editor.target == null) continue;

                DrawInspectorTitlebar(editor);

                EditorGUILayout.Space();
                if (!m_activeEditors[editor]) continue;

                // インスペクタ.
                editor.OnInspectorGUI();
                
                // 制御ボタン.
                EditorGUILayout.Space();
                if (DrawSubAssetButtons(editor.target)) return;

                EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
            }
        }

        private bool DrawSubAssetButtons(Object editorTarget)
        {
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Up"))
                {
                    m_orders?.MoveUp(target as VariableAsset, editorTarget as VariableAsset);
                    InitActiveEditors();
                    Repaint();
                    return true;
                }
                if (GUILayout.Button("Down"))
                {
                    m_orders?.MoveDown(target as VariableAsset, editorTarget as VariableAsset);
                    InitActiveEditors();
                    Repaint();
                    return true;
                }
                if (GUILayout.Button("Delete"))
                {
                    Undo.DestroyObjectImmediate(editorTarget);
                    return true;
                }
            }
            EditorGUILayout.EndHorizontal();
            return false;
        }

        private void DrawHideFlagsMenu()
        {
            EditorGUILayout.Space();

            EditorGUILayout.LabelField("SubAssets", EditorStyles.boldLabel);
            EditorGUILayout.BeginHorizontal();
            {
                GUILayout.FlexibleSpace();
                if (GUILayout.Button("Show"))
                {
                    foreach (var t in targets)
                    {
                        SetHideFlags(t, true);
                    }
                }
                if (GUILayout.Button("Hide"))
                {
                    foreach (var t in targets)
                    {
                        SetHideFlags(t, false);
                    }
                }
            }
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space(EditorGUIUtility.singleLineHeight);
        }

        private void ClearActiveEditors()
        {
            foreach (var activeEditor in m_activeEditors)
            {
                DestroyImmediate(activeEditor.Key);
            }
            m_activeEditors.Clear();
        }

        private void InitActiveEditors()
        {
            ClearActiveEditors();
            m_orders = m_orders ?? VariableInspectorOrders.GetInstance();
            foreach (var subasset in m_orders.GetOrderedSubAssets(target as VariableAsset))
            {
                m_activeEditors.Add(CreateEditor(subasset), true);
            }
        }
        
        private void SetHideFlags(Object target, bool show)
        {
            if (!(target is VariableAsset variable)) return;

            foreach (var subasset in variable.GetAllVariables())
            {
                if (subasset == target) continue;
                subasset.hideFlags = (show) ? HideFlags.None : HideFlags.HideInHierarchy;
            }
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(target));
        }

        private void DrawInspectorTitlebar(Editor editor)
        {
            var rect = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.Height(20));
            rect.x = 0;
            rect.y -= 5;
            rect.width += 20;
            m_activeEditors[editor] = EditorGUI.InspectorTitlebar(rect, m_activeEditors[editor], editor.target, true);
        }

        private void DrawLine()
        {
            EditorGUILayout.Space();
            var lineRect = GUILayoutUtility.GetRect(GUIContent.none, GUIStyle.none, GUILayout.Height(2));
            lineRect.y -= 3;
            lineRect.width += 20;
            Handles.color = Color.black;
            var start = new Vector2(0, lineRect.y);
            var end = new Vector2(lineRect.width, lineRect.y);
            Handles.DrawLine(start, end);
        }
    }
}