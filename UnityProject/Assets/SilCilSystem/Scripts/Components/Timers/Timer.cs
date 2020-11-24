using UnityEngine;
using SilCilSystem.Variables;

namespace SilCilSystem.Components
{
    [AddComponentMenu(menuName: EditorConstants.AddComponentPath + "Times/" + nameof(Timer))]
    public class Timer : MonoBehaviour
    {
        public enum TimerMode
        {
            CountUp,
            CountDown,
        }

        [SerializeField] private ReadonlyBool m_running = default;
        [SerializeField] private VariableFloat m_time = default;
        [SerializeField] private float m_initialTime = 30f;
        [SerializeField] private TimerMode m_mode = TimerMode.CountUp;

        private void Start()
        {
            if (m_time == null) return;
            m_time.Value = m_initialTime;
        }

        private void Update()
        {
            if (m_time == null) return;
            if (m_running?.Value != true) return;

            switch (m_mode)
            {
                case TimerMode.CountUp:
                    m_time.Value += Time.deltaTime;
                    break;
                case TimerMode.CountDown:
                    m_time.Value -= Time.deltaTime;
                    break;
            }
        }
    }
}
