﻿using System;
using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    [AddSubAssetMenu(VariablePath.ListenerMenuPath + "(Bool)", typeof(GameEventBool))]
    internal class EventBoolListener : GameEventBoolListener
    {
        [SerializeField] private GameEventBool m_event = default;

        public override IDisposable Subscribe(Action<bool> action) => m_event?.Subscribe(action);

        public override void GetAssetName(ref string name) => name = $"{name}_Listener";
        public override void OnAttached(IEnumerable<VariableAsset> variables)
        {
            foreach (var variable in variables)
            {
                if (!(variable is GameEventBool onChanged)) continue;
                m_event = onChanged;
                return;
            }
        }
    }
}