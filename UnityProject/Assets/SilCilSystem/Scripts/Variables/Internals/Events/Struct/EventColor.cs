using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class EventColor : GameEventColor
    {
        private event Action<Color> m_event;
        public override void Publish(Color arg) => m_event?.Invoke(arg);
        public override IDisposable Subscribe(Action<Color> action)
        {
            m_event += action;
            return DelegateDispose.Create(() => m_event -= action);
        }
    }
}