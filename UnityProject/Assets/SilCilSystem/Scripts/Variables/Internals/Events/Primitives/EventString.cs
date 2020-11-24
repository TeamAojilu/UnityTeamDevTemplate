using System;

namespace SilCilSystem.Variables
{
    internal class EventString : GameEventString
    {
        private event Action<string> m_event;
        public override void Publish(string arg) => m_event?.Invoke(arg);
        public override IDisposable Subscribe(Action<string> action)
        {
            m_event += action;
            return DelegateDispose.Create(() => m_event -= action);
        }
    }
}