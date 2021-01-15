using System;
using System.Diagnostics;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals.Variables
{
    [Variable("Listener", Constants.ListenerMenuPath + "(Vector3Int)", typeof(GameEventVector3Int))]
    internal class EventVector3IntListener : GameEventVector3IntListener
    {
        [SerializeField, NonEditable] private GameEventVector3Int m_event = default;

        public override IDisposable Subscribe(Action<Vector3Int> action) => m_event?.Subscribe(action);

        [OnAttached, Conditional("UNITY_EDITOR")]
        private void OnAttached(VariableAsset parent)
        {
            m_event = parent.GetSubVariable<GameEventVector3Int>();
        }
    }
}