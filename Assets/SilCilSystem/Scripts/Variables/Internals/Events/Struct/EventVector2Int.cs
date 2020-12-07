using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class EventVector2Int : GameEventVector2Int
    {
        private event Action<Vector2Int> m_event;
        public override void Publish(Vector2Int arg) => m_event?.Invoke(arg);
        public override IDisposable Subscribe(Action<Vector2Int> action)
        {
            m_event += action;
            return DelegateDispose.Create(() => m_event -= action);
        }
    }
}