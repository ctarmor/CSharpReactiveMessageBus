using System;
using System.Reactive.Subjects;
using CSharpRxMessageBus.Interface;
using CSharpRxMessageBus.Utility;

namespace CSharpRxMessageBus
{
    /// <summary>
    /// Supports full Reactive Subject behavior. 
    /// </summary>
    public class RxMemoryBus 
    {
        public virtual ISubject<T> GetBus<T>()
        {
            // Get Bus. Factor if it does nto exists.
            var bus = _busDictionary.Get(typeof(T), () => new Subject<T>());

            return bus as ISubject<T>;
        }

        public virtual IObservable<T> Subscriber<T>()
        {
            return GetBus<T>();
        }

        public virtual void Publish<T>(T msg)
        {
            GetBus<T>().OnNext(msg);
        }

        public virtual void Complete<T>()
        {
            GetBus<T>().OnCompleted();
        }

        private readonly LockingDictionary<Type, object> _busDictionary = new LockingDictionary<Type, object>();
    }
}
