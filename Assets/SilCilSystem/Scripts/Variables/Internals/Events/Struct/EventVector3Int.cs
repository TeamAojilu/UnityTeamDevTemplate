using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class EventVector3Int : GameEventVector3Int
    {
        private event Action<Vector3Int> m_event;
        public override void Publish(Vector3Int arg) => m_event?.Invoke(arg);
        public override IDisposable Subscribe(Action<Vector3Int> action)
        {
            m_event += action;
            return DelegateDispose.Create(() => m_event -= action);
        }
    }
}