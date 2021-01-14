using System;
using System.Diagnostics;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals.Variables
{
    [Variable("Listener", Constants.ListenerMenuPath + "(Float)", typeof(GameEventFloat))]
    internal class EventFloatListener : GameEventFloatListener
    {
        [SerializeField, NonEditable] private GameEventFloat m_event = default;

        public override IDisposable Subscribe(Action<float> action) => m_event?.Subscribe(action);

        [OnAttached, Conditional("UNITY_EDITOR")]
        private void OnAttached(VariableAsset parent)
        {
            m_event = parent.GetSubVariable<GameEventFloat>();
        }
    }
}