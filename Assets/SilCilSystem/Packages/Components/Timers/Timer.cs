using UnityEngine;
using SilCilSystem.Variables;
using UnityEngine.Events;

namespace SilCilSystem.Components.Timers
{
    [AddComponentMenu(menuName: Constants.AddComponentPath + "Timers/" + nameof(Timer))]
    public class Timer : MonoBehaviour
    {
        public ReadonlyPropertyBool m_enable = new ReadonlyPropertyBool(true);

        [Header("Basic")]
        public PropertyFloat m_time = new PropertyFloat(0f);
        public ReadonlyPropertyFloat m_initialTime = new ReadonlyPropertyFloat(0f);
        public ReadonlyPropertyFloat m_timeScale = new ReadonlyPropertyFloat(1f);

        [Header("Range")]
        public ReadonlyPropertyFloat m_min = new ReadonlyPropertyFloat(0f);
        public ReadonlyPropertyFloat m_max = new ReadonlyPropertyFloat(float.MaxValue);

        [Header("Repeat")]
        public ReadonlyPropertyBool m_repeating = new ReadonlyPropertyBool(false);

        [Header("Events")]
        public UnityEvent m_onMinValue = default;
        public UnityEvent m_onMaxValue = default;

        private void Start()
        {
            SetTime(m_initialTime);
        }

        private void Update()
        {
            if (m_time == null) return;
            if (!m_enable) return;
            SetTime(m_time + Time.deltaTime * m_timeScale);
        }

        private void SetTime(float t)
        {
            if (m_repeating)
            {
                t = Mathf.Repeat(t - m_min, m_max - m_min) + m_min;
                if (t == m_time) return;

                float dt = t - m_time;
                m_time.Value = t;

                // ループする場合、最大値に到達＝最小値に戻るなので、両方同時に達成.
                // ループした＝timeScaleとは逆の方向に変化した.
                if (m_timeScale * dt >= 0f) return;
                m_onMinValue?.Invoke();
                m_onMaxValue?.Invoke();
            }
            else
            {
                t = Mathf.Clamp(t, m_min, m_max);
                if (t == m_time) return;

                m_time.Value = t;
                if (t == m_min) m_onMinValue?.Invoke();
                if (t == m_max) m_onMaxValue?.Invoke();
            }
        }
    }
}
