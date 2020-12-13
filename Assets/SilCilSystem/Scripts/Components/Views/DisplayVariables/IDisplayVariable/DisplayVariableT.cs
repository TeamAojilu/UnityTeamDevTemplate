using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Math;

namespace SilCilSystem.Components.Views
{
    internal interface IDisplayVariable
    {
        string Key { get; }
        bool IsBusy { get; }
        void Initialize();
        string Update();
    }

    [System.Serializable]
    internal abstract class DisplayVariableWithAnimation<T, TVariable>: IDisplayVariable where TVariable : ReadonlyVariable<T>
    {
        [Header("Basic")]
        [SerializeField] private string m_key = "key"; // インスペクタのヘッダーがm_keyの内容になるようにstring.
        [SerializeField] protected ReadonlyPropertyString m_format = new ReadonlyPropertyString("G");
        [SerializeField] private TVariable m_variable = default;

        [Header("Animation")]
        [SerializeField] private ReadonlyPropertyFloat m_duration = new ReadonlyPropertyFloat(-1f);
        [SerializeField] private InterpolationCurve m_curve = default;
        [SerializeField] private ReadonlyPropertyBool m_useInitial = new ReadonlyPropertyBool(false);
        [SerializeField] private T m_initialValue = default;

        private ReadonlyVariableAnimation<T, TVariable> m_animation = default;

        protected abstract ReadonlyVariableAnimation<T, TVariable> CreateAnimation(TVariable variable, ReadonlyPropertyFloat duration, InterpolationCurve curve);

        public string Key => m_key;
        public bool IsBusy => m_animation.IsBusy();

        public void Initialize()
        {
            m_animation = CreateAnimation(m_variable, m_duration, m_curve);
            m_animation.Initialize((m_useInitial) ? m_initialValue : m_variable);
        }

        public string Update()
        {
            return ToText(m_animation.Update());
        }

        protected abstract string ToText(T value);
    }
}