using System;
using SilCilSystem.ObjectPools;

namespace SilCilSystem.Timers
{
    internal class CallDelayedFactory
    {
        private ObjectPool<CallDelayedObject> m_pool = new ObjectPool<CallDelayedObject>(() => new CallDelayedObject());

        public Func<float, bool> Create(Action method, float delay, Func<bool> isEnabled = null)
        {
            var call = m_pool.GetInstance();
            call.SetParameter(method, delay, isEnabled);
            return call.Update;
        }
    }
}