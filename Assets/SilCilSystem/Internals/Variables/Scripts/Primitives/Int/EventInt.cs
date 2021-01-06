using System;
using SilCilSystem.Editors;
using SilCilSystem.Variables;

namespace SilCilSystem.Internals.Variables
{
    [Variable("Changed")]
    internal class EventInt : GameEventInt
    {
        private event Action<int> m_event = default;

        public override void Publish(int value)
        {
            m_event?.Invoke(value);
        }

        public override IDisposable Subscribe(Action<int> action)
        {
            m_event += action;
            return DelegateDispose.Create(() => m_event -= action);
        }
    }
}