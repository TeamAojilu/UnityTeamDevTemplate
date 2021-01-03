using System;
using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;

namespace SilCilSystem.Internals
{
    internal class EventVector3IntListener : GameEventVector3IntListener
    {
        [SerializeField, HideInInspector] private GameEventVector3Int m_event = default;

        public override IDisposable Subscribe(Action<Vector3Int> action) => m_event?.Subscribe(action);

        public override void GetAssetName(ref string name) => name = $"{name}_Listener";
        public override void OnAttached(IEnumerable<VariableAsset> variables)
        {
            foreach (var variable in variables)
            {
                if (!(variable is GameEventVector3Int onChanged)) continue;
                m_event = onChanged;
                return;
            }
        }
    }
}