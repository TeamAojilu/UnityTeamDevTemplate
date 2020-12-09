using UnityEngine;

namespace SilCilSystem.Components.Views
{
    public interface IBindingParameters
    {
        void SetParameters();
    }

    public abstract class BindingComponent : MonoBehaviour
    {
        [SerializeField] private bool m_setOnStart = true;
        [SerializeField] protected bool m_setOnUpdate = true;

        public IBindingParameters Binding { get; private set; }
        protected abstract IBindingParameters GetBindingParameters();

        protected virtual void Start()
        {
            Binding = GetBindingParameters();

            if (!m_setOnStart) return;
            Binding?.SetParameters();
        }

        protected virtual void OnValidate()
        {
            Binding = GetBindingParameters();
            Binding?.SetParameters();
        }
        
        protected virtual void Update()
        {
            if (!m_setOnUpdate) return;
            Binding?.SetParameters();
        }
    }
}