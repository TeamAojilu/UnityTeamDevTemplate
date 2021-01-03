﻿using UnityEngine;
using System.Collections.Generic;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;

namespace SilCilSystem.Internals
{
    internal class NotificationVector2Int : VariableVector2Int
    {
        [SerializeField] private Vector2Int m_value = default;
        [SerializeField] private GameEventVector2Int m_onValueChanged = default;

        public override void GetAssetName(ref string name) => name = $"{name}_Variable";
        public override void OnAttached(IEnumerable<VariableAsset> variables)
        {
            foreach (var variable in variables)
            {
                if (variable is GameEventVector2Int onChanged)
                {
                    m_onValueChanged = onChanged;
                    return;
                }
            }
        }

        public override Vector2Int Value
        {
            get => m_value;
            set
            {
                m_value = value;
                m_onValueChanged?.Publish(m_value);
            }
        }
    }
}