using System;

namespace SilCilSystem.Variables
{
    internal class EventInt : GameEventInt
    {
        private event Action<int> m_event;
        public override void Publish(int arg) => m_event?.Invoke(arg);
        public override IDisposable Subscribe(Action<int> action)
        {
            m_event += action;
            return DelegateDispose.Create(() => m_event -= action);
        }
    }
}