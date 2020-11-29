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
    internal abstract class DisplayVariable<T>: IDisplayVariable
    {
        [Header("Basic")]
        [SerializeField] private string m_key = "key";
        [SerializeField] protected string m_format = "G";
        [SerializeField] private ScriptableObject m_variable = default;

        [Header("Start")]
        [SerializeField] private bool m_useInitial = false;
        [SerializeField] private T m_initialValue = default;

        [Header("Update")]
        [SerializeField] private float m_duration = -1f;
        [SerializeField] private InterpolationCurve m_curve = default;

        // 変数オブジェクト.
        private Variable<T> m_value = default;
        private ReadonlyVariable<T> m_readonlyValue = default;
        private T Value => (m_value != null) ? m_value.Value : m_readonlyValue.Value;

        // Update処理用.
        private T m_startValue = default;
        private T m_targetValue = default;
        private float m_timer = 0f;

        public string Key => m_key;
        public bool IsBusy => !EqualityComparer<T>.Default.Equals(Value, m_targetValue) || m_timer < m_duration;

        public void Initialize()
        {
            m_value = m_variable as Variable<T>;
            m_readonlyValue = m_variable as ReadonlyVariable<T>;
            m_startValue = (m_useInitial) ? m_initialValue : Value;
            m_timer = 0f;
        }

        public string Update()
        {
            // 値が変更された時はtimerを0に戻す.
            if (!EqualityComparer<T>.Default.Equals(Value, m_targetValue))
            {
                m_startValue = GetCurrentValue();
                m_timer = 0f;
                m_targetValue = Value;
            }

            // 時間変化させない場合は変数の値をそのまま使う.
            if (m_timer >= m_duration) return ToText(Value);

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