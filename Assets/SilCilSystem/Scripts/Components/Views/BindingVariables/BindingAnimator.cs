using System;
using UnityEngine;
using SilCilSystem.Variables;

namespace SilCilSystem.Components.Views
{
    [AddComponentMenu(menuName: EditorConstants.AddComponentPath + "Views/" + nameof(BindingAnimator))]
    [RequireComponent(typeof(Animator))]
    public class BindingAnimator : BindingComponent, IBindingParameters
    {
        [Serializable]
        private abstract class ParameterInfo<TProperty>
        {
            public string m_name = default;
            public TProperty m_property = default;
        }

        [Serializable] private class ParameterInt : ParameterInfo<ReadonlyPropertyInt> { }
        [Serializable] private class ParameterFloat : ParameterInfo<ReadonlyPropertyFloat> { }
        [Serializable] private class ParameterBool : ParameterInfo<ReadonlyPropertyBool> { }
        [Serializable] private class ParameterTrigger : ParameterInfo<GameEventListener> { }

        [Header("Params")]
        [SerializeField] private ParameterInt[] m_intValues = default;
        [SerializeField] private ParameterFloat[] m_floatValues = default;
        [SerializeField] private ParameterBool[] m_boolValues = default;
        [SerializeField] private ParameterTrigger[] m_triggers = default;
        
        private Animator m_animator = default;
        private IDisposable m_disposable = default;

        protected override void OnValidate()
        {
            base.OnValidate();
            m_setOnUpdate = true;
        }

        protected override IBindingParameters GetBindingParameters()
        {
            if (m_intValues == null) return null;
            m_animator = GetComponent<Animator>();
            return this;
        }

        public void SetParameters()
        {
            if (m_animator == null) return;
            SetInts();
            SetFloats();
            SetBools();
        }

        private void SetInts()
        {
            foreach (var param in m_intValues)
            {
                if (string.IsNullOrEmpty(param?.m_name)) continue;
                m_animator.SetInteger(param.m_name, param.m_property);
            }
        }

        private void SetFloats()
        {
            foreach (var param in m_floatValues)
            {
                if (string.IsNullOrEmpty(param?.m_name)) continue;
                m_animator.SetFloat(param.m_name, param.m_property);
            }
        }

        private void SetBools()
        {
            foreach (var param in m_boolValues)
            {
                if (string.IsNullOrEmpty(param?.m_name)) continue;
                m_animator.SetBool(param.m_name, param.m_property);
            }
        }

        private IDisposable SetTriggers()
        {
            var disposable = new CompositeDisposable();
            foreach (var trigger in m_triggers)
            {
                if (string.IsNullOrEmpty(trigger?.m_name)) continue;
                disposable.Add(trigger.m_property.Subscribe(() => m_animator.SetTrigger(trigger.m_name)));
            }
            return disposable;
        }

        private void OnEnable()
        {
            m_disposable = SetTriggers();
        }

        private void OnDisable() => m_disposable?.Dispose();
    }
}