using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class EventVector3 : GameEventVector3
    {
        private event Action<Vector3> m_event;
        public override void Publish(Vector3 arg) => m_event?.Invoke(arg);
        public override IDisposable Subscribe(Action<Vector3> action)
        {
            m_event += action;
            return DelegateDispose.Create(() => m_event -= action);
        }
    }
}