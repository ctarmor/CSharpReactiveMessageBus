using System;

namespace CSharpRxMessageBus.Utility
{
    public class RxTimerDisposible
    {
        public IDisposable Set(IDisposable d)
        {
            _disposible = d;

            return d;
        }

        ~RxTimerDisposible()
        {
            Unsubscribe();
        }

        public void Unsubscribe()
        {
            _disposible?.Dispose();
            _disposible = null;
        }

        public IDisposable _disposible;
    }
}