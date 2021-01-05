using System;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    [Variable("Listener", Constants.ListenerMenuPath + "(Vector2)", typeof(GameEventVector2))]
    internal class EventVector2Listener : GameEventVector2Listener
    {
        [SerializeField] private GameEventVector2 m_event = default;

        public override IDisposable Subscribe(Action<Vector2> action) => m_event?.Subscribe(action);

        [OnAttached]
        private void OnAttached(VariableAsset parent)
        {
            m_event = parent.GetSubVariable<GameEventVector2>();
        }
    }
}