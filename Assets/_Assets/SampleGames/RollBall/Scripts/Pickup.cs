using UnityEngine;
using SilCilSystem.Variables;

namespace Samples.RollBall
{
    public class Pickup : MonoBehaviour, IPickup
    {
        [SerializeField] private VariableInt m_count = default;

        public void OnPicked()
        {
            m_count.Value++;
        }
    }
}