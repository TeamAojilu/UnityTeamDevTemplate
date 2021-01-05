using System;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    [Variable("Listener", Constants.ListenerMenuPath + "(Vector3)", typeof(GameEventVector3))]
    internal class EventVector3Listener : GameEventVector3Listener
    {
        [SerializeField] private GameEventVector3 m_event = default;

        public override IDisposable Subscribe(Action<Vector3> action) => m_event?.Subscribe(action);

        [OnAttached]
        private void OnAttached(VariableAsset parent)
        {
            m_event = parent.GetSubVariable<GameEventVector3>();
        }
    }
}