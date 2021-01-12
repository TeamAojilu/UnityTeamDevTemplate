using System;
using SilCilSystem.ObjectPools;
using Object = UnityEngine.Object;

namespace SilCilSystem.Timers
{
    internal class CallDelayedFactory
    {
        private ObjectPool<CallDelayedObject> m_pool = new ObjectPool<CallDelayedObject>(() => new CallDelayedObject());

        public IUpdatable Create(Action method, float delay, Object lifeTimeObject, Func<bool> isEnabled = null)
        {
            var call = m_pool.GetInstance();
            call.CallBack = method;
            call.Delay = delay;
            call.LifeTimeObject = lifeTimeObject;
            return call;
        }
    }
}