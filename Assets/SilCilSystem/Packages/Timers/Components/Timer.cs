using UnityEngine;
using SilCilSystem.Variables;
using UnityEngine.Events;

namespace SilCilSystem.Timers
{
    [AddComponentMenu(menuName: Constants.AddComponentPath + "Timers/" + nameof(Timer))]
    public class Timer : MonoBehaviour
    {
        [SerializeField] internal ReadonlyPropertyBool m_enable = new ReadonlyPropertyBool(true);

        [Header("Basic")]
        [SerializeField] internal PropertyFloat m_time = new PropertyFloat(0f);
        [SerializeField] internal ReadonlyPropertyFloat m_min = new ReadonlyPropertyFloat(0f);
        [SerializeField] internal ReadonlyPropertyFloat m_max = new ReadonlyPropertyFloat(float.MaxValue);
        [SerializeField] internal ReadonlyPropertyFloat m_timeScale = new ReadonlyPropertyFloat(1f);
        
        [Header("Initial")]
        [SerializeField] internal UpdateMode m_updateMode = UpdateMode.DeltaTime;
        [SerializeField] internal ReadonlyPropertyBool m_useInitialTime = new ReadonlyPropertyBool(true);
        [SerializeField] internal ReadonlyPropertyFloat m_initialTime = new ReadonlyPropertyFloat(0f);

        [Header("Repeat")]
        [SerializeField] internal ReadonlyPropertyBool m_repeating = new ReadonlyPropertyBool(false);

        [Header("Events")]
        [SerializeField] internal UnityEvent m_onMinValue = default;
        [SerializeField] internal UnityEvent m_onMaxValue = default;

        private void Start()
        {
            if (m_useInitialTime) SetTime(m_initialTime);
            UpdateDispatcher.Register(MicroUpdate, gameObject);
        }

        private bool MicroUpdate(float deltaTime)
        {
            if (m_time == null) return true;
            if (!enabled) return true;
            if (!m_enable) return true;
            SetTime(m_time + deltaTime * m_timeScale);
            return true;
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
