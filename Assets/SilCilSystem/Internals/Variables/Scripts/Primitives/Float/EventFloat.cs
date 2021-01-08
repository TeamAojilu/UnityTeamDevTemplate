using System;
using SilCilSystem.Variables;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals.Variables
{
    [Variable("Changed")]
    internal class EventFloat : GameEventFloat
    {
        private event Action<float> m_event = default;

        public override void Publish(float value)
        {
            m_event?.Invoke(value);
        }

        public override IDisposable Subscribe(Action<float> action)
        {
            m_event += action;
            return DelegateDispose.Create(() => m_event -= action);
        }
    }
}