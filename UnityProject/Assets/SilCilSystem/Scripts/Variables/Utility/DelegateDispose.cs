using System;

namespace SilCilSystem.Variables
{
    /// <summary>Dispose呼び出しで登録したActionを呼ぶ</summary>
    public class DelegateDispose : IDisposable
    {
        private readonly Action m_delegate;

        /// <summary>Dispose呼び出しで登録したActionを呼ぶ</summary>
        private DelegateDispose(Action action) => m_delegate = action;
        
        public void Dispose() => m_delegate.Invoke();

        /// <summary>
        /// コンストラクタを隠すための静的メソッド
        /// 後々キャッシュなどの最適化用に作成.
        /// </summary>
        public static IDisposable Create(Action action)
        {
            return new DelegateDispose(action);
        }
    }
}