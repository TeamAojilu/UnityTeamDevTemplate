using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEditor;
using SilCilSystem.Variables.Base;

namespace SilCilSystem.Editors
{
    // [CreateAssetMenu]
    internal class VariableInspectorOrders : ScriptableObject
    {
        // GUIDで指定. フォルダ構成をいじられても動くように.
        private const string VariableInspectorOrdersID = "632d6d46c658303489412669cd0c2a10";

        public static VariableInspectorOrders GetInstance()
        {
            var path = AssetDatabase.GUIDToAssetPath(VariableInspectorOrdersID);
            return AssetDatabase.LoadAssetAtPath<VariableInspectorOrders>(path);
        }

        [System.Serializable]
        private class OrderInfo
        {
            public int m_instanceID = default;
            public int m_order = default;

            public OrderInfo(int instanceID, int order)
            {
                m_instanceID = instanceID;
                m_order = order;
            }
        }

        [SerializeField] private List<OrderInfo> m_orders = new List<OrderInfo>();

        private OrderInfo this[int id] => m_orders.FirstOrDefault(x => x.m_instanceID == id);

        public static void RemoveNull()
        {
            var instance = GetInstance();
            if (instance == null) return;
            instance.m_orders = instance.m_orders.Where(x => EditorUtility.InstanceIDToObject(x.m_instanceID) != null).ToList();
        }

        private VariableAsset[] Sort(IEnumerable<VariableAsset> variables)
        {
            bool import = false;
            var array = variables.OrderBy(x => 
            {
                var order = this[x.GetInstanceID()];
                if(order == null)
                {
                    order = new OrderInfo(x.GetInstanceID(), (AssetDatabase.IsMainAsset(x)) ? -1 : m_orders.Count);
                    m_orders.Add(order);
                    import = true;
                }
                return order.m_order;
            }).ToArray();
            if (import) ImportThis();
            return array;
        }

        public VariableAsset[] GetOrderedSubAssets(VariableAsset parent, bool includeParent = false)
        {
            string path = AssetDatabase.GetAssetPath(parent);
            var assets = AssetDatabase.LoadAllAssetsAtPath(path).Select(x => x as VariableAsset).Where(x => x != null && includeParent || !AssetDatabase.IsMainAsset(x));
            return Sort(assets);
        }

        private void ImportThis()
        {
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(this.GetInstanceID()));
        }

        public void MoveUp(VariableAsset parent, VariableAsset target)
        {
            Undo.RecordObject(this, $"MoveUp {target.name}");
            VariableAsset[] ordered = GetOrderedSubAssets(parent);

            for (int i = 0; i < ordered.Length; i++)
            {
                if (i != 0 && ordered[i] == target)
                {
                    this[ordered[i - 1].GetInstanceID()].m_order = i;
                    this[ordered[i].GetInstanceID()].m_order = i - 1;
                }
                else
                {
                    this[ordered[i].GetInstanceID()].m_order = i;
                }
            }
            ImportThis();
        }
        
        public void MoveDown(VariableAsset parent, VariableAsset target)
        {
            Undo.RecordObject(this, $"MoveDown {target.name}");
            var ordered = GetOrderedSubAssets(parent);

            for (int i = 0; i < ordered.Length; i++)
            {
                if (i != ordered.Length - 1 && ordered[i] == target)
                {
                    this[ordered[i].GetInstanceID()].m_order = i + 1;
                    this[ordered[i + 1].GetInstanceID()].m_order = i;
                    i++;
                }
                else
                {
                    this[ordered[i].GetInstanceID()].m_order = i;
                }
            }
            ImportThis();
        }
    }
}