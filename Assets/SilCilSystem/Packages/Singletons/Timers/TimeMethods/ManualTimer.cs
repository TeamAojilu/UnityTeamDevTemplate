using System;
using SilCilSystem.Variables;
using SilCilSystem.ObjectPools;

namespace SilCilSystem.Timers
{
    internal class ManualTimer : IUpdatable, IPooledObject
    {
        public bool Enabled => IsEnabled?.Invoke() != false;
        public bool IsPooled { get; private set; } = false;

        /// <summary>true when it is null</summary>
        public Func<bool> IsEnabled { get; set; }
        /// <summary>1st arg is current time. true when it is null</summary>
        public Func<float, bool> IsFinished { get; set; }
        /// <summary>1f when it is null</summary>
        public Func<float> TimeScale { get; set; }
        /// <summary>【Default】0f</summary>
        public PropertyFloat Time { get; private set; } = new PropertyFloat(0f);

        public void ResetPooledObject() => Reset(false);

        public bool Update(float deltaTime)
        {
            Time.Value += deltaTime * (TimeScale?.Invoke() ?? 1f);
            var finished = IsFinished?.Invoke(Time.Value) != false;
            if (finished) Reset(true);
            return !finished;
        }

        private void Reset(bool pooled)
        {
            IsFinished = null;
            IsEnabled = null;
            IsPooled = pooled;
            TimeScale = null;
            Time.Variable = null;
            Time.Value = 0f;
        }
    }
}