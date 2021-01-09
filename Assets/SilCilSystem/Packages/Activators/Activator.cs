using UnityEngine;
using SilCilSystem.Variables;
using System;
using SilCilSystem.Timers;

namespace SilCilSystem.Activators
{
    public abstract class Activator : MonoBehaviour
    {
        [Header("Options")]
        [SerializeField] private bool m_setOnStart = true;
        [SerializeField] private bool m_setOnUpdate = true;

        [Header("Binding")]
        [SerializeField] internal ReadonlyPropertyBool m_isActive = new ReadonlyPropertyBool(true);
        [SerializeField] private bool m_reverse = false;

        private class Updatable : IUpdatable
        {
            public GameObject LifeTimeObject { get; set; }
            public MonoBehaviour Component { get; set; }
            public Action UpdateAction { get; set; }
            public bool Enabled => LifeTimeObject != null && Component.enabled;
            public bool Update(float deltaTime)
            {
                UpdateAction?.Invoke();
                return LifeTimeObject != null;
            }
        }

        private void Awake()
        {
            var updatable = new Updatable()
            {
                LifeTimeObject = gameObject,
                Component = this,
                UpdateAction = UpdateAction,
            };
            UpdateDispatcher.Register(updatable, UpdateMode.DeltaTime);
        }

        private void Start()
        {
            if (m_setOnStart) SetActives(m_isActive);
        }

        private void UpdateAction()
        {
            if (m_setOnUpdate)
            {
                SetActives((m_reverse == false) ? m_isActive : !m_isActive);
            }
        }

        protected abstract void SetActives(bool value);
    }
}