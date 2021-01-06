using System;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals.Variables
{
    [Variable("Changed")]
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
    }
}