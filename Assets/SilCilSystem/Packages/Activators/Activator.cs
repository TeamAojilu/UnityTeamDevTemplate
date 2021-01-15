using UnityEngine;
using SilCilSystem.Variables;
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

        private void Start()
        {
            if (m_setOnStart) SetActives(m_isActive);
            UpdateDispatcher.Register(MicroUpdate, gameObject, UpdateMode.DeltaTime);
        }

        private bool MicroUpdate(float deltaTime)
        {
            if (!enabled) return true;
            if (m_setOnUpdate) SetActives((m_reverse == false) ? m_isActive : !m_isActive);
            return true;
        }

        protected abstract void SetActives(bool value);
    }
}