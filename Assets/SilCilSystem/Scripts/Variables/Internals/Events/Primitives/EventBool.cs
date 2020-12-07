using System;

namespace SilCilSystem.Variables
{
    internal class EventBool : GameEventBool
    {
        private event Action<bool> m_event;
        public override void Publish(bool arg) => m_event?.Invoke(arg);
        public override IDisposable Subscribe(Action<bool> action)
        {
            m_event += action;
            return DelegateDispose.Create(() => m_event -= action);
        }
    }
}