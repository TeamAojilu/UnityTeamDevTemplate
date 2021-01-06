using System.Collections;
using UnityEngine;

namespace SilCilSystem.ObjectPools
{
    public interface IPooledObject
    {
        bool IsPooled { get; }
        void ResetPooledObject();
    }
}