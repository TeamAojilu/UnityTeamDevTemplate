using SilCilSystem.Variables;
using UnityEngine;

namespace Samples.RollBall
{
    public class ItemGetter : MonoBehaviour
    {
        [SerializeField] private ReadonlyBool m_isActive = default;

        private void OnTriggerEnter(Collider other)
        {
            if (m_isActive?.Value != true) return;
            if (!other.TryGetComponent(out IItem item)) return;
            item.PickUp();
        }
    }
}