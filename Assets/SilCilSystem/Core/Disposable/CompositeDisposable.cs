using System;
using System.Collections.Generic;

namespace SilCilSystem.Variables
{
    /// <summary>複数のIDisposableを1つにまとめる</summary>
    public class CompositeDisposable : IDisposable
    {
        private readonly List<IDisposable> m_disposables = new List<IDisposable>();

        public void Add(IDisposable item) => m_disposables.Add(item);
        public void Remove(IDisposable item) => m_disposables.Remove(item);

        public void Dispose()
        {
            foreach(var disposable in m_disposables)
            {
                disposable?.Dispose();
            }
            m_disposables.Clear();
        }
    }
}