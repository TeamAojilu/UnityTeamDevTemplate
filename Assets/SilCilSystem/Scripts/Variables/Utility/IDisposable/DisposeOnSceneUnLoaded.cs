using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SilCilSystem.Variables
{
    public static class DisposeOnSceneUnLoadedExtensios
    {
        private static bool _Registered = false;
        private static List<IDisposable> _Disposables = new List<IDisposable>(); 

        [RuntimeInitializeOnLoadMethod]
        private static void RegisterCallback()
        {
            if (_Registered) return;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        private static void OnSceneUnloaded(Scene arg0)
        {
            foreach (var dispose in _Disposables)
            {
                dispose?.Dispose();
            }
            _Disposables.Clear();
#if UNITY_EDITOR
            Debug.Log("DisposeOnSceneUnloaded");
#endif
        }

        /// <summary>シーンのアンロード時に自動でDisposeが呼ばれるようにする</summary>
        public static void DisposeOnSceneUnLoaded(this IDisposable disposable)
        {
            _Disposables.Add(disposable);
        }
    }
}