using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class EventQuaternion : GameEventQuaternion
    {
        private event Action<Quaternion> m_event;
        public override void Publish(Quaternion arg) => m_event?.Invoke(arg);
        public override IDisposable Subscribe(Action<Quaternion> action)
        {
            m_event += action;
            return DelegateDispose.Create(() => m_event -= action);
        }
    }
}