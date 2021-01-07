using System;
using UnityEngine;

namespace SilCilSystem.Timers
{
    internal class ClampedTimer : IDecoratedTimer
    {
        public float CurrentTime => Mathf.Clamp(m_timer.CurrentTime, Min, Max);
        public bool Enabled => m_timer != null && m_timer.Enabled;
        public bool IsPooled => m_timer == null;

        /// <summary>timer.Update(deltaTime) == false | IsFinished?.Invoke(CurrentTime) == true で終了</summary>
        public Func<float, bool> IsFinished { get; set; } = null;
        
        public float Min { get; set; } = 0f;
        public float Max { get; set; } = float.MaxValue;

        private ITimerObject m_timer = default;

        public void Reset()
        {
            Min = 0f;
            Max = float.MaxValue;
        }

        public void ToPool()
        {
            m_timer?.ToPool();
            m_timer = null;
            IsFinished = null;
        }

        public void SetTimer(ITimerObject timer) => m_timer = timer;

        public bool Update(float deltaTime)
        {
            if (IsPooled) return false;

            // 2項目のInvokeも呼びたいので||ではなく|を使用.
            if(m_timer.Update(deltaTime) == false | IsFinished?.Invoke(CurrentTime) == true)
            {
                ToPool();
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}