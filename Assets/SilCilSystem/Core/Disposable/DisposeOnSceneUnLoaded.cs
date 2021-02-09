using System;

namespace SilCilSystem.Variables
{
    public static class DisposeOnSceneChangedExtensios
    {
        public const int ExecutionOrder = -1000;

        /// <summary>シーンの切り替え時に自動でDisposeが呼ばれるようにする</summary>
        public static void DisposeOnSceneChanged(this IDisposable disposable)
        {
            SceneChangedDispatcher.Register((_, __) => disposable?.Dispose(), ExecutionOrder);
        }
    }
}