﻿using System;
using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    [AddSubAssetMenu(VariablePath.ListenerMenuPath + "(Vector3Int)", typeof(GameEventVector3Int))]
    internal class EventVector3IntListener : GameEventVector3IntListener
    {
        [SerializeField] private GameEventVector3Int m_event = default;

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