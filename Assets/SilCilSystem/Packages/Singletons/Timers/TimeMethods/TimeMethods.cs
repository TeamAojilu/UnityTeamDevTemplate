using System;
using SilCilSystem.ObjectPools;
using Object = UnityEngine.Object;

namespace SilCilSystem.Timers
{
    public static class TimeMethods
    {
        private static ObjectPool<ManualTimer> m_timerPool = new ObjectPool<ManualTimer>(() => new ManualTimer());

        public static void CallDelayed(Action method, float delay, Object lifeTimeObject, UpdateMode updateMode = UpdateMode.DeltaTime)
        { 
            var timer = m_timerPool.GetInstance();
            timer.IsFinished = t =>
            {
                if (lifeTimeObject == null) return true;
                if (t < delay) return false;
                method?.Invoke();
                return true;
            };
            UpdateDispatcher.Register(timer, updateMode);
        }

        public static void CallRepeatedly(Action method, float interval, Object lifeTimeObject, UpdateMode updateMode = UpdateMode.DeltaTime, int repeatCount = -1, Func<bool> enabled = null)
        { 
            int count = 0;
            var timer = m_timerPool.GetInstance();
            timer.IsEnabled = enabled;
            timer.IsFinished = t =>
            {
                if (lifeTimeObject == null) return true;
                if (repeatCount >= 0 && count >= repeatCount) return true;
                if (t < interval) return false;

                timer.Time.Value = 0f;
                method?.Invoke();
                count++;
                return false;
            };
            UpdateDispatcher.Register(timer, updateMode);
        }

        public static void MeasureTime(Action<float> update, Object lifeTimeObject, UpdateMode updateMode = UpdateMode.DeltaTime, Func<bool> enabled = null, Func<float> timeScale = null)
        {
            var timer = m_timerPool.GetInstance();
            timer.IsEnabled = enabled;
            timer.TimeScale = timeScale;
            timer.IsFinished = t => 
            {
                update.Invoke(t);
                return lifeTimeObject == null;
            };
            UpdateDispatcher.Register(timer, updateMode);
        }
    }
}