using System;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;

namespace SilCilSystem.Internals
{
    internal class EventColor : GameEventColor
    {
        private event Action<Color> m_event = default;

        public override void Publish(Color value)
        {
            m_event?.Invoke(value);
        }

        public override IDisposable Subscribe(Action<Color> action)
        {
            m_event += action;
            return DelegateDispose.Create(() => m_event -= action);
        }
        
        public override void GetAssetName(ref string name) => name = $"{name}_OnChanged";
        public override void OnAttached(VariableAsset parent) { }
    }
}