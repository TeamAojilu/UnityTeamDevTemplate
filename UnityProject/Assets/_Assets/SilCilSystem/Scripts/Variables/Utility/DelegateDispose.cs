using System;

namespace SilCilSystem.Variables
{
    /// <summary>Dispose呼び出しで登録したActionを呼ぶ</summary>
    public class DelegateDispose : IDisposable
    {
        private readonly Action m_delegate;

        /// <summary>Dispose呼び出しで登録したActionを呼ぶ</summary>
        public DelegateDispose(Action action) => m_delegate = action;
        
        public void Dispose() => m_delegate.Invoke();
    }
}