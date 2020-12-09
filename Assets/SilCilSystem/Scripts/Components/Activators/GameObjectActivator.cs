using UnityEngine;

namespace SilCilSystem.Components.Activators
{
    [AddComponentMenu(menuName: EditorConstants.AddComponentPath + "Activators/" + nameof(GameObjectActivator))]
    public class GameObjectActivator : Activator
    {
        [Header("GameObjects")]
        [SerializeField] private GameObject[] m_targets = default;

        protected override void SetActives(bool value) 
        {
            foreach(var target in m_targets)
            {
                if (target == null) continue;
                target.SetActive(value);
            }
        }
    }
}