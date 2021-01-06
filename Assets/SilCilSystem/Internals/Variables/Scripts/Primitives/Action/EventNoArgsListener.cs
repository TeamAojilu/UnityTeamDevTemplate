using System;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals.Variables
{
    [Variable("Listener", Constants.ListenerMenuPath + "(Action)", typeof(GameEvent))]
    internal class EventNoArgsListener : GameEventListener
    {
        [SerializeField] private GameEvent m_event = default;
        public override IDisposable Subscribe(Action action) => m_event?.Subscribe(action);

        [OnAttached]
        private void OnAttached(VariableAsset parent)
        {
            m_event = parent.GetSubVariable<GameEvent>();
        }
    }
}