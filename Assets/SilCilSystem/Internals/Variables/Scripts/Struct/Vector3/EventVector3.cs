using System;
using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals
{
    [Variable("Changed")]
    internal class EventVector3 : GameEventVector3
    {
        private event Action<Vector3> m_event = default;

        public override void Publish(Vector3 value)
        {
            m_event?.Invoke(value);
        }

        public override IDisposable Subscribe(Action<Vector3> action)
        {
            m_event += action;
            return DelegateDispose.Create(() => m_event -= action);
        }
    }
}