using SilCilSystem.ObjectPools;
using System;
using Object = UnityEngine.Object;

namespace SilCilSystem.Timers
{
    internal class TimerFactory
    {
        private ObjectPool<SimpleTimer> m_simpleTimerPool = new ObjectPool<SimpleTimer>(() => new SimpleTimer());
        private ObjectPool<TimeScaledTimer> m_timeScalePool = new ObjectPool<TimeScaledTimer>(() => new TimeScaledTimer());
        
        public void CallDelayed(Action method, float delay, Object lifeTimeObject, UpdateMode updateMode = UpdateMode.DeltaTime, Func<bool> enabled = null)
        {
            var timer = m_simpleTimerPool.GetInstance();
            timer.Reset();
            timer.IsEnabled = enabled;
            timer.IsFinished = t => IsFinishedForDelay(t, lifeTimeObject, method, delay);
            UpdateDispatcher.Register(timer, updateMode);
        }

        public void CallRepeatedly(Action method, float interval, Object lifeTimeObject, UpdateMode updateMode = UpdateMode.DeltaTime, int repeatCount = -1, Func<bool> enabled = null)
        {
            CallDelayed(() => 
            {
                method?.Invoke();
                if (repeatCount != 0)
                {
                    CallRepeatedly(method, interval, lifeTimeObject, updateMode: updateMode, repeatCount - 1, enabled: enabled);
                }
            }, interval, lifeTimeObject, updateMode: updateMode, enabled: enabled);
        }

        public void MeasureTime(Action<float> update, Object lifeTimeObject, UpdateMode updateMode = UpdateMode.DeltaTime, Func<bool> enabled = null, Func<float> timeScale = null)
        {
            var timer = m_simpleTimerPool.GetInstance();
            timer.Reset();
            timer.IsEnabled = enabled;
            timer.IsFinished = t =>
            {
                update.Invoke(t);
                return lifeTimeObject == null;
            };

            var scaleTimer = m_timeScalePool.GetInstance();
            scaleTimer.Reset();
            scaleTimer.SetTimer(timer);
            scaleTimer.TimeScale = timeScale;

            UpdateDispatcher.Register(scaleTimer, updateMode);
        }

        private static bool IsFinishedForDelay(float time, Object lifeTimeObject, Action method, float delay)
        {
            if (lifeTimeObject == null) return true;
            if (time < delay) return false;
            method?.Invoke();
            return true;
        }
    }
}