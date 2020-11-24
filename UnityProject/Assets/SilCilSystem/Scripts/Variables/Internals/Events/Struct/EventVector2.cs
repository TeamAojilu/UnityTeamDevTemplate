using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class EventVector2 : GameEventVector2
    {
        private event Action<Vector2> m_event;
        public override void Publish(Vector2 arg) => m_event?.Invoke(arg);
        public override IDisposable Subscribe(Action<Vector2> action)
        {
            m_event += action;
            return DelegateDispose.Create(() => m_event -= action);
        }
    }
}