using System;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    [Variable("Listener", Constants.ListenerMenuPath + "(String)", typeof(GameEventString))]
    internal class EventStringListener : GameEventStringListener
    {
        [SerializeField] private GameEventString m_event = default;

        public override IDisposable Subscribe(Action<string> action) => m_event?.Subscribe(action);

        [OnAttached]
        private void OnAttached(VariableAsset parent)
        {
            m_event = parent.GetSubVariable<GameEventString>();
        }
    }
}