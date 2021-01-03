using System;
using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    [AddSubAssetMenu(VariablePath.ListenerMenuPath + "(Int)", typeof(GameEventIntListener))]
    internal class EventIntListener : GameEventIntListener
    {
        [SerializeField] private GameEventInt m_event = default;

        public override IDisposable Subscribe(Action<int> action) => m_event?.Subscribe(action);

        public override void GetAssetName(ref string name) => name = $"{name}_Listener";
        public override void OnAttached(IEnumerable<VariableAsset> variables)
        {
            foreach (var variable in variables)
            {
                if (!(variable is GameEventInt onChanged)) continue;
                m_event = onChanged;
                return;
            }
        }
    }
}