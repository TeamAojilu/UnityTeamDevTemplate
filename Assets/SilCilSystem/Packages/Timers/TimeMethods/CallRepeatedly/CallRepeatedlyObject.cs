using System;
using SilCilSystem.ObjectPools;

namespace SilCilSystem.Timers
{
    internal class CallRepeatedlyObject : IPooledObject
    {
        private float m_time = 0f;
        private int m_count = 0;

        public Action Method { get; private set; }
        public float Interval { get; set; }
        /// <summary>負なら無限</summary>
        public int RepeatCount { get; set; }
        /// <summary>nullならtrue扱い</summary>
        public Func<bool> IsEnabled { get; private set; }
        public bool IsPooled { get; private set; }

        public bool Update(float deltaTime)
        {
            if (IsPooled) return false;
            if (IsEnabled?.Invoke() == false) return true;

            m_time += deltaTime;
            if (m_time < Interval) return true;

            Method?.Invoke();
            m_count++;

            if(RepeatCount >= 0 && m_count >= RepeatCount)
            {
                Clear();
                return false;
            }
            else
            {
                m_time = 0f;
                return true;
            }
        }

        public void SetParameters(Action method, float interval, int repeatCount, Func<bool> enabled)
        {
            Method = method;
            Interval = interval;
            RepeatCount = repeatCount;
            IsEnabled = enabled;
            IsPooled = false;
        }

        private void Clear()
        {
            m_time = 0f;
            m_count = 0;
            Method = null;
            IsEnabled = null;
            IsPooled = true;
        }
    }
}