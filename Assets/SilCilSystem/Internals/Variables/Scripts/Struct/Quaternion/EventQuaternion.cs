using System;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    [Variable("Changed")]
    internal class EventQuaternion : GameEventQuaternion
    {
        private event Action<Quaternion> m_event = default;

        public override void Publish(Quaternion value)
        {
            m_event?.Invoke(value);
        }

        public override IDisposable Subscribe(Action<Quaternion> action)
        {
            m_event += action;
            return DelegateDispose.Create(() => m_event -= action);
        }
    }
}