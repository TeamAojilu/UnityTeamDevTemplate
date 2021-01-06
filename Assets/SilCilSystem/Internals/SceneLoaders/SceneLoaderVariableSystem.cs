using UnityEngine;
using System;
using SilCilSystem.Variables;
using SilCilSystem.SceneLoaders;

namespace SilCilSystem.Internals.SceneLoaders
{
    internal class SceneLoaderVariableSystem : MonoBehaviour
    {
        [SerializeField] private PropertyBool m_isBusy = default;
        [SerializeField] private GameEventStringListener m_loadScene = default;

        private IDisposable m_disposable = default;

        private void OnEnable()
        {
            m_disposable = m_loadScene?.Subscribe(x => SceneLoader.LoadScene(x));
            m_isBusy.Value = SceneLoader.IsBusy;
        }

        private void OnDisable() => m_disposable?.Dispose();

        private void Update()
        {
            if (SceneLoader.IsBusy == m_isBusy) return;
            m_isBusy.Value = SceneLoader.IsBusy;
        }
    }
}