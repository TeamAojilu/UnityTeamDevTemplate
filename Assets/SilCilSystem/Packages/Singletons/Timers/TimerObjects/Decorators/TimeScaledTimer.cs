using System;

namespace SilCilSystem.Timers
{
    internal class TimeScaledTimer : IDecoratedTimer
    {
        public float CurrentTime => m_timer.CurrentTime;
        public bool Enabled => m_timer != null && m_timer.Enabled;
        public bool IsPooled => m_timer == null;

        /// <summary>使用しない. SetTimerで指定するTimer.IsFinishedで終了判定</summary>
        [Obsolete] public Func<float, bool> IsFinished { get => null; set { } }

        /// <summary>nullなら1f</summary>
        public Func<float> TimeScale { get; set; } = null;
        private ITimerObject m_timer = null;

        public void Reset() { }
        
        public void ToPool()
        {
            m_timer?.ToPool();
            TimeScale = null;
            m_timer = null;
        }

        public void SetTimer(ITimerObject timer) => m_timer = timer;

        public bool Update(float deltaTime)
        {
            if (IsPooled) return false;

            if (m_timer.Update(deltaTime * (TimeScale?.Invoke() ?? 1f))) return true;
            ToPool();
            return false;
        }
    }
}