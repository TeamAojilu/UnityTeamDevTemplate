using UnityEngine;
using SilCilSystem.Variables;

namespace Samples.RollBall
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private ReadonlyPropertyFloat m_speed = new ReadonlyPropertyFloat(10f);

        private Rigidbody m_rigidbody = default;

        private void Start()
        {
            m_rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            var force = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) * m_speed;
            m_rigidbody.AddForce(force);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IPickup pickup))
            {
                other.gameObject.SetActive(false);
                pickup.OnPicked();
            }
        }
    }
}