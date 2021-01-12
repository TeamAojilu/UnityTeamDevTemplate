using System;
using SilCilSystem.ObjectPools;
using Object = UnityEngine.Object;

namespace SilCilSystem.Timers
{
    internal class CallRepeatedlyObject : IUpdatable, IPooledObject
    {
        private float m_time = 0f;
        private int m_count = 0;

        public Action Method { get; set; }

        public float Interval { get; set; }

        public Object LifeTimeObject { get; set; }

        /// <summary>負なら無限</summary>
        public int RepeatCount { get; set; }

        /// <summary>nullならtrue扱い</summary>
        public Func<bool> IsEnabled { get; set; }

        public bool Enabled => LifeTimeObjectIsNull() == false && IsEnabled?.Invoke() != false;

        public bool IsPooled => LifeTimeObject == null;

        public bool Update(float deltaTime)
        {
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

        private bool LifeTimeObjectIsNull()
        {
            if(LifeTimeObject == null)
            {
                Clear();
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Clear()
        {
            Method = null;
            LifeTimeObject = null;
            IsEnabled = null;
            m_time = 0f;
            m_count = 0;
        }
    }
}