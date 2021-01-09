using UnityEngine;
using SilCilSystem.Variables;

namespace Samples.NewRollBall
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private ReadonlyPropertyFloat m_speed = new ReadonlyPropertyFloat(10f);
        [SerializeField] private VariableBool m_isPlaying = default;
        [SerializeField] private VariableInt m_count = default;
        [SerializeField] private string m_pickupTag = "PickUp";

        private Rigidbody m_rigidbody = default;

        private void Start()
        {
            m_count.Value = 0;
            m_rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            var force = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) * m_speed;
            m_rigidbody.AddForce(force);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(m_pickupTag))
            {
                m_count.Value++;
                if (m_count >= 13) m_isPlaying.Value = false;
                other.gameObject.SetActive(false);
            }
        }
    }
}