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
        public static void DisposeOnDestroy(this IDisposable disposable, GameObject lifeTimeObject)
        {
            if (lifeTimeObject == null)
            {
                Debug.LogError($"{nameof(DisposeOnDestroy)}: {nameof(lifeTimeObject)} is null");
                return;
            }

            if(lifeTimeObject.TryGetComponent(out DisposeOnDestroyCaller caller))
            {
                caller.Set(disposable);
            }
            else
            {
                lifeTimeObject.AddComponent<DisposeOnDestroyCaller>().Set(disposable);
            }
        }

        /// <summary>イベントの登録. ゲームオブジェクトが破棄される時に自動でDisposeが呼ばれる</summary>
        public static void Subscribe(this GameEvent gameEvent, Action action, GameObject lifeTimeObject)
        {
            gameEvent?.Subscribe(action).DisposeOnDestroy(lifeTimeObject);
        }

        /// <summary>イベントの登録. ゲームオブジェクトが破棄される時に自動でDisposeが呼ばれる</summary>
        public static void Subscribe<T>(this GameEvent<T> gameEvent, Action<T> action, GameObject lifeTimeObject)
        {
            gameEvent?.Subscribe(action).DisposeOnDestroy(lifeTimeObject);
        }
    }
}