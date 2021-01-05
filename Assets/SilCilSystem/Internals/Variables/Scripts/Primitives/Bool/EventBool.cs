using System;
using SilCilSystem.Editors;
using SilCilSystem.Variables;
using SilCilSystem.Variables.Base;

namespace SilCilSystem.Internals
{
    [Variable("Changed")]
    internal class EventBool : GameEventBool
    {
        private event Action<bool> m_event = default;

        public override void Publish(bool value)
        {
            m_event?.Invoke(value);
        }

        public override IDisposable Subscribe(Action<bool> action)
        {
            m_event += action;
            return DelegateDispose.Create(() => m_event -= action);
        }
    }
}