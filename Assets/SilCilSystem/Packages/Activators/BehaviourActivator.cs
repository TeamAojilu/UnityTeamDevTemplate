using UnityEngine;

namespace SilCilSystem.Activators
{
    [AddComponentMenu(menuName: Constants.AddComponentPath + "Activators/" + nameof(BehaviourActivator))]
    public class BehaviourActivator : Activator
    {
        [Header("Behaviours")]
        [SerializeField] private Behaviour[] m_targets = default;

        protected override void SetActives(bool value)
        {
            foreach (var target in m_targets)
            {
                if (target == null) continue;
                target.enabled = value;
            }
        }
    }
}