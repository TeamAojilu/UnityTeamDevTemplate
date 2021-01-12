using System;
using SilCilSystem.ObjectPools;
using Object = UnityEngine.Object;

namespace SilCilSystem.Timers
{
    internal class CallRepeatedlyFactory
    {
        private ObjectPool<CallRepeatedlyObject> m_pool = new ObjectPool<CallRepeatedlyObject>(() => new CallRepeatedlyObject());

        public IUpdatable Create(Action method, float interval, Object lifeTimeObject, int repeatCount = -1, Func<bool> enabled = null)
        {
            var call = m_pool.GetInstance();
            call.Method = method;
            call.Interval = interval;
            call.LifeTimeObject = lifeTimeObject;
            call.RepeatCount = repeatCount;
            call.IsEnabled = enabled;
            return call;
        }
    }
}