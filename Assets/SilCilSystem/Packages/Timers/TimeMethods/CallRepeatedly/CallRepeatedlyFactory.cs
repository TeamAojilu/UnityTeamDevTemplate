using System;
using SilCilSystem.ObjectPools;

namespace SilCilSystem.Timers
{
    internal class CallRepeatedlyFactory
    {
        private ObjectPool<CallRepeatedlyObject> m_pool = new ObjectPool<CallRepeatedlyObject>(() => new CallRepeatedlyObject());

        public Func<float, bool> Create(Action method, float interval, int repeatCount, Func<bool> enabled)
        {
            var call = m_pool.GetInstance();
            call.SetParameters(method, interval, repeatCount, enabled: enabled);
            return call.Update;
        }
    }
}