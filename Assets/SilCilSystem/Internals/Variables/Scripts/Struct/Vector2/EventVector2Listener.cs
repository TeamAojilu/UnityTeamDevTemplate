﻿using System;
using System.Diagnostics;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals.Variables
{
    [Variable("Listener", Constants.ListenerMenuPath + "(Vector2)", typeof(GameEventVector2))]
    internal class EventVector2Listener : GameEventVector2Listener
    {
        [SerializeField, NonEditable] private GameEventVector2 m_event = default;

        public override IDisposable Subscribe(Action<Vector2> action) => m_event?.Subscribe(action);

        [OnAttached, Conditional("UNITY_EDITOR")]
        private void OnAttached(VariableAsset parent)
        {
            m_event = parent.GetSubVariable<GameEventVector2>();
        }
    }
}