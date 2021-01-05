using SilCilSystem.Variables.Generic;
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
            if (gameObject == null)
            {
                Debug.LogError($"{nameof(DisposeOnDestroy)}: GameObject is null");
                return;
            }

            if(gameObject.TryGetComponent(out DisposeOnDestroyCaller caller))
            {
                caller.Set(disposable);
            }
            else
            {
                gameObject.AddComponent<DisposeOnDestroyCaller>().Set(disposable);
            }
        }

        /// <summary>イベントの登録. ゲームオブジェクトが破棄される時に自動でDisposeが呼ばれる</summary>
        public static void Subscribe(this GameEvent gameEvent, Action action, GameObject gameObject)
        {
            gameEvent?.Subscribe(action).DisposeOnDestroy(gameObject);
        }

        /// <summary>イベントの登録. ゲームオブジェクトが破棄される時に自動でDisposeが呼ばれる</summary>
        public static void Subscribe<T>(this GameEvent<T> gameEvent, Action<T> action, GameObject gameObject)
        {
            gameEvent?.Subscribe(action).DisposeOnDestroy(gameObject);
        }
    }
}