using System;
using SilCilSystem.Variables;
using SilCilSystem.Editors;

namespace SilCilSystem.Internals.Variables
{
    [Variable("Changed")]
    internal class EventString : GameEventString
    {
        private event Action<string> m_event = default;

        public override void Publish(string value)
        {
            m_event?.Invoke(value);
        }

        public override IDisposable Subscribe(Action<string> action)
        {
            m_event += action;
            return DelegateDispose.Create(() => m_event -= action);
        }
    }
}