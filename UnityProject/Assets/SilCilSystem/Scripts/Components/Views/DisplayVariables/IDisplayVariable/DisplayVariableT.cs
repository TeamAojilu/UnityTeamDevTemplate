using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.Math;
using System.Collections.Generic;

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
    internal abstract class DisplayVariable<T, TVariable>: IDisplayVariable where TVariable : ReadonlyVariable<T>
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

        // Update処理用.
        private T m_startValue = default;
        private T m_targetValue = default;
        private float m_timer = 0f;

        public string Key => m_key;
        public bool IsBusy => !EqualityComparer<T>.Default.Equals(m_variable, m_targetValue) || m_timer < m_duration;

        public void Initialize()
        {
            m_startValue = (m_useInitial) ? m_initialValue : m_variable;
            m_timer = 0f;
        }

        public string Update()
        {
            // 値が変更された時はtimerを0に戻す.
            if (!EqualityComparer<T>.Default.Equals(m_variable, m_targetValue))
            {
                m_startValue = GetCurrentValue();
                m_timer = 0f;
                m_targetValue = m_variable;
            }

            // 時間変化させない場合は変数の値をそのまま使う.
            if (m_timer >= m_duration) return ToText(m_variable);

            m_timer += Time.deltaTime;
            return ToText(GetCurrentValue());
        }

        protected abstract string ToText(T value);
        protected abstract T Lerp(T start, T end, float t);

        private T GetCurrentValue()
        {
            float rate = Mathf.Clamp01(m_timer / m_duration);
            return Lerp(m_startValue, m_targetValue, m_curve.Evaluate(rate));
        }
    }
}