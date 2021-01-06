using System;
using System.Collections.Generic;

namespace SilCilSystem.ObjectPools
{
    public class ObjectPool<T> where T : IPooledObject
    {
        private List<T> m_instances = new List<T>();
        private readonly Func<T> m_createFunction;

        public ObjectPool(Func<T> createFunction)
        {
            m_createFunction = createFunction;
        }

        public T GetInstance()
        {
            var instance = GetOrCreateInstance();
            instance.ResetPooledObject();
            return instance;
        }

        private T GetOrCreateInstance()
        {
            foreach (var instance in m_instances)
            {
                if (instance == null) continue;
                if (!instance.IsPooled) continue;
                return instance;
            }

            var newInstance = m_createFunction.Invoke();
            m_instances.Add(newInstance);
            return newInstance;
        }
    }
}