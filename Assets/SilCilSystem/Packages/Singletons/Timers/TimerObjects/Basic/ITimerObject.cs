using SilCilSystem.ObjectPools;
using System;

namespace SilCilSystem.Timers
{
    internal interface ITimerObject : IUpdatable, IPooledObject
    {
        float CurrentTime { get; }
        Func<float, bool> IsFinished { get; set; }
        void Reset();
        void ToPool();
    }
}