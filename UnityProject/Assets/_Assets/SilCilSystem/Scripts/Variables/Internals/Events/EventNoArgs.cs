using System;

namespace SilCilSystem.Variables
{
    internal class EventNoArgs : GameEvent
    {
        private event Action m_event;
        public override void Publish() => m_event?.Invoke();
        public override IDisposable Subscribe(Action action)
        {
            m_event += action;
            return new DelegateDispose(() => m_event -= action);
        }
    }
}