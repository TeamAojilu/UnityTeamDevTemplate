using UnityEngine;
using SilCilSystem.Variables;
using SilCilSystem.SceneLoaders;

namespace SilCilSystem.Internals.SceneLoaders
{
    internal class SceneLoaderVariableSystem : MonoBehaviour
    {
        [SerializeField] private PropertyBool m_isBusy = default;
        [SerializeField] private GameEventStringListener m_loadScene = default;

        private void Awake()
        {
            m_loadScene?.Subscribe(x => SceneLoader.LoadScene(x), gameObject);
            m_isBusy.Value = SceneLoader.IsBusy;
        }

        private void Update()
        {
            if (SceneLoader.IsBusy == m_isBusy) return;
            m_isBusy.Value = SceneLoader.IsBusy;
        }
    }
}