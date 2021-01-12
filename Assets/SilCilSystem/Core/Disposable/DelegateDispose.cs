using System;
using SilCilSystem.ObjectPools;

namespace SilCilSystem.Variables
{
    /// <summary>Dispose呼び出しで登録したActionを呼ぶIDisposable</summary>
    public class DelegateDispose : IDisposable, IPooledObject
    {
        private static ObjectPool<DelegateDispose> ObjectPool = new ObjectPool<DelegateDispose>(() => new DelegateDispose());

        public static IDisposable Create(Action action)
        {
            var instance = ObjectPool.GetInstance();
            instance.m_delegate = action;
            return instance;
        }

        private Action m_delegate = default;
        
        private DelegateDispose() { }

        bool IPooledObject.IsPooled => m_delegate == null;

        public void Dispose()
        {
            m_delegate?.Invoke();
            m_delegate = null;
        }
    }
}