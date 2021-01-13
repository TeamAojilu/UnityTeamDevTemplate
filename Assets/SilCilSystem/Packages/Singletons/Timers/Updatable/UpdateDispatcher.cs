using System;
using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Singletons;
using SilCilSystem.ObjectPools;
using Object = UnityEngine.Object;

namespace SilCilSystem.Timers
{
    public class UpdateDispatcher : SingletonMonoBehaviour<UpdateDispatcher>
    {
        public static void Register(Func<float, bool> update, Object lifeTimeObject, UpdateMode mode = UpdateMode.DeltaTime)
            => Instance.Add(update, lifeTimeObject, mode);
        
        private class UpdateInfo : IPooledObject
        {
            public Object LifeTimeObject { get; set; }
            public Func<float, bool> Update { get; set; }
            public bool IsPooled => LifeTimeObject == null;
            public void Clear()
            {
                LifeTimeObject = null;
                Update = null;
            }
        }

        private const int Capacity = 32;
        private Dictionary<UpdateMode, UpdateInfo[]> m_updatables = new Dictionary<UpdateMode, UpdateInfo[]>();
        private ObjectPool<UpdateInfo> m_pool = new ObjectPool<UpdateInfo>(() => new UpdateInfo());

        protected override void OnAwake()
        {
            foreach(UpdateMode key in Enum.GetValues(typeof(UpdateMode)))
            {
                m_updatables[key] = new UpdateInfo[Capacity];
            }
        }

        private void Add(Func<float, bool> update, Object lifeTimeObject, UpdateMode mode)
        {
            var instance = m_pool.GetInstance();
            instance.LifeTimeObject = lifeTimeObject;
            instance.Update = update;

            // 空いているところに設定する.
            for (int i = 0; i < m_updatables[mode].Length; i++)
            {
                if (m_updatables[mode][i] != null) continue;
                m_updatables[mode][i] = instance;
                return;
            }

            // 配列がすべて埋まっている場合は配列をリサイズ.
            var array = m_updatables[mode];
            int length = array.Length;
            Array.Resize(ref array, length * 2);
            array[length] = instance;
        }

        private void FixedUpdate()
        {
            UpdateTimers(UpdateMode.FixedUnscaledDeltaTime, Time.fixedUnscaledDeltaTime);
            UpdateTimers(UpdateMode.FixedDeltaTime, Time.fixedDeltaTime);
        }

        private void Update()
        {
            UpdateTimers(UpdateMode.UnscaledDeltaTime, Time.unscaledDeltaTime);
            UpdateTimers(UpdateMode.DeltaTime, Time.deltaTime);
        }

        private void LateUpdate()
        {
            UpdateTimers(UpdateMode.LateUpdateUnscaledDeltaTime, Time.unscaledDeltaTime);
            UpdateTimers(UpdateMode.LateUpdateDeltaTime, Time.deltaTime);
        }

        private void UpdateTimers(UpdateMode mode, float deltaTime)
        {
            for(int i = 0; i < m_updatables[mode].Length; i++)
            {
                if (m_updatables[mode][i] == null) continue;
                if (m_updatables[mode][i].LifeTimeObject == null || m_updatables[mode][i].Update?.Invoke(deltaTime) != true)
                {
                    m_updatables[mode][i]?.Clear();
                    m_updatables[mode][i] = null;            
                }
            }
        }

        protected override void OnDestroyCallback() { }
    }
}