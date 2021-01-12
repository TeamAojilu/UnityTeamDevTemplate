using System;
using Object = UnityEngine.Object;

namespace SilCilSystem.Timers
{
    public static class TimeMethods
    {
        private static TimerFactory m_factory = new TimerFactory();

        public static void CallDelayed(Action method, float delay, Object lifeTimeObject, UpdateMode updateMode = UpdateMode.DeltaTime)
            => m_factory.CallDelayed(method, delay, lifeTimeObject, updateMode: updateMode);

        public static void CallRepeatedly(Action method, float interval, Object lifeTimeObject, UpdateMode updateMode = UpdateMode.DeltaTime, int repeatCount = -1, Func<bool> enabled = null)
            => m_factory.CallRepeatedly(method, interval, lifeTimeObject, updateMode: updateMode, repeatCount: repeatCount, enabled: enabled);

        public static void MeasureTime(Action<float> update, Object lifeTimeObject, UpdateMode updateMode = UpdateMode.DeltaTime, Func<bool> enabled = null, Func<float> timeScale = null)
            => m_factory.MeasureTime(update, lifeTimeObject, updateMode: updateMode, enabled: enabled, timeScale: timeScale);
    }
}