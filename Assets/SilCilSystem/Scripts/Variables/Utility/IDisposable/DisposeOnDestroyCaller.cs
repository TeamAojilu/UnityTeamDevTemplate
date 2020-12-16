using System;
using UnityEngine;

namespace SilCilSystem.Variables
{
    internal class DisposeOnDestroyCaller : MonoBehaviour
    {
        private CompositeDisposable m_disposable = new CompositeDisposable();

        private void OnDestroy()
        {
            m_disposable?.Dispose();
        }

        internal void Set(IDisposable disposable)
        {
            m_disposable.Add(disposable);
        }
    }

    public static class DisposeOnDestroyCallerExtensions
    {
        /// <summary>ゲームオブジェクトが破棄される時に自動でDisposeが呼ばれるようにする</summary>
        public static void DisposeOnDestroy(this IDisposable disposable, GameObject gameObject)
        {
            if (gameObject == null) return;

            if(gameObject.TryGetComponent(out DisposeOnDestroyCaller caller))
            {
                caller.Set(disposable);
            }
            else
            {
                gameObject.AddComponent<DisposeOnDestroyCaller>().Set(disposable);
            }
        }
    }
}