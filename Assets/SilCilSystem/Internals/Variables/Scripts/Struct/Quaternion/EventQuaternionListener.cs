using System;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    [Variable("Listener", Constants.ListenerMenuPath + "(Quaternion)", typeof(GameEventQuaternion))]
    internal class EventQuaternionListener : GameEventQuaternionListener
    {
        [SerializeField] private GameEventQuaternion m_event = default;

        public override IDisposable Subscribe(Action<Quaternion> action) => m_event?.Subscribe(action);

        [OnAttached]
        private void OnAttached(VariableAsset parent)
        {
            m_event = parent.GetSubVariable<GameEventQuaternion>();
        }
    }
}