using System;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    [AddSubAssetMenu(Constants.ListenerMenuPath + "(Int)", typeof(GameEventIntListener))]
    internal class EventIntListener : GameEventIntListener
    {
        [SerializeField] private GameEventInt m_event = default;

        public override IDisposable Subscribe(Action<int> action) => m_event?.Subscribe(action);

        public override void GetAssetName(ref string name) => name = $"{name}_Listener";
        public override void OnAttached(VariableAsset parent)
        {
            m_event = parent.GetSubVariable<GameEventInt>();
        }
    }
}