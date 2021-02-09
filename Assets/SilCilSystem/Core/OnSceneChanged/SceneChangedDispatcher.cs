using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SilCilSystem.Variables
{
    public static class SceneChangedDispatcher
    {
        private class InvokeInfo
        {
            public event Action<Scene, Scene> Callback;
            public void Invoke(Scene arg0, Scene arg1)
            {
                Callback?.Invoke(arg0, arg1);
            }
        }

        private static Dictionary<int, InvokeInfo> _Callbacks = new Dictionary<int, InvokeInfo>();

        public static void Register(Action<Scene,Scene> action, int order)
        {
            if (_Callbacks.ContainsKey(order) == false)
            {
                _Callbacks[order] = new InvokeInfo();
            }

            _Callbacks[order].Callback += action;
        }

        /// <summary>【注意】orderが異なっていた場合は解除できない、エラーも吐かない</summary>
        public static void UnRegister(Action<Scene,Scene> action, int order)
        {
            if (_Callbacks.ContainsKey(order) == false) return;
            _Callbacks[order].Callback -= action;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Initialize()
        {
            SceneManager.activeSceneChanged += OnSceneChanged;
        }

        private static void OnSceneChanged(Scene arg0, Scene arg1)
        {
            foreach (var callback in _Callbacks.OrderBy(x => x.Key))
            {
                callback.Value.Invoke(arg0, arg1);
            }
        }
    }
}