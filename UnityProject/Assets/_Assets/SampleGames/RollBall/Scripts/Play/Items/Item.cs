using SilCilSystem.Variables;
using UnityEngine;

namespace Samples.RollBall
{
    public class Item : MonoBehaviour, IItem
    {
        [SerializeField] private GameEvent m_onItemPicked = default;

        public void PickUp()
        {
            m_onItemPicked?.Publish();
            Destroy(gameObject);
        }
    }
}