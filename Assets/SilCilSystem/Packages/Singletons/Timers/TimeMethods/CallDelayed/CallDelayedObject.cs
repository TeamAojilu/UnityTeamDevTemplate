using System;
using SilCilSystem.ObjectPools;

namespace SilCilSystem.Timers
{
    internal class CallDelayedObject : IPooledObject
    {
        private float m_time = 0f;

        public float Delay { get; set; }
        public Action Method { get; private set; }
        /// <summary>nullならtrue扱い</summary>
        public Func<bool> IsEnable { get; private set; }
        public bool IsPooled { get; private set; } = true;

        public bool Update(float deltaTime)
        {
            if (IsPooled) return false;
            if (IsEnable?.Invoke() == false) return true;
            m_time += deltaTime;
            if (m_time < Delay) return true;
            Method?.Invoke();
            Clear();
            return false;
        }

        public void SetParameter(Action method, float delay, Func<bool> enabled = null)
        {
            Method = method;
            Delay = delay;
            IsEnable = enabled;
            IsPooled = false;
        }

        private void Clear()
        {
            m_time = 0f;
            Delay = 0f;
            Method = null;
            IsEnable = null;
            IsPooled = true;
        }
    }
}