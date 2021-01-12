using System;
using SilCilSystem.ObjectPools;
using Object = UnityEngine.Object;

namespace SilCilSystem.Timers
{
    internal class CallDelayedObject : IUpdatable, IPooledObject
    {
        private float m_time = 0f;

        public float Delay { get; set; }

        public Object LifeTimeObject { get; set; }

        public Action CallBack { get; set; }

        /// <summary>nullならtrue扱い</summary>
        public Func<bool> IsEnable { get; set; }

        public bool Enabled => LifeTimeObjectIsNull() == false && IsEnable?.Invoke() != false;

        public bool IsPooled => LifeTimeObject == null;

        public bool Update(float deltaTime)
        {
            m_time += deltaTime;
            if (m_time < Delay) return true;
            CallBack?.Invoke();
            Clear();
            return false;
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
            m_time = 0f;
            Delay = 0f;
            LifeTimeObject = null;
            CallBack = null;
            IsEnable = null;
        }
    }
}