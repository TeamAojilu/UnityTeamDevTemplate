using UnityEngine;
using SilCilSystem.Variables;
using System;

namespace SilCilSystem.Components.Activators
{
    public abstract class Activator : MonoBehaviour
    {
        [Header("Events")]
        [SerializeField] private GameEventBoolListener m_setActive = default;
        [SerializeField] private GameEventListener m_setActiveTrue = default;
        [SerializeField] private GameEventListener m_setActiveFalse = default;

        private IDisposable m_disposable = default;

        private void OnEnable()
        {
            var disposable = new CompositeDisposable();
            disposable.Add(m_setActive?.Subscribe(SetActives));
            disposable.Add(m_setActiveTrue?.Subscribe(() => SetActives(true)));
            disposable.Add(m_setActiveFalse?.Subscribe(() => SetActives(false)));
            m_disposable = disposable;
        }

        private void OnDisable() => m_disposable?.Dispose();

        protected abstract void SetActives(bool value);
    }
}