using System;
using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;

namespace SilCilSystem.Interernals
{
    internal class EventVector2 : GameEventVector2
    {
        private event Action<Vector2> m_event = default;

        public override void Publish(Vector2 value)
        {
            m_event?.Invoke(value);
        }

        public override IDisposable Subscribe(Action<Vector2> action)
        {
            m_event += action;
            return DelegateDispose.Create(() => m_event -= action);
        }

        public override void GetAssetName(ref string name) => name = $"{name}_OnChanged";
        public override void OnAttached(IEnumerable<VariableAsset> variables) { }
    }
}