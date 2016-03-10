using System;

namespace CSharpRxMessageBus.Utility
{
    public class DisposableSubscription : IDisposable
    {
        public DisposableSubscription(IDisposable disposable)
        {
            _disposible = disposable;
        }

        ~DisposableSubscription()
        {
            Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            var disposed = _isDisposed;
            if (disposed || _disposible == null) return;

            _isDisposed = true;
            _disposible.Dispose();
        }

        private volatile bool _isDisposed = false;
        private readonly IDisposable _disposible;
    }
}