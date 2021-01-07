using System;

namespace SilCilSystem.Timers
{
    internal class SimpleTimer : ITimerObject
    {
        public float CurrentTime { get; private set; } = 0f;
        public bool Enabled => IsEnabled?.Invoke() != false;
        public bool IsPooled { get; private set; } = false;

        /// <summary>nullならtrue</summary>
        public Func<bool> IsEnabled { get; set; } = null;

        /// <summary>nullなら終わらない</summary>
        public Func<float, bool> IsFinished { get; set; } = null;

        public void Reset()
        {
            CurrentTime = 0f;
            IsPooled = false;
        }

        public void ToPool()
        {
            IsEnabled = null;
            IsFinished = null;
            IsPooled = true;
        }

        public bool Update(float deltaTime)
        {
            if (IsPooled) return false;

            CurrentTime += deltaTime;
            if(IsFinished?.Invoke(CurrentTime) == true)
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