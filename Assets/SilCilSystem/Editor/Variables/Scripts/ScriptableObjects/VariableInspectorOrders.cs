using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using SilCilSystem.Variables.Base;
using System.Linq;

namespace SilCilSystem.Editors
{
    // [CreateAssetMenu]
    internal class VariableInspectorOrders : ScriptableObject
    {
        public static VariableInspectorOrders GetInstance()
        {
            var path = AssetDatabase.GUIDToAssetPath(Constants.VariableInspectorOrdersID);
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
            instance.m_orders = instance.m_orders.Where(x => EditorUtility.InstanceIDToObject(x.m_instanceID) != null).ToList();
        }

        public IEnumerable<VariableAsset> Sort(IEnumerable<VariableAsset> variables)
        {
            return variables.OrderBy(x => 
            {
                var order = this[x.GetInstanceID()];
                if(order == null)
                {
                    order = new OrderInfo(x.GetInstanceID(), m_orders.Count);
                    m_orders.Add(order);
                }
                return order.m_order;
            });
        }

        public VariableAsset[] GetOrderedSubAssets(VariableAsset parent)
        {
            var assets = parent.GetAllVariables().Where(x => !AssetDatabase.IsMainAsset(x));
            var ordered = Sort(assets).ToArray();
            return ordered;
        }

        private void ImportThis()
        {
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(this.GetInstanceID()));
        }

        public void MoveUp(VariableAsset parent, VariableAsset target)
        {
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