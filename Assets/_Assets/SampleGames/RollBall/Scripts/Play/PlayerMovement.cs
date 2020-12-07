using SilCilSystem.Variables;
using UnityEngine;

namespace Samples.RollBall
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float m_force = 10f;
        [SerializeField] private ReadonlyFloat m_inputX = default;
        [SerializeField] private ReadonlyFloat m_inputY = default;

        private Rigidbody m_rigidbody = default;

        private void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();    
        }

        private void FixedUpdate()
        {
            var x = m_inputX?.Value ?? 0f;
            var y = m_inputY?.Value ?? 0f;
            var direction = new Vector3(x, 0f, y).normalized;
            m_rigidbody.AddForce(direction * m_force);
        }
    }
}