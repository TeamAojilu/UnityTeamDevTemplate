using System;
using Object = UnityEngine.Object;

namespace SilCilSystem.Timers
{
    public static class TimeMethods
    {
        private static CallDelayedFactory m_callDelayedFactory = new CallDelayedFactory();
        private static CallRepeatedlyFactory m_callRepeatedlyFactory = new CallRepeatedlyFactory();
        
        public static void CallDelayed(Action method, float delay, Object lifeTimeObject, UpdateMode updateMode = UpdateMode.DeltaTime)
        {
            var call = m_callDelayedFactory.Create(method, delay);
            UpdateDispatcher.Register(call, lifeTimeObject, updateMode);
        }

        public static void CallRepeatedly(Action method, float interval, Object lifeTimeObject, UpdateMode updateMode = UpdateMode.DeltaTime, int repeatCount = -1, Func<bool> enabled = null)
        {
            var call = m_callRepeatedlyFactory.Create(method, interval, repeatCount: repeatCount, enabled: enabled);
            UpdateDispatcher.Register(call, lifeTimeObject, updateMode);
        }
    }
}