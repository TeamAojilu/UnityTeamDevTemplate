using System;
using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;

namespace SilCilSystem.Internals
{
    internal class EventNoArgsListener : GameEventListener
    {
        [SerializeField, HideInInspector] private GameEvent m_event = default;
        public override IDisposable Subscribe(Action action) => m_event?.Subscribe(action);

        public override void GetAssetName(ref string name) => name = $"{name}_Listener";
        public override void OnAttached(IEnumerable<VariableAsset> variables)
        {
            foreach (var variable in variables)
            {
                if (!(variable is GameEvent gameEvent)) continue;
                m_event = gameEvent;
                return;
            }
        }
    }
}