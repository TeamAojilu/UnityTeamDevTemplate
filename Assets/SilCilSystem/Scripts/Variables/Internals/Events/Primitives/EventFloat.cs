using System;

namespace SilCilSystem.Variables
{
    internal class EventFloat : GameEventFloat
    {
        private event Action<float> m_event;
        public override void Publish(float arg) => m_event?.Invoke(arg);
        public override IDisposable Subscribe(Action<float> action)
        {
            m_event += action;
            return DelegateDispose.Create(() => m_event -= action);
        }
    }
}