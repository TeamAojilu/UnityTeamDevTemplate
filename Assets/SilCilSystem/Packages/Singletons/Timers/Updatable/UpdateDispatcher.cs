using System;
using System.Collections.Generic;
using UnityEngine;
using SilCilSystem.Singletons;

namespace SilCilSystem.Timers
{
    public class UpdateDispatcher : SingletonMonoBehaviour<UpdateDispatcher>
    {
        public static void Register(IUpdatable updatable, UpdateMode mode, bool canDuplicated = false)
            => Instance.Add(updatable, mode, canDuplicated);

        private const int Capacity = 32;
        private Dictionary<UpdateMode, IUpdatable[]> m_updatables = new Dictionary<UpdateMode, IUpdatable[]>();

        protected override void OnAwake()
        {
            foreach(UpdateMode key in Enum.GetValues(typeof(UpdateMode)))
            {
                m_updatables[key] = new IUpdatable[Capacity];
            }
        }

        private void Add(IUpdatable timer, UpdateMode mode, bool canDuplicated)
        {
            for (int i = 0; i < m_updatables[mode].Length; i++)
            {
                if (canDuplicated == false && m_updatables[mode][i] == timer) return;
                if (m_updatables[mode][i] != null) continue;
                m_updatables[mode][i] = timer;
                return;
            }

            // 配列がすべて埋まっている場合は配列をリサイズ.
            var array = m_updatables[mode];
            int length = array.Length;
            Array.Resize(ref array, length * 2);
            array[length] = timer;
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
                if (!m_updatables[mode][i].Enabled) continue;
                if (m_updatables[mode][i].Update(deltaTime)) continue;
                m_updatables[mode][i] = null;
            }
        }

        protected override void OnDestroyCallback() { }
    }
}