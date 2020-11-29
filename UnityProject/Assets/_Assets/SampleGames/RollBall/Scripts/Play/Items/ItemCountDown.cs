using SilCilSystem.Variables;
using System;
using UnityEngine;

namespace Samples.RollBall
{
    public class ItemCountDown : MonoBehaviour
    {
        [SerializeField] private VariableInt m_itemCountDown = default;
        [SerializeField] private GameEventListener m_onItemPicked = default;

        private IDisposable m_unsubscriber = default;

        private void Start()
        {
            m_itemCountDown.Value = GetComponentsInChildren<IItem>().Length;
            m_unsubscriber = m_onItemPicked?.Subscribe(OnItemPicked);
        }

        private void OnItemPicked()
        {
            m_itemCountDown.Value--;
        }

        private void OnDestroy()
        {
            m_unsubscriber?.Dispose();
        }
    }
}