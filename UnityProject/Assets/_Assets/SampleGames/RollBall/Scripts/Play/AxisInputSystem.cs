using UnityEngine;
using SilCilSystem.Variables;

namespace Samples.RollBall
{
    public class AxisInputSystem : MonoBehaviour
    {
        [SerializeField] private ReadonlyBool m_running = default;
        [SerializeField] private VariableFloat m_horizontal = default;
        [SerializeField] private VariableFloat m_vertical = default;

        private void Update()
        {
            if (m_running?.Value != true) return;
            m_horizontal.Value = Input.GetAxis("Horizontal");
            m_vertical.Value = Input.GetAxis("Vertical");
        }
    }
}